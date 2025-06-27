using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Player;

namespace Main.Scripts.Core
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }

        // 현재 진행 중인 퀘스트들을 저장하는 딕셔너리 (key: questId)
        private readonly Dictionary<string, QuestData> activeQuests = new();

        // 완료한 퀘스트들의 ID를 저장하는 해시셋
        private readonly HashSet<string> completedQuests = new();//HashSet<string>	완료 여부 체크할 때 O(1) 성능

        private void Awake()
        {
            // 싱글톤 패턴 구성
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        /// <summary>
        /// 퀘스트 수락 처리. 이미 수락한 퀘스트는 중복 수락 불가
        /// </summary>
        public void AcceptQuest(QuestData quest)
        {
            if (activeQuests.ContainsKey(quest.questId))
            {
                GameManager.Instance.LogToConsole("이미 수락한 퀘스트입니다.");
                return;
            }

            activeQuests.Add(quest.questId, quest);
            GameManager.Instance.LogToConsole($"[퀘스트 수락] {quest.questName}");
        }

        /// <summary>
        /// 퀘스트 목표 진행도 업데이트.
        /// 몬스터 처치/아이템 획득 시 호출됨.
        /// </summary>
        public void UpdateObjective(string objectiveId, int amount = 1)
        {
            foreach (var quest in activeQuests.Values)
            {
                foreach (var obj in quest.objectives)
                {
                    // objectiveId가 일치하고 아직 완료되지 않은 경우에만 갱신
                    if (obj.objectiveId == objectiveId && !obj.IsComplete)
                    {
                        obj.AddProgress(amount);  // 진행도 증가

                        // UI 로그 출력
                        GameManager.Instance.LogToConsole(
                            $"[퀘스트 진행] {obj.description} ({obj.currentAmount}/{obj.requiredAmount})"
                        );

                        // 전체 목표가 모두 완료되었는지 체크 (퀘스트 완료 가능)
                        if (IsObjectiveComplete(quest))
                        {
                            GameManager.Instance.LogToConsole(
                                $"[퀘스트 완료 가능] {quest.questName} → NPC에게 보고하세요."
                            );
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 퀘스트 완료 처리.
        /// - 보상 지급
        /// - 효과 출력
        /// - 상태 업데이트
        /// </summary>
        public void CompleteQuest(QuestData quest, Vector3 position)
        {
            // 예외처리: 수락한 퀘스트가 아니거나 아직 완료 조건을 못 채운 경우
            if (!activeQuests.ContainsKey(quest.questId)) return;
            if (!IsObjectiveComplete(quest)) return;

            // 1. 골드 보상
            PlayerWallet.Instance.AddGold(quest.rewardGold);

            // 2. 아이템 보상
            foreach (var item in quest.rewardItems)
            {
                PlayerInventory.Instance.AddItem(item);
            }

            // 3. 효과 재생 (FX + 사운드)
            GameManager.Instance.FX?.PlayEffect("QuestComplete", position);
            GameManager.Instance.Audio?.PlaySFX("QuestComplete");

            // 4. 로그 출력
            GameManager.Instance.LogToConsole($"[퀘스트 완료] {quest.questName} 보상 지급 완료!");

            // 5. 퀘스트 상태 갱신
            activeQuests.Remove(quest.questId);
            completedQuests.Add(quest.questId);
        }

        /// <summary>
        /// 퀘스트가 현재 진행 중인지 확인
        /// </summary>
        public bool IsInProgress(QuestData quest) => activeQuests.ContainsKey(quest.questId);

        /// <summary>
        /// 퀘스트가 완료되었는지 확인
        /// </summary>
        public bool IsCompleted(QuestData quest) => completedQuests.Contains(quest.questId);

        /// <summary>
        /// 해당 퀘스트의 모든 목표가 완료되었는지 확인
        /// </summary>
        public bool IsObjectiveComplete(QuestData quest)//AND 조건 판단 루프 구조
        {
            foreach (var obj in quest.objectives)
            {
                if (!obj.IsComplete) return false;
            }
            return true;
        }
    }
}