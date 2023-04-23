using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    public string gameScene;

    public void ReStart(){
        SaveManager.ClearSaveGame();
        SceneManager.LoadScene(gameScene, LoadSceneMode.Single);

    }

    public void Quit()
    {
        Application.Quit();
    }
}
