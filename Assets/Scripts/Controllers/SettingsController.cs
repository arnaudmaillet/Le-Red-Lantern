using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider volumeSlider;
    public TextMeshProUGUI volumeTxt;
    public AudioMixer musicMixer;

    private void Start()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 2);
        volumeTxt.SetText("Volume: 50%");
        musicMixer.SetFloat("volume", 0.5f);
        SetResolution();
    }

    public void SetResolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0:
                Screen.SetResolution(3840, 2160, true);
                break;
            case 1:
                Screen.SetResolution(2560, 1440, true);
                break;
            case 2:
                Screen.SetResolution(1920, 1080, true);
                break;
            case 3:
                Screen.SetResolution(1280, 720, true);
                break;
        }
    }

    public void OnMusicChanged(float value)
    {
        // convert the value to a percentage
        volumeTxt.SetText("Volume: " + value + "%");
        musicMixer.SetFloat("volume", -50 + value / 2);
    }
}
