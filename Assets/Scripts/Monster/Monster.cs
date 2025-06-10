// Monster.cs
using UnityEngine;
using TMPro;

public class Monster : MonoBehaviour
{
    public int maxHealth = 80;
    public int currentHealth;
    public int baseDamage = 10;
    public TextMeshProUGUI healthText;


    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthText();
    }

    public int GetAttackDamage()
    {
        float multiplier = Random.Range(0.8f, 1.5f);
        int damage = Mathf.RoundToInt(baseDamage * multiplier);
        Debug.Log("Monster attacks for " + damage + " damage!");
        return damage;
    }

public void TakeDamage(int damage)
{
    currentHealth = Mathf.Max(currentHealth - damage, 0);
    UpdateHealthText();

    if (currentHealth <= 0)
    {
        Die();
    }
}

void Die()
{
        foreach (var quest in QuestManager.instance.activeQuests)
        {

                QuestManager.instance.CompleteQuest(quest);
            
        }
        
        Destroy(gameObject);

}
        void UpdateHealthText()
    {
        healthText.text = "Monster HP: " + currentHealth;
    }
}
