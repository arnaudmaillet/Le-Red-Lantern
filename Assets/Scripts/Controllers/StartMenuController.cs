using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    public string GameScene;

    private void Start(){
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.5f);
    }

    public void NewGame(){
        SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
    }

    public void LoadGame(){

    }

    public void ExitGame(){
        Application.Quit();
    }
}
