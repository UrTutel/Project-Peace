using UnityEngine;

using System.Collections.Generic;
using System.Linq;      
    
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<ItemData> items = new List<ItemData>();

    // Event for UI updates
    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChanged;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log("Added item: " + item.itemName);

        // Notify UI to update
        onInventoryChanged?.Invoke();
    }

    
}