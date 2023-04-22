using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class StartMenuController : MonoBehaviour
{
    public string GameScene;

    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider soundSlider;
    public TextMeshProUGUI volumeTxt;
    public TextMeshProUGUI soundTxt;
    public AudioMixer musicMixer;
    public AudioMixer soundsMixer;

    private Animator animator;

    private int _window = 0;
    private float percentage = 0;

    private void Start(){
        animator = GetComponent<Animator>();
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", 2);
        SetResolution();
    }

    public void Update(){
        if (Input.GetKeyDown(KeyCode.Escape) && _window == 1){
            animator.SetTrigger("HideOptions");
            _window = 0;
        }
    }

    public void NewGame(){
        SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
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
        musicMixer.SetFloat("volume", Mathf.Log10(value) * 20);
    }

    public void OnSoundsChanged(float value)
    {
        percentage = Mathf.Round(value * 100);
        soundTxt.SetText("Sounds: " + percentage + "%");
        soundsMixer.SetFloat("volume", Mathf.Log10(value) * 20);
    }
}
