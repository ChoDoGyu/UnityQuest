using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.CameraSystem
{
    public class CameraRaycaster : MonoBehaviour
    {
        [SerializeField] private Transform player;
        [SerializeField] private LayerMask obstacleLayer;
        [SerializeField] private Material transparentMaterial;

        private Dictionary<Renderer, Material[]> originalMaterials = new();

        private void LateUpdate()
        {
            ClearObstacles();
            HandleObstacles();
        }

        private void HandleObstacles()
        {
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;

            Ray ray = new Ray(transform.position, direction);
            RaycastHit[] hits = Physics.RaycastAll(ray, distance, obstacleLayer);

            foreach (var hit in hits)
            {
                //바뀐 부분: root 대신 hit.collider.transform 사용
                Renderer renderer = hit.collider.GetComponent<Renderer>();

                if (renderer != null)
                {
                    if (!originalMaterials.ContainsKey(renderer))
                    {
                        originalMaterials[renderer] = renderer.materials;
                        Material[] newMats = new Material[renderer.materials.Length];
                        for (int i = 0; i < newMats.Length; i++)
                        {
                            newMats[i] = transparentMaterial;
                        }
                        renderer.materials = newMats;
                    }
                }
            }
        }

        private void ClearObstacles()
        {
            foreach (var kvp in originalMaterials)
            {
                if (kvp.Key != null)
                {
                    kvp.Key.materials = kvp.Value;
                }
            }
            originalMaterials.Clear();
        }
    }
}
