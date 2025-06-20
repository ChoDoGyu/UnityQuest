using UnityEngine;
using System.Collections.Generic;

namespace Main.Scripts.Core
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance { get; private set; }

        [SerializeField] private RectTransform mapRect;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private GameObject iconPrefab;

        private List<MapIcon> icons = new List<MapIcon>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Update()
        {
            foreach (MapIcon icon in icons)
            {
                Vector2 pos = WorldToMapPosition(icon.Target.position);
                icon.RectTransform.anchoredPosition = pos;
            }
        }

        /// <summary>
        /// 아이콘 등록 (플레이어, 몬스터 등)
        /// </summary>
        public void RegisterIcon(Transform target)
        {
            if (target == null) return;

            GameObject iconGO = Instantiate(iconPrefab, mapRect);
            MapIcon icon = new MapIcon(target, iconGO.GetComponent<RectTransform>());
            icons.Add(icon);
        }

        /// <summary>
        /// 아이콘 제거
        /// </summary>
        public void UnregisterIcon(Transform target)
        {
            for (int i = icons.Count - 1; i >= 0; i--)
            {
                if (icons[i].Target == target)
                {
                    Destroy(icons[i].RectTransform.gameObject);
                    icons.RemoveAt(i);
                    break;
                }
            }
        }

        private Vector2 WorldToMapPosition(Vector3 worldPos)
        {
            Vector3 offset = worldPos - playerTransform.position;
            return new Vector2(offset.x, offset.z) * 5f; // 스케일은 필드 크기에 맞게
        }
    }

    public class MapIcon
    {
        public Transform Target { get; }
        public RectTransform RectTransform { get; }

        public MapIcon(Transform target, RectTransform rect)
        {
            Target = target;
            RectTransform = rect;
        }
    }
}