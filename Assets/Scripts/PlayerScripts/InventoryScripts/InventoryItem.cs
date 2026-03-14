using UnityEngine;
public enum ItemType
{
    Food,
    Water
}

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public ItemType type;
    public float restoreValue;
    public Sprite icon;

    public InventoryItem(string name, ItemType type, float value, Sprite sprite)
    {
        this.itemName = name;
        this.type = type;
        this.restoreValue = value;
        this.icon = sprite;
    }
}
