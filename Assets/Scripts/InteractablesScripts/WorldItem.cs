using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public string itemName;
    public ItemType itemType;
    public float restoreValue;
    public Sprite sprite;

    public InventoryItem ToInventoryItem()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        if (sprite == null)
            Debug.LogWarning("Sprite não encontrado para " + itemName);
        return new InventoryItem(itemName, itemType, restoreValue, sprite);
    }
}
