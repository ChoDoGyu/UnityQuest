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
        public string saveId;                // ��: "Auto_0", "Manual_1"
        public string saveTime;              // ���� �ð� (���ڿ��� ����)
        public string currentScene;          // ���� ����� �� �̸�
        public Vector3 playerPosition;       // �÷��̾� ��ġ
        public float playerHp;               // �÷��̾� ü��
        public string previewImagePath;

        //�߰�
        public int saveVersion = CURRENT_VERSION;

        //��� ����
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

            // ���� ID ����
            string saveId = (type == SaveSlotType.Auto) ? "Auto_0" : GetNextManualSlotId();
            data.saveId = saveId;
            data.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            data.currentScene = SceneManager.GetActiveScene().name;

            // �÷��̾� ��ġ ����
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
                    Debug.LogWarning("[Save] PlayerStatus ������Ʈ�� �����ϴ�.");
                }
            }
            else
            {
                Debug.LogWarning("[Save] �±װ� 'Player'�� ������Ʈ�� ã�� �� �����ϴ�.");
            }

            //Preview ���� ��� ����
            string previewDir = Path.Combine(SaveDir, "Preview");
            if (!Directory.Exists(previewDir))
                Directory.CreateDirectory(previewDir);

            //�̹��� ĸó �� ����
            Texture2D preview = CaptureScreenshot();
            string previewPath = Path.Combine(previewDir, saveId + ".png");
            File.WriteAllBytes(previewPath, preview.EncodeToPNG());
            data.previewImagePath = previewPath;

            // JSON ����
            string json = JsonUtility.ToJson(data, true);
            string encoded = Encode(json);
            string filePath = Path.Combine(SaveDir, saveId + ".json");
            File.WriteAllText(filePath, encoded);

            Debug.Log($"[���� �Ϸ�] {saveId} �� {filePath}");
        }

        public void LoadGame(string saveFileName)
        {
            string path = Path.Combine(SaveDir, saveFileName);
            if (!File.Exists(path))
            {
                Debug.LogError($"[�ε� ����] ���� ����: {path}");
                return;
            }

            try
            {
                // 1. ����� JSON ������ ������ ���ڿ��� ����
                string encoded = File.ReadAllText(path);
                string json = Decode(encoded);

                // 2. ���ڿ�(json)�� SaveData ��ü�� ��ȯ (������ȭ)
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                // 3.��ȿ�� �˻�: ����� ������ ���� ������ �ٸ� ��� ��� ��� �� �ε� �ߴ�
                if (data.saveVersion != SaveData.CURRENT_VERSION)
                {
                    Debug.LogWarning($"[�ε� ����] ���� ���� ����ġ: {data.saveVersion}");
                    return;
                }

                // 4. ���� �� �ε��� �Ϸ�Ǹ� �����͸� �����ϱ� ���� �ӽ÷� ����
                pendingLoadData = data;

                // 5. �� �ε� �� �̺�Ʈ ���
                SceneManager.sceneLoaded += OnSceneLoaded;

                // 6. ����� ������ �̵�
                SceneManager.LoadScene(data.currentScene);
            }
            catch (Exception ex)
            {
                //���� �߻� ��: ���� �޽����� ��� (ex: ���� ����, JSON ���� ���� ��)
                Debug.LogError($"[�ε� ����] JSON �Ľ� ����: {ex.Message}");
            }
        }

        public void DeleteSave(string fileName)
        {
            string path = Path.Combine(SaveDir, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log($"[����] ���� ���� ���ŵ�: {fileName}");
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
                        Debug.LogWarning($"[���õ�] ȣȯ���� �ʴ� ���� ����: {path}");
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
                    Debug.LogWarning($"[���õ�] ���� ���� �б� ����: {path} �� {e.Message}");
                }
            }

            return slotList;
        }

        private string GetNextManualSlotId()
        {
            // ���� ������ 2�������� ����: Manual_0, Manual_1
            string[] slots = Directory.GetFiles(SaveDir, "Manual_*.json");

            if (slots.Length < 2)
                return $"Manual_{slots.Length}";

            // �� ���� �� ������ ������ �� Manual_0���� �����
            return "Manual_0"; // ���� �������� Manual_1�� Manual_0���� �о ��
        }

        private string FormatSlotTitle(string saveId)
        {
            if (saveId.StartsWith("Auto"))
                return "�ڵ� ����";
            if (saveId.StartsWith("Manual_0"))
                return "���� ���� 1";
            if (saveId.StartsWith("Manual_1"))
                return "���� ���� 2";
            return "���� ����";
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