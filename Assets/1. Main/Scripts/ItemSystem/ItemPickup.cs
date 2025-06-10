using UnityEngine;
using Main.Scripts.Data;
using Main.Scripts.Player;

namespace Main.Scripts.ItemSystem
{
    public class ItemPickup : MonoBehaviour
    {
        public ItemData itemData;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            var player = other.GetComponent<PlayerManager>();
            if (player == null)
            {
                Debug.LogWarning("PlayerManager�� ã�� �� �����ϴ�.");
                return;
            }

            switch (itemData.itemType)
            {
                case ItemType.Gold:
                    // ��� �ý����� �����Ƿ� �ӽ� ó��
                    Debug.Log($"��� {itemData.itemName} ȹ�� (���� �ý��� ���� �ʿ�)");
                    break;

                case ItemType.Potion:
                    // �κ��丮�� �߰��� �ϰ� ����� ���߿� �� �ӽ� ���
                    Debug.Log($"���� {itemData.itemName} �κ��丮�� �߰��� (�̻�� ����)");
                    break;
            }

            Destroy(gameObject); // ������ ����
        }
    }
}
