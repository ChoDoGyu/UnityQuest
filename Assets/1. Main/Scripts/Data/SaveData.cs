using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Player;

namespace Main.Scripts.Data
{
    [System.Serializable]
    public class SaveData
    {
        public string saveId;           // 예: "Auto_0", "Manual_0"
        public string saveTime;         // 저장 시간
        public string currentScene;     // 현재 씬
        public Vector3 playerPosition;  // 플레이어 위치
        public int saveVersion = CURRENT_VERSION;         // 저장 버전
        public string previewImagePath; // 미리보기 이미지 경로
        public const int CURRENT_VERSION = 1;

        //데이터 분리
        public PlayerData playerData;         // 체력, 공격력, 장비, 골드
        public InventoryData inventoryData;   // 인벤토리 데이터
        public List<QuestProgressData> questList;     // 퀘스트 리스트
    }

    [System.Serializable]
    public class PlayerData
    {
        public float hp;  // 체력
        public int gold;  // 보유 골드
        public List<string> equippedItemIds;  // 장착된 아이템 리스트 (아이템 ID)
        public Dictionary<StatType, float> stats; // 공격력, 방어력 등 스탯
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<string> itemIds = new List<string>();  // 보유 아이템 리스트 (아이템 ID)
    }

    [System.Serializable]
    public class QuestProgressData
    {
        public string questId;   // 퀘스트 ID
        public bool isCompleted; // 퀘스트 완료 여부
        public List<ObjectiveProgress> objectives;  // 퀘스트 진행 상태 (예: "보스 처치", "아이템 수집")
    }

    [System.Serializable]
    public class ObjectiveProgress
    {
        public string objectiveId;
        public int currentAmount;
    }
}
