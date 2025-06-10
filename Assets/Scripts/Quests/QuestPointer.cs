using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    public Transform player;
    public Transform target;

    public RectTransform arrowUI;
    public float rotationOffset = 0f;

    void Update()
    {
        if (target == null || player == null) return;

        // Get direction
        Vector2 direction = target.position - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrowUI.rotation = Quaternion.Euler(0, 0, angle + rotationOffset);
    }

public void SetTarget(Transform newTarget)
{
    target = newTarget;
    gameObject.SetActive(true);
    Debug.Log("Quest Arrow: Target set to " + newTarget.name);
}


    public void ClearTarget()
    {
        target = null;
        gameObject.SetActive(false);
    }
}
