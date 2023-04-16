using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScriptBoxController : MonoBehaviour
{

    // Public
    [Header("References")]
    public TextMeshProUGUI barText;
    public TextMeshProUGUI speakerText;
    public StoryScene currentScene;

    // Private
    private int sentenceIndex = -1;
    private enum State { PLAYING, COMPLETED };
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

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

    public void Hide()
    {
        if (!isHidden) {
            animator.SetTrigger("Hide");
            isHidden = true;
        }
    }

    public void Show()
    {
        animator.SetTrigger("Show");
        isHidden = false;
    }

    public void ClearText()
    {
        barText.text = "";
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
            yield return new WaitForSeconds(0.05f);
            if (++wordIndex >= sentence.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }
}
