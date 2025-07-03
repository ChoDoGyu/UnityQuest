using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Scripts.Data
{
    [System.Serializable]
    public class SaveSlotData
    {
        public string title;              // 예: "자동 저장", "수동 저장 1"
        public string saveTime;          // 예: "2025-07-02 14:10"
        public Texture2D previewTexture;
    }
}

