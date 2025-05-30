using UnityEngine;
using Main.Scripts.Core;

namespace Main.Scripts.Combat
{
    public class Projectile : MonoBehaviour
    {
        private float speed = 10f;
        private Vector3 direction;
        private GameObject originPrefab; // � �����տ��� ���Դ��� ���

        public void Init(Vector3 dir, float moveSpeed, GameObject prefabSource)
        {
            direction = dir.normalized;
            speed = moveSpeed;
            originPrefab = prefabSource;
        }

        private void Update()
        {
            transform.position += direction * speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"{name} �浹: {other.name}");

            // ������ ó�� �� ���� �߰� ����

            //Destroy ��� ObjectPool ��ȯ
            if (originPrefab != null)
            {
                ObjectPoolManager.Instance.ReturnToPool(originPrefab, this.gameObject);
            }
            else
            {
                Destroy(gameObject); // ���� ��Ȳ: prefab ���� ���� ��� ������
            }
        }
    }
}
