using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    // Public
    [Header("References")]
    public GameScene currentScene;
    public ScriptBoxController scriptBox;
    public SpriteSwitcher background;
    public ChoiceController choiceController;
    public AudioController audioController;

    public DataHolder data;

    public string menuScene;

    // Private
    private State state = State.IDLE;
    private List<StoryScene> history = new List<StoryScene>();
    private enum State { IDLE, ANIMATE, CHOICE };

    private ProgressBarController progressBarController;

    void Start()
    {
        // ----------------- test progressBar
        progressBarController = FindObjectOfType<ProgressBarController>();
        progressBarController.AddFillAmount(0.4f, 1);
        progressBarController.AddFillAmount(0.1f, 2);
        Debug.Log("Vampire: " + progressBarController.fillAmount[0]);
        Debug.Log("Police: " + progressBarController.fillAmount[1]);
        Debug.Log("Pirate: " + progressBarController.fillAmount[2]);
        progressBarController.RemoveFillAmount(0.1f, 1);
        Debug.Log("Police: " + progressBarController.fillAmount[1]);
        // ----------------- test progressBar


        if (SaveManager.IsGameSaved())
        {
            SaveData data = SaveManager.LoadGame();
            data.prevScenes.ForEach(scene => {
                history.Add(this.data.scenes[scene] as StoryScene);
            });
            currentScene = history[history.Count - 1];
            history.RemoveAt(history.Count - 1);
            scriptBox.SetSentenceIndex(data.sentence - 1);
        }

        if (currentScene is StoryScene) {
            StoryScene storyScene = currentScene as StoryScene;
            history.Add(storyScene);
            scriptBox.PlayScene(storyScene, scriptBox.GetSentenceIndex());
            background.SetImage(storyScene.background);
            PlayAudio(storyScene.sentences[scriptBox.GetSentenceIndex()]);
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
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                List<int> historyIndicies = new List<int>();
                history.ForEach(scene => historyIndicies.Add(this.data.scenes.IndexOf(scene)));
                SaveData data = new SaveData{
                    sentence = scriptBox.GetSentenceIndex(),
                    prevScenes = historyIndicies
                };
                SaveManager.SaveGame(data);
                SceneManager.LoadScene(menuScene);
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
            }
            else 
            {
                background.SetImage(storyScene.background);
            }
            
            yield return new WaitForSeconds(1f);
            scriptBox.ClearText();
            scriptBox.Show();
            yield return new WaitForSeconds(1f);

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
