using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    [TextArea] public string description;
    public bool isConsumable;
    public int healthRestore; // If it's a healing potion, etc.
    // Add other item properties as needed
}