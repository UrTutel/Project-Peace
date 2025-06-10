using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    public List<QuestData> activeQuests = new List<QuestData>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AcceptQuest(QuestData quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            Debug.Log("Accepted quest: " + quest.questTitle);
        }
    }

    public void CompleteQuest(QuestData quest)
    {
        if (activeQuests.Contains(quest) && !quest.isCompleted)
        {
            quest.isCompleted = true;
            Debug.Log("Completed quest: " + quest.questTitle);
            
            if (quest.completionDialogue != null)
            {
                // Pass the quest as parameter to the reward callback
                DialogueManager.instance.StartDialogue(
                    quest.completionDialogue, 
                    () => GiveReward(quest) // Lambda to pass the quest
                );
            }
            else
            {
                GiveReward(quest);
            }
        }
    }

    private void GiveReward(QuestData quest)
    {
        if (quest.rewardItem != null)
        {
            InventoryManager.instance.AddItem(quest.rewardItem);
            Debug.Log("Received reward: " + quest.rewardItem.itemName);
            
            // You might want to show a reward popup here
            // RewardPopup.Show(quest.rewardItem);
        }
    }
}