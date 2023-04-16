using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StoryScene", menuName = "Data/StoryScene")]
[System.Serializable]
public class StoryScene : ScriptableObject
{
    [Header("References")]
    public Sprite background;
    public StoryScene nextScene;
    public List<Sentence> sentences;

    [System.Serializable]
    public struct Sentence
    {
        public Speaker speaker;
        public string text;
    }
}
