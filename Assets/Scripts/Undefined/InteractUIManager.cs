using UnityEngine;
using UnityEngine.UI;

public class InteractUIManager : MonoBehaviour
{
    public static InteractUIManager instance;

    public GameObject interactButton;  // Assign the Button UI here
    private QuestGiver currentInteractable;

    void Awake()
    {
        instance = this;
        interactButton.SetActive(false); // Ensure hidden at start
    }

    public void ShowButton(QuestGiver interactable)
    {
        currentInteractable = interactable;
        interactButton.SetActive(true);
    }

    public void HideButton()
    {
        currentInteractable = null;
        interactButton.SetActive(false);
    }

    // Called by the Button's OnClick
public void OnInteractPressed()
{
    Debug.Log("Interact Button Pressed");

    if (currentInteractable != null)
    {
        Debug.Log("Triggering dialogue on: " + currentInteractable.name);
        currentInteractable.TriggerDialogue();
        HideButton();
    }
    else
    {
        Debug.LogWarning("No interactable assigned on button press!");
    }
}

    
}

