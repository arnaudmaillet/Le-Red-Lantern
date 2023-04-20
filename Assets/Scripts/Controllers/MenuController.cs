using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string GameScene;

    public void NewGame(){
        SceneManager.LoadScene(GameScene, LoadSceneMode.Single);
    }

    public void LoadGame(){

    }

    public void ExitGame(){
        Application.Quit();
    }

    public void Options(){

    }
}
