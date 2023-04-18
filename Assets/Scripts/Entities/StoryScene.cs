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
        public List<Action> actions;
        [System.Serializable]
        public struct Action
        {
            public Speaker speaker;
            public int spriteIndex;
            public Type actionType;
            public Vector2 coords;
            public float moveSpeed;

            [System.Serializable]
            public enum Type { NONE, APPEAR, MOVE, DISAPPEAR };
        }
    }
}

public class GameScene : ScriptableObject { }