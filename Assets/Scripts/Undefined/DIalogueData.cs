using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] lines;
    public bool hasChoices = true;
}