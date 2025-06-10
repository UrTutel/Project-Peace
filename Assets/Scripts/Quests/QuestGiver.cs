using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public static DialogueManager instance;
    public QuestData quest;
    public DialogueData initialDialogue;
    public DialogueData completionDialogue;
    
    [Header("State Tracking")]
    public bool hasGivenQuest = false;
    public bool isReadyForCompletion = false;
    public bool isQuestFinished = false;

    void Update()
    {
        // Only check progress if quest is active but not completed
        if (hasGivenQuest && !isReadyForCompletion && !quest.isCompleted)
        {
            CheckQuestProgress();
        }
    }

    public void TriggerDialogue()
    {
        Debug.Log($"Interacting with {gameObject.name} | Quest State: {GetQuestStatus()}");

        // 1. Quest already completed
        if (quest.isCompleted)
        {
            DialogueManager.instance.StartDialogue(completionDialogue);
            return;
        }

        // 2. Quest ready to turn in
        if (isReadyForCompletion)
        {
            DialogueManager.instance.StartDialogue(
                completionDialogue,
                () => CompleteQuest() // Callback when dialogue finishes
            );
            return;
        }

        // 3. Quest not yet given
        if (!hasGivenQuest)
        {
            DialogueManager.instance.StartDialogue(
                initialDialogue,
                AcceptQuest,
                DeclineQuest
            );
        }
        // 4. Quest in progress
        else
        {
            // Show progress dialogue
            string progress = GetQuestProgress();
            DialogueManager.instance.ShowMessage($"Come back when you've completed: {progress}");
        }
    }

    void AcceptQuest()
    {
        hasGivenQuest = true;
        QuestManager.instance.AcceptQuest(quest);
        Debug.Log($"Quest accepted: {quest.questTitle}");
    }

    void CheckQuestProgress()
    {
        isReadyForCompletion = quest.CheckCompletion();
        
        if (isReadyForCompletion)
        {
            Debug.Log($"Quest ready for completion: {quest.questTitle}");
            // Optional: Visual feedback like NPC exclamation mark
        }
    }
// In QuestGiver.cs:
    void GiveCompletionDialogue()
    {

            
        
    }
    void CompleteQuest()
    {
        if (!isReadyForCompletion) return;
        isQuestFinished = true;
        quest.isCompleted = true;
        QuestManager.instance.CompleteQuest(quest);
        Debug.Log("Quest is finished");
        
        // Give rewards
        if (quest.rewardItem != null)
        {
            InventoryManager.instance.AddItem(quest.rewardItem);
            Debug.Log($"Reward given: {quest.rewardItem.itemName}");
        }
    }

    void DeclineQuest()
    {
        Debug.Log("Quest declined");
    }

    string GetQuestStatus()
    {
        if (quest.isCompleted) return "Completed";
        if (isReadyForCompletion) return "ReadyToComplete";
        if (hasGivenQuest) return "InProgress";
        return "NotStarted";
    }

    string GetQuestProgress()
    {
        // Implement your specific progress string
        // Example for kill quests:
        return $"{quest.currentKills}/{quest.requiredKills} kills";
    }
}