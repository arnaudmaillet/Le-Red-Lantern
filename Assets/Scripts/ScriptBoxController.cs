using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptBoxController : MonoBehaviour
{

    [Header("References")]
    public TextMeshProUGUI barText;
    public TextMeshProUGUI speakerText;
    public StoryScene currentScene;

    [Header("Settings")]
    public float speedTyping = 0.1f;

    private int sentenceIndex = -1;
    private enum State { PLAYING, COMPLETED };
    private State state = State.COMPLETED;

    public void PlayScene(StoryScene scene)
    {
        currentScene = scene;
        sentenceIndex = -1;
        NextSentence();
    }


    public void NextSentence()
    {
        StartCoroutine(TypeSentence(currentScene.sentences[++sentenceIndex].text));
        speakerText.text = currentScene.sentences[sentenceIndex].speaker.speakerName;
        speakerText.color = currentScene.sentences[sentenceIndex].speaker.textColor;

    }
    

    void Update()
    {
        
    }

    public bool isCompleted()
    {
        return state == State.COMPLETED;
    }

    public bool isLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    private IEnumerator TypeSentence(string sentence)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;
        while (state != State.COMPLETED)
        {
            barText.text += sentence[wordIndex];
            yield return new WaitForSeconds(speedTyping);
            if (++wordIndex >= sentence.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
