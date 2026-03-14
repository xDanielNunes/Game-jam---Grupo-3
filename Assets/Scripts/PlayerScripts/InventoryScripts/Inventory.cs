using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> items = new List<InventoryItem>();
    public int capacity = 5;

    // Evento para notificar mudanças no inventário
    public event Action OnInventoryChanged;

    private PlayerSurvival survival;

    private void Awake()
    {
        survival = GetComponent<PlayerSurvival>();
    }

    public void AddItem(InventoryItem item)
    {
        
        if (items.Count >= capacity)
        {
            Debug.Log("Inventário cheio!");
            return;
        }

        items.Add(item);

        // Notifica a UI para atualizar
        OnInventoryChanged?.Invoke();
        Debug.Log(item.itemName + " foi adicionado.");

    }

    public void ConsumeItem(int index)
    {
        if (index < 0 || index >= items.Count)
            return;

        InventoryItem item = items[index];

        switch (item.type)
        {
            case ItemType.Food:
                survival.RestoreHunger(item.restoreValue);
                break;

            case ItemType.Water:
                survival.RestoreThirst(item.restoreValue);
                break;
        }

        Debug.Log(item.itemName + " consumido.");
        items.RemoveAt(index);
        
        // Notifica a UI para atualizar
        OnInventoryChanged?.Invoke();
    }


}


