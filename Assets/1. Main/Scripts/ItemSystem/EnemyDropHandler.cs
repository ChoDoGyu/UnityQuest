using UnityEngine;
using Main.Scripts.Data;

namespace Main.Scripts.ItemSystem
{
    public class EnemyDropHandler : MonoBehaviour
    {
        [SerializeField] private DropTable dropTable;

        public void DropItems()
        {
            foreach (var entry in dropTable.drops)
            {
                if (Random.value <= entry.dropChance && entry.item.worldPrefab != null)
                {
                    Instantiate(entry.item.worldPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }
}
