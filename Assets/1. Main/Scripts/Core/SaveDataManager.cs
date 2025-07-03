using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;
using Main.Scripts.Data;
using UnityEngine.SceneManagement;
using Main.Scripts.Player;

namespace Main.Scripts.Core
{
    [System.Serializable]
    public class SaveData
    {
        public string saveId;                // 예: "Auto_0", "Manual_1"
        public string saveTime;              // 저장 시간 (문자열로 저장)
        public string currentScene;          // 저장 당시의 씬 이름
        public Vector3 playerPosition;       // 플레이어 위치
        public float playerHp;               // 플레이어 체력
        public string previewImagePath;

        //추가
        public int saveVersion = CURRENT_VERSION;

        //상수 선언
        public const int CURRENT_VERSION = 1;
    }

    public class SaveDataManager : MonoBehaviour
    {
        public static SaveDataManager Instance { get; private set; }

        private const int MaxManualSaves = 2;
        private string SaveDir => Application.persistentDataPath + "/Saves";

        private SaveData pendingLoadData;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (!Directory.Exists(SaveDir))
                Directory.CreateDirectory(SaveDir);
        }

        public enum SaveSlotType { Auto, Manual }


        public void SaveGame(SaveSlotType type)
        {
            var data = new SaveData();

            // 저장 ID 설정
            string saveId = (type == SaveSlotType.Auto) ? "Auto_0" : GetNextManualSlotId();
            data.saveId = saveId;
            data.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            data.currentScene = SceneManager.GetActiveScene().name;

            // 플레이어 위치 저장
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                data.playerPosition = player.transform.position;

                var status = player.GetComponent<PlayerStat>();
                if (status != null)
                {
                    data.playerHp = status.GetStat(StatType.HP);
                }
                else
                {
                    Debug.LogWarning("[Save] PlayerStatus 컴포넌트가 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("[Save] 태그가 'Player'인 오브젝트를 찾을 수 없습니다.");
            }

            //Preview 폴더 경로 생성
            string previewDir = Path.Combine(SaveDir, "Preview");
            if (!Directory.Exists(previewDir))
                Directory.CreateDirectory(previewDir);

            //이미지 캡처 및 저장
            Texture2D preview = CaptureScreenshot();
            string previewPath = Path.Combine(previewDir, saveId + ".png");
            File.WriteAllBytes(previewPath, preview.EncodeToPNG());
            data.previewImagePath = previewPath;

            // JSON 저장
            string json = JsonUtility.ToJson(data, true);
            string encoded = Encode(json);
            string filePath = Path.Combine(SaveDir, saveId + ".json");
            File.WriteAllText(filePath, encoded);

            Debug.Log($"[저장 완료] {saveId} → {filePath}");
        }

        public void LoadGame(string saveFileName)
        {
            string path = Path.Combine(SaveDir, saveFileName);
            if (!File.Exists(path))
            {
                Debug.LogError($"[로드 실패] 파일 없음: {path}");
                return;
            }

            try
            {
                // 1. 저장된 JSON 파일의 내용을 문자열로 읽음
                string encoded = File.ReadAllText(path);
                string json = Decode(encoded);

                // 2. 문자열(json)을 SaveData 객체로 변환 (역직렬화)
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                // 3.유효성 검사: 저장된 버전이 현재 버전과 다른 경우 경고 출력 후 로드 중단
                if (data.saveVersion != SaveData.CURRENT_VERSION)
                {
                    Debug.LogWarning($"[로드 실패] 저장 버전 불일치: {data.saveVersion}");
                    return;
                }

                // 4. 이후 씬 로딩이 완료되면 데이터를 적용하기 위해 임시로 저장
                pendingLoadData = data;

                // 5. 씬 로딩 후 이벤트 등록
                SceneManager.sceneLoaded += OnSceneLoaded;

                // 6. 저장된 씬으로 이동
                SceneManager.LoadScene(data.currentScene);
            }
            catch (Exception ex)
            {
                //예외 발생 시: 예외 메시지를 출력 (ex: 파일 없음, JSON 포맷 오류 등)
                Debug.LogError($"[로드 실패] JSON 파싱 오류: {ex.Message}");
            }
        }

        public void DeleteSave(string fileName)
        {
            string path = Path.Combine(SaveDir, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log($"[삭제] 저장 파일 제거됨: {fileName}");
            }
        }

        public List<SaveSlotData> GetSaveSlots()
        {
            List<SaveSlotData> slotList = new();

            if (!Directory.Exists(SaveDir))
                return slotList;

            string[] files = Directory.GetFiles(SaveDir, "*.json");


            foreach (var path in files)
            {
                try
                {
                    string encoded = File.ReadAllText(path);
                    string json = Decode(encoded);
                    SaveData data = JsonUtility.FromJson<SaveData>(json);

                    if (data.saveVersion != SaveData.CURRENT_VERSION)
                    {
                        Debug.LogWarning($"[무시됨] 호환되지 않는 저장 버전: {path}");
                        continue;
                    }

                    var slotData = new SaveSlotData
                    {
                        saveTime = data.saveTime,
                        title = FormatSlotTitle(data.saveId)
                    };

                    string previewPath = data.previewImagePath;
                    if (!string.IsNullOrEmpty(previewPath) && File.Exists(previewPath))
                    {
                        byte[] bytes = File.ReadAllBytes(previewPath);
                        Texture2D tex = new Texture2D(2, 2);
                        tex.LoadImage(bytes);
                        slotData.previewTexture = tex;
                    }

                    slotList.Add(slotData);
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"[무시됨] 저장 슬롯 읽기 실패: {path} → {e.Message}");
                }
            }

            return slotList;
        }

        private string GetNextManualSlotId()
        {
            // 수동 저장은 2개까지만 유지: Manual_0, Manual_1
            string[] slots = Directory.GetFiles(SaveDir, "Manual_*.json");

            if (slots.Length < 2)
                return $"Manual_{slots.Length}";

            // 두 개가 다 있으면 오래된 걸 Manual_0으로 덮어쓰기
            return "Manual_0"; // 이후 로직에서 Manual_1을 Manual_0으로 밀어도 됨
        }

        private string FormatSlotTitle(string saveId)
        {
            if (saveId.StartsWith("Auto"))
                return "자동 저장";
            if (saveId.StartsWith("Manual_0"))
                return "수동 저장 1";
            if (saveId.StartsWith("Manual_1"))
                return "수동 저장 2";
            return "저장 슬롯";
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (pendingLoadData == null) return;

            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = pendingLoadData.playerPosition;

                var stat = player.GetComponent<PlayerStat>();
                if (stat != null)
                {
                    stat.SetStat(StatType.HP, pendingLoadData.playerHp);
                }
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;
            pendingLoadData = null;
        }

        private Texture2D CaptureScreenshot()
        {
            int width = Screen.width;
            int height = Screen.height;
            Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

            tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            tex.Apply();

            return tex;
        }

        private string Encode(string plainText)
        {
            byte[] plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainBytes);
        }

        private string Decode(string encodedText)
        {
            byte[] encodedBytes = Convert.FromBase64String(encodedText);
            return System.Text.Encoding.UTF8.GetString(encodedBytes);
        }
    }
}