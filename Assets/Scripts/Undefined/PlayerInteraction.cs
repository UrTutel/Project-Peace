using UnityEngine;

using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRadius = 1.5f;
    public LayerMask interactionLayer;

    void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRadius, interactionLayer);

        bool foundInteractable = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out QuestGiver questGiver))
            {
                InteractUIManager.instance.ShowButton(questGiver);
                foundInteractable = true;
                break;
            }
        }

        if (!foundInteractable)
        {
            InteractUIManager.instance.HideButton();
        }
    }
}
