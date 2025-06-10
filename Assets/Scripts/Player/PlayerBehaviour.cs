using UnityEngine;
using TMPro;
public class PlayerBehaviour : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public bool isDefending = false;
    public TextMeshProUGUI healthText;


    void Start()
    {
           currentHealth = maxHealth;
            UpdateHealthText();
    }   


    public void TakeDamage(int damage)
    {
        if (isDefending)
        {
            Debug.Log("Player blocked the attack!");
            isDefending = false;
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateHealthText();

        Debug.Log("Player took damage! Current HP: " + currentHealth);
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        UpdateHealthText();

        Debug.Log("Player healed. Current HP: " + currentHealth);
    }

    public void Defend()
    {
        isDefending = true;
        Debug.Log("Player is defending.");
    }
        void UpdateHealthText()
    {
        healthText.text = "Player: " + currentHealth;
    }
}