using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    public Dictionary<int, int> InventorySlots;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InventorySlots = new Dictionary<int, int>();
    }

    public void AddItem(int id, int amount) 
    {
        if (InventorySlots.ContainsKey(id)) 
        {
            InventorySlots[id] += amount;
            Mathf.Clamp(amount, 0, 999);
        }
        else 
        {
            InventorySlots.Add(id, amount);
        }
    }
}
