using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChooseScene", menuName = "Data/ChooseScene")]
[System.Serializable]
public class ChooseScene : GameScene
{
    public List<ChooseLabel> labels;
    [System.Serializable]
    public struct ChooseLabel
    {
        public string label;
        public StoryScene nextScene;
        public bool fade;
        public float vampireValue;
        public float pirateValue;
        public float copsValue;
    }
}
