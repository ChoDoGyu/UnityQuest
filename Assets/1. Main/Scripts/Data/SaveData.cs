using System.Collections.Generic;
using UnityEngine;
using Main.Scripts.Player;

namespace Main.Scripts.Data
{
    [System.Serializable]
    public class SaveData
    {
        public string saveId;           // ��: "Auto_0", "Manual_0"
        public string saveTime;         // ���� �ð�
        public string currentScene;     // ���� ��
        public Vector3 playerPosition;  // �÷��̾� ��ġ
        public int saveVersion = CURRENT_VERSION;         // ���� ����
        public string previewImagePath; // �̸����� �̹��� ���
        public const int CURRENT_VERSION = 1;

        //������ �и�
        public PlayerData playerData;         // ü��, ���ݷ�, ���, ���
        public InventoryData inventoryData;   // �κ��丮 ������
        public List<QuestProgressData> questList;     // ����Ʈ ����Ʈ
    }

    [System.Serializable]
    public class PlayerData
    {
        public float hp;  // ü��
        public int gold;  // ���� ���
        public List<string> equippedItemIds;  // ������ ������ ����Ʈ (������ ID)
        public Dictionary<StatType, float> stats; // ���ݷ�, ���� �� ����
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<string> itemIds = new List<string>();  // ���� ������ ����Ʈ (������ ID)
    }

    [System.Serializable]
    public class QuestProgressData
    {
        public string questId;   // ����Ʈ ID
        public bool isCompleted; // ����Ʈ �Ϸ� ����
        public List<ObjectiveProgress> objectives;  // ����Ʈ ���� ���� (��: "���� óġ", "������ ����")
    }

    [System.Serializable]
    public class ObjectiveProgress
    {
        public string objectiveId;
        public int currentAmount;
    }
}
