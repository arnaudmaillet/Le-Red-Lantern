using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    // Public
    [Header("References")]
    public GameScene currentScene;
    public ScriptBoxController scriptBox;
    public SpriteSwitcher background;
    public ChoiceController choiceController;
    public AudioController audioController;

    // Private
    private State state = State.IDLE;
    private List<StoryScene> history = new List<StoryScene>();
    private enum State { IDLE, ANIMATE, CHOICE };

    void Start()
    {
        if (currentScene is StoryScene) {
            StoryScene storyScene = currentScene as StoryScene;
            history.Add(storyScene);
            scriptBox.PlayScene(storyScene);
            background.SetImage(storyScene.background);
            PlayAudio(storyScene.sentences[0]);
        }
    }

    void Update()
    {
        if (state == State.IDLE)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (scriptBox.isCompleted())
                {
                    scriptBox.StopTyping();
                    if (scriptBox.isLastSentence())
                    {
                        PlayScene((currentScene as StoryScene).nextScene);
                    }
                    else
                    {
                        scriptBox.NextSentence();
                        PlayAudio((currentScene as StoryScene).sentences[scriptBox.GetSentenceIndex()]);
                    }
                } else {
                    scriptBox.SpeedUp();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (scriptBox.isFirstSentence())
            {
                if (history.Count > 1)
                {
                    scriptBox.StopTyping();
                    scriptBox.HideSprites();
                    history.RemoveAt(history.Count - 1);
                    StoryScene scene = history[history.Count - 1];
                    history.RemoveAt(history.Count - 1);
                    PlayScene(scene, scene.sentences.Count - 2, false);
                }
            } else {
                scriptBox.GoBack();
            }
        } 
    }

    public void PlayScene(GameScene scene, int sentenceIndex = -1, bool isAnimated = true)
    {
        StartCoroutine(SwitchScene(scene, sentenceIndex, isAnimated));
    }

    private IEnumerator SwitchScene(GameScene scene, int sentenceIndex = -1, bool isAnimated = true)
    {
        state = State.ANIMATE;
        currentScene = scene;
        if (isAnimated)
        {
            scriptBox.Hide();
            yield return new WaitForSeconds(1f);
        }

        if (scene is StoryScene) 
        {
            StoryScene storyScene = scene as StoryScene;
            history.Add(storyScene);
            PlayAudio(storyScene.sentences[sentenceIndex + 1]);
            
            if (isAnimated)
            {
                background.SwitchImage(storyScene.background);
                yield return new WaitForSeconds(1f);
                scriptBox.ClearText();
                scriptBox.Show();
                yield return new WaitForSeconds(1f);
            }
            else 
            {
                background.SetImage(storyScene.background);
                scriptBox.ClearText();
            }
            scriptBox.PlayScene(storyScene, sentenceIndex, isAnimated);
            state = State.IDLE;
        } else if (scene is ChooseScene)
        {
            state = State.CHOICE;
            choiceController.SetupChoice(scene as ChooseScene);
        }
    }

    private void PlayAudio(StoryScene.Sentence sentence)
    {
        audioController.PlayAudio(sentence.music, sentence.sound);
    }
}
