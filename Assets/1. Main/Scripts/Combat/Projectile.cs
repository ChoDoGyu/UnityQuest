using UnityEngine;
using Main.Scripts.Core;

namespace Main.Scripts.Combat
{
    public class Projectile : MonoBehaviour
    {
        private float speed = 10f;
        private Vector3 direction;
        private GameObject originPrefab; // 어떤 프리팹에서 나왔는지 기록

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
            Debug.Log($"{name} 충돌: {other.name}");

            // 데미지 처리 등 추후 추가 가능

            //Destroy 대신 ObjectPool 반환
            if (originPrefab != null)
            {
                ObjectPoolManager.Instance.ReturnToPool(originPrefab, this.gameObject);
            }
            else
            {
                Destroy(gameObject); // 예외 상황: prefab 정보 없을 경우 안전망
            }
        }
    }
}
