using UnityEngine;

[CreateAssetMenu(fileName = "Speaker", menuName = "Data/Speaker")]
[System.Serializable]
public class Speaker : ScriptableObject
{
    public string speakerName;
    public Color textColor;
}
