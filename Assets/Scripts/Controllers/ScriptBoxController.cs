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
    public AudioSource voicePlayer;
    public StoryScene currentScene;
    public Dictionary<Speaker, SpriteController> sprites;
    public GameObject spritePrefab;

    // Private
    private int sentenceIndex = -1;
    private enum State { PLAYING, SPEEDED_UP, COMPLETED };
    private State state = State.COMPLETED;
    private Animator animator;
    private bool isHidden = false;
    private Coroutine typingCoroutine;
    private float speedFactor = 1f;

    void Awake()
    {
        sprites = new Dictionary<Speaker, SpriteController>();
        animator = GetComponent<Animator>();
    }

    public int GetSentenceIndex()
    {
        return sentenceIndex;
    }

    public void SetSentenceIndex(int sentenceIndex)
    {
        this.sentenceIndex = sentenceIndex;
    }

    public void PlayScene(StoryScene scene, int sentenceIndex = -1, bool isAnimated = true)
    {
        currentScene = scene;
        this.sentenceIndex = sentenceIndex;
        NextSentence(isAnimated);
    }

    public void NextSentence(bool isAnimated = true)
    {
        sentenceIndex++;
        PlaySentence(isAnimated);
    }

    public void GoBack()
    {
        sentenceIndex--;
        StopTyping();
        HideSprites();
        PlaySentence(false);
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
        speakerText.text = "";
    }

    public bool isCompleted()
    {
        return state == State.COMPLETED || state == State.SPEEDED_UP;
    }

    public bool isLastSentence()
    {
        return sentenceIndex + 1 == currentScene.sentences.Count;
    }

    public bool isFirstSentence()
    {
        return sentenceIndex == 0;
    }

    public void SpeedUp()
    {
        state = State.SPEEDED_UP;
        speedFactor = 0.25f;
    }

    public void StopTyping()
    {
        state = State.COMPLETED;
        StopCoroutine(typingCoroutine);
    }

    public void HideSprites()
    {
        while (spritePrefab.transform.childCount > 0)
        {
            DestroyImmediate(spritePrefab.transform.GetChild(0).gameObject);
        }
        sprites.Clear();
    }

    private void PlaySentence(bool isAnimated = true)
    {
        StoryScene.Sentence sentence = currentScene.sentences[sentenceIndex];
        speedFactor = 1f;
        typingCoroutine = StartCoroutine(TypeSentence(sentence.text));
        speakerText.text = sentence.speaker.speakerName;
        speakerText.color = sentence.speaker.textColor;

        if (sentence.audio)
        {
            voicePlayer.clip = sentence.audio;
            voicePlayer.Play();
        }
        else
        {
            voicePlayer.Stop();
        }
        ActSpeakers(isAnimated);
    }

    private IEnumerator TypeSentence(string sentence)
    {
        barText.text = "";
        state = State.PLAYING;
        int wordIndex = 0;
        
        while (state != State.COMPLETED)
        {
            barText.text += sentence[wordIndex];
            yield return new WaitForSeconds(speedFactor * 0.05f);
            if (++wordIndex == sentence.Length)
            {
                state = State.COMPLETED;
                break;
            }
        }
    }

    private void ActSpeakers(bool isAnimated = true)
    {
        List<StoryScene.Sentence.Action> actions = currentScene.sentences[sentenceIndex].actions;
        for (int i = 0; i < actions.Count; i++)
        {
            ActSpeaker(actions[i], isAnimated);
        }
    }

    private void ActSpeaker(StoryScene.Sentence.Action action, bool isAnimated = true)
    {
        SpriteController controller;
        if (!sprites.ContainsKey(action.speaker))
        {
            controller = Instantiate(action.speaker.prefab.gameObject, spritePrefab.transform).GetComponent<SpriteController>();
            sprites.Add(action.speaker, controller);
        }
        else
        {
            controller = sprites[action.speaker];
        }
        switch (action.actionType)
        {
            case StoryScene.Sentence.Action.Type.APPEAR:
                controller.Setup(action.speaker.sprites[action.spriteIndex]);
                controller.Show(action.coords, isAnimated);
                return;
            case StoryScene.Sentence.Action.Type.MOVE:
                controller.Move(action.coords, action.moveSpeed, isAnimated);
                break;
            case StoryScene.Sentence.Action.Type.DISAPPEAR:
                controller.Hide(isAnimated);
                break;
        }
        controller.SwitchSprite(action.speaker.sprites[action.spriteIndex], isAnimated);
    }
}
