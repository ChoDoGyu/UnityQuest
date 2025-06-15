using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
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

        [Header("해상도 설정")]
        [SerializeField] private TMP_Dropdown resolutionDropdown;

        private Resolution[] resolutions;

        private void Start()
        {
            SetupResolutionOptions();
            SetupAudioSliders();
        }

        // 사운드 탭 표시
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

        //슬라이더 초기값 설정
        private void SetupAudioSliders()
        {
            masterSlider.value = AudioManager.Instance.GetVolume("MasterVolume");
            bgmSlider.value = AudioManager.Instance.GetVolume("BGMVolume");
            sfxSlider.value = AudioManager.Instance.GetVolume("SFXVolume");
        }

        //슬라이더가 바뀌었을 때
        public void SetMasterVolume(float value)
        {
            AudioManager.Instance.SetMasterVolume(value);
        }
        public void SetBGMVolume(float value)
        {
            AudioManager.Instance.SetBGMVolume(value);
        }
        public void SetSFXVolume(float value)
        {
            AudioManager.Instance.SetSFXVolume(value);
        }

        //해상도 설정
        private void SetupResolutionOptions()
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();

            var options = new System.Collections.Generic.List<string>();
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
            resolutionDropdown.value = current;
            resolutionDropdown.RefreshShownValue();
        }

        public void SetResolution(int index)
        {
            Resolution res = resolutions[index];
            Screen.SetResolution(res.width, res.height, FullScreenMode.FullScreenWindow);
        }
    }
}
