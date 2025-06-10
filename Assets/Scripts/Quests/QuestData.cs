using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest Data")]
public class QuestData : ScriptableObject
{
    public Transform targetTransform;
    [Header("Basic Info")]
    public string questID;
    public string questTitle;
    [TextArea] public string description;
    public bool isCompleted;

    [Header("Objectives")]
    public int requiredKills;
    public string targetEnemyID;
    [System.NonSerialized] public int currentKills;

    [Header("Rewards")]
    public ItemData rewardItem;
    public DialogueData completionDialogue;

    public bool CheckCompletion()
    {
        // For kill quests
        if (currentKills >= requiredKills)
        {
            return true;
        }
        return false;
    }

    // Reset temporary data when game starts
    public void Reset()
    {
        isCompleted = false;
        currentKills = 0;
    }
}