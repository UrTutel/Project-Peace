using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public GameObject startBattleButton;
    public GameObject cardPanel;
    public TextMeshProUGUI monsterHealthText;
    public GameObject defeatPanel;
    public GameObject questCompletePanel; // NEW: Quest completion UI

    public Monster monster;
    public Player player;

    public int attackDamage = 20;
    public int healAmount = 15;

    private bool battleStarted = false;

    void Start()
    {
        monsterHealthText.gameObject.SetActive(false);
        cardPanel.SetActive(false);
        defeatPanel.SetActive(false);
        questCompletePanel.SetActive(false); // NEW: Hide quest complete panel

        startBattleButton.GetComponent<Button>().onClick.AddListener(StartBattle);
    }

    void StartBattle()
    {
        battleStarted = true;
        startBattleButton.SetActive(false);
        monsterHealthText.gameObject.SetActive(true);
        cardPanel.SetActive(true);

        MonsterAttack();
    }

    public void OnAttackCard()
    {
        monster.TakeDamage(attackDamage);
        Debug.Log("Player used Attack card!");
        PostPlayerAction();
    }

    public void OnDefendCard()
    {
        player.Defend();
        Debug.Log("Player used Defend card!");
        PostPlayerAction();
    }

    public void OnHealCard()
    {
        player.Heal(healAmount);
        Debug.Log("Player used Heal card!");
        PostPlayerAction();
    }

    void PostPlayerAction()
    {
        if (monster.currentHealth <= 0)
        {
            Debug.Log("Monster defeated!");
            cardPanel.SetActive(false);
            ShowDefeatPanel();
            CompleteMonsterQuest(); // NEW: Handle quest completion
        }
        else
        {
            MonsterAttack();
        }
    }

    void CompleteMonsterQuest()
    {
        // Check if this monster is part of any active quests
        foreach (var quest in QuestManager.instance.activeQuests)
        {
            if (quest.targetTransform == monster.transform && !quest.isCompleted)
            {
                quest.isCompleted = true;
                ShowQuestCompletePanel(quest);
                
                // If there's completion dialogue, show it
                if (quest.completionDialogue != null)
                {
                    DialogueManager.instance.StartDialogue(quest.completionDialogue, 
                        () => GiveQuestReward(quest));
                }
                else
                {
                    GiveQuestReward(quest);
                }
                break;
            }
        }
    }

    void ShowQuestCompletePanel(QuestData quest)
    {
        questCompletePanel.SetActive(true);
        // You could add more UI elements here to show quest details
    }

    void GiveQuestReward(QuestData quest)
    {
        if (quest.rewardItem != null)
        {
            InventoryManager.instance.AddItem(quest.rewardItem);
        }
        questCompletePanel.SetActive(false);
    }

    void MonsterAttack()
    {
        int damage = monster.GetAttackDamage();
        player.TakeDamage(damage);

        if (player.currentHealth <= 0)
        {
            Debug.Log("Player is defeated!");
            cardPanel.SetActive(false);
        }
    }

    void ShowDefeatPanel()
    {
        defeatPanel.SetActive(true);
    }

    public void OnOkButton()
    {
        defeatPanel.SetActive(false);
        // You might want to destroy the monster here
        if (monster != null && monster.currentHealth <= 0)
        {
            Destroy(monster.gameObject);
        }
    }
}