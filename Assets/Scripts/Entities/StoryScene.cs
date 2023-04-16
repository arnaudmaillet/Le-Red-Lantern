using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryScene", menuName = "Data/StoryScene")]
[System.Serializable]
public class StoryScene : GameScene
{

    // Public
    [Header("References")]
    public Sprite background;
    public GameScene nextScene;
    public List<Sentence> sentences;

    [System.Serializable]
    public struct Sentence
    {
        public Speaker speaker;
        public string text;
    }
}

public class GameScene : ScriptableObject
{
}