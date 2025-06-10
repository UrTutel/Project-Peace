using UnityEngine;

public class MonsterTrigger : MonoBehaviour
{
    private GameObject startBattleButton;

    void Start()
    {
        // Finds the button ONLY ONCE when monster spawns
        startBattleButton = GameObject.FindGameObjectWithTag("StartButton");
        
        if (startBattleButton == null)
            Debug.LogError("Assign the 'StartButton' tag to your battle button!");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && startBattleButton != null)
            startBattleButton.SetActive(true);
        Debug.Log("Player is near a monster");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && startBattleButton != null)
            startBattleButton.SetActive(false);
                    Debug.Log("Player is move out from a monster");

    }
}