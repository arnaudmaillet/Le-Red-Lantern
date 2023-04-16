using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("References")]
    public StoryScene currentScene;
    public ScriptBoxController scriptBox;
    public BackgroundController background;

    void Start()
    {
        scriptBox.PlayScene(currentScene);
        background.setImage(currentScene.background);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (scriptBox.isCompleted())
            {
                if (scriptBox.isLastSentence())
                {
                    currentScene = currentScene.nextScene;
                    scriptBox.PlayScene(currentScene);
                    background.switchBackground(currentScene.background);
                }
                else
                {
                    scriptBox.NextSentence();
                }
            }
        }
    }
}
