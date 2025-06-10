// Assets/Scripts/MonsterBehavior.cs
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public enum MovementType { Horizontal, Vertical, Combined }
    public MovementType movementType = MovementType.Horizontal; // Movement mode
    public float moveDistance = 2f; // Distance for movement
    public float moveSpeed = 2f; // Speed of movement
    public MapBoundaries mapBoundaries; // Reference to map boundaries
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        if (mapBoundaries == null)
        {
            Debug.LogError("MapBoundaries not assigned in MonsterBehavior!");
        }
    }

    void Update()
    {
        if (mapBoundaries == null) return;

        Vector3 targetPosition = startPosition;

        switch (movementType)
        {
            case MovementType.Horizontal:
                float hOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance) - moveDistance / 2f;
                targetPosition += new Vector3(hOffset, 0, 0);
                break;

            case MovementType.Vertical:
                float vOffset = Mathf.PingPong(Time.time * moveSpeed, moveDistance) - moveDistance / 2f;
                targetPosition += new Vector3(0, vOffset, 0);
                break;

            case MovementType.Combined:
                // Elliptical movement: horizontal and vertical oscillation
                float xOffset = Mathf.Sin(Time.time * moveSpeed) * (moveDistance / 2f);
                float yOffset = Mathf.Cos(Time.time * moveSpeed) * (moveDistance / 2f);
                targetPosition += new Vector3(xOffset, yOffset, 0);
                break;
        }

        // Clamp position to map boundaries
        targetPosition = mapBoundaries.ClampPosition(targetPosition);
        transform.position = targetPosition;
    }


}
