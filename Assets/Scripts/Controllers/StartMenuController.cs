using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    public string gameScene;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider soundSlider;
    public TextMeshProUGUI volumeTxt;
    public TextMeshProUGUI soundTxt;
    public AudioMixer musicMixer;
    public AudioMixer soundsMixer;

    public Button loadButton;

    private Animator animator;

    private int _window = 0;
    private float percentage = 0;

    private void Start(){
        animator = GetComponent<Animator>();

        // affiche le volume de musicMixer
        float volume = 0;
        musicMixer.GetFloat("volume", out volume);
        volumeSlider.value = Mathf.Pow(10, volume / 20);
        OnMusicChanged(volumeSlider.value);

        // affiche le volume de soundsMixer
        float sound = 0;
        soundsMixer.GetFloat("volume", out sound);
        soundSlider.value = Mathf.Pow(10, sound / 20);
        OnSoundsChanged(soundSlider.value);

        // affiche la résolution
        resolutionDropdown.value = PlayerPrefs.GetInt("resolution", 2);
        SetResolution();

        loadButton.interactable = SaveManager.IsGameSaved();
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) && _window == 1){
            animator.SetTrigger("HideOptions");
            _window = 0;
        }
    }

    public void NewGame(){
        SaveManager.ClearSaveGame();
        Load();
    }

    public void Load(){
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);
    }

    public void ExitGame(){
        Application.Quit();
    }

    public void ShowOptions(){
        animator.SetTrigger("ShowOptions");
        _window = 1;
    }

    public void HideOptions(){
        animator.SetTrigger("HideOptions");
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
        percentage = Mathf.Round(value * 100);
        volumeTxt.SetText("Volume: " + percentage + "%");
        if (value == 0){
            musicMixer.SetFloat("volume", -80);
            volumeTxt.SetText("Volume: Mute");
        }
        else
            musicMixer.SetFloat("volume", Mathf.Log10(value) * 20);
    }

    public void OnSoundsChanged(float value)
    {
        percentage = Mathf.Round(value * 100);
        soundTxt.SetText("Sounds: " + percentage + "%");
        if (value == 0){
            soundsMixer.SetFloat("volume", -80);
            soundTxt.SetText("Sounds: Mute");
        }
        else
        soundsMixer.SetFloat("volume", Mathf.Log10(value) * 20);
    }
}
