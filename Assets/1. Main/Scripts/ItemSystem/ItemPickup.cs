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
                Debug.LogWarning("PlayerManager를 찾을 수 없습니다.");
                return;
            }

            switch (itemData.itemType)
            {
                case ItemType.Gold:
                    // 골드 시스템이 없으므로 임시 처리
                    Debug.Log($"골드 {itemData.itemName} 획득 (추후 시스템 연동 필요)");
                    break;

                case ItemType.Potion:
                    // 인벤토리에 추가만 하고 사용은 나중에 → 임시 출력
                    Debug.Log($"포션 {itemData.itemName} 인벤토리에 추가됨 (미사용 상태)");
                    break;
            }

            Destroy(gameObject); // 아이템 제거
        }
    }
}
