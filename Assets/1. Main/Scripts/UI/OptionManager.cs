using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using Main.Scripts.Core;

namespace Main.Scripts.UI
{
    public class OptionManager : MonoBehaviour
    {
        [Header("탭에 따라 표시할 패널들")]
        [SerializeField] private GameObject soundPanel;
        [SerializeField] private GameObject resolutionPanel;
        [SerializeField] private GameObject keybindPanel;

        [Header("Audio 설정")]
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private Slider uiSlider;

        [Header("해상도 설정")]
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        [Header("입력 시스템 (키 바인딩)")]
        [SerializeField] private InputActionAsset inputActions;

        [SerializeField] private GameObject optionRoot;

        private Resolution[] resolutions;

        private void Start()
        {
            SetupResolutionOptions();
        }

        private void OnEnable()
        {
            LoadOptions();
            ShowSoundPanel();
        }

        //볼륨 설정 슬라이더 초기화
        private void SetupAudioSliders()
        {
            float master = PlayerPrefs.GetFloat("MasterVolume", 100f);
            float bgm = PlayerPrefs.GetFloat("BGMVolume", 100f);
            float sfx = PlayerPrefs.GetFloat("SFXVolume", 100f);
            float ui = PlayerPrefs.GetFloat("UIVolume", 100f);

            masterSlider.SetValueWithoutNotify(master);
            bgmSlider.SetValueWithoutNotify(bgm);
            sfxSlider.SetValueWithoutNotify(sfx);
            uiSlider.SetValueWithoutNotify(ui);

            AudioManager.Instance?.SetVolume("MasterVolume", master / 100f);
            AudioManager.Instance?.SetVolume("BGMVolume", bgm / 100f);
            AudioManager.Instance?.SetVolume("SFXVolume", sfx / 100f);
            AudioManager.Instance?.SetVolume("UIVolume", ui / 100f);
        }

        //해상도 드롭다운 설정
        private void SetupResolutionOptions()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            List<string> options = new List<string>();
            int current = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                var res = resolutions[i];
                string option = $"{res.width} x {res.height}";
                options.Add(option);

                if (res.width == Screen.currentResolution.width &&
                    res.height == Screen.currentResolution.height)
                {
                    current = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("ResolutionIndex", current));
            resolutionDropdown.RefreshShownValue();
        }

        //실시간 볼륨 적용용 메서드 (슬라이더 이벤트 연결)
        public void SetMasterVolume(float value) => AudioManager.Instance?.SetVolume("MasterVolume", value / 100f);
        public void SetBGMVolume(float value) => AudioManager.Instance?.SetVolume("BGMVolume", value / 100f);
        public void SetSFXVolume(float value) => AudioManager.Instance?.SetVolume("SFXVolume", value / 100f);
        public void SetUIVolume(float value) => AudioManager.Instance?.SetVolume("UIVolume", value / 100f);

        //해상도 선택 이벤트 처리
        public void SetResolution(int index)
        {
            if (resolutions == null || resolutions.Length == 0) return;
            Resolution res = resolutions[index];
            Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
        }

        //옵션 저장
        public void SaveOptions()
        {
            PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
            PlayerPrefs.SetFloat("BGMVolume", bgmSlider.value);
            PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
            PlayerPrefs.SetFloat("UIVolume", uiSlider.value);

            PlayerPrefs.SetInt("ResolutionIndex", resolutionDropdown.value);

            if (inputActions!= null)
            {
                string bindings = inputActions.SaveBindingOverridesAsJson();
                PlayerPrefs.SetString("KeyBindings", bindings);
            }

            PlayerPrefs.Save();
            Debug.Log("[Option] 설정 저장 완료");

            //AudioManager에 실시간 반영
            AudioManager.Instance?.SetMasterVolume(masterSlider.value / 100f);
            AudioManager.Instance?.SetBGMVolume(bgmSlider.value / 100f);
            AudioManager.Instance?.SetSFXVolume(sfxSlider.value / 100f);
            AudioManager.Instance?.SetUIVolume(uiSlider.value / 100f);

            if (optionRoot != null)
                optionRoot.SetActive(false);
        }

        //옵션 복원
        public void LoadOptions()
        {
            SetupAudioSliders();

            if (resolutions == null || resolutions.Length == 0)
                resolutions = Screen.resolutions;

            int resolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", 0);

            if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
            {
                Resolution res = resolutions[resolutionIndex];
                Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
            }

            resolutionDropdown.SetValueWithoutNotify(resolutionIndex);
            resolutionDropdown.RefreshShownValue();

            string bindingJson = PlayerPrefs.GetString("KeyBindings", "");
            if (!string.IsNullOrEmpty(bindingJson) && inputActions != null)
            {
                inputActions.LoadBindingOverridesFromJson(bindingJson);
            }
        }

        //탭 전환 UI 처리
        public void ShowSoundPanel()
        {
            soundPanel.SetActive(true);
            resolutionPanel.SetActive(false);
            keybindPanel.SetActive(false);
        }

        // 해상도 탭 표시
        public void ShowResolutionPanel()
        {
            soundPanel.SetActive(false);
            resolutionPanel.SetActive(true);
            keybindPanel.SetActive(false);
        }

        // 키 설정 탭 표시
        public void ShowKeybindPanel()
        {
            soundPanel.SetActive(false);
            resolutionPanel.SetActive(false);
            keybindPanel.SetActive(true);
        }
    }
}
