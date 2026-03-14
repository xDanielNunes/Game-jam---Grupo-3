using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotContainer;

    private Inventory inventory;
    private PlayerInputHandler input;

    private List<GameObject> slots = new List<GameObject>();

    private int selectedIndex = 0;
    public bool isOpen = false;

    private void Awake()
    {
        inventory = Object.FindFirstObjectByType<Inventory>();
        input = Object.FindFirstObjectByType<PlayerInputHandler>();
    }

    private void Start()
    {
        // Atualiza a UI inicialmente e se inscreve no evento de mudança do inventário
        RefreshUI();
        inventory.OnInventoryChanged += RefreshUI;
    }

    private void Update()
    {
        if (input == null) return;

        //RefreshUI();

        if (input.IsToggleInventoryPressed())
        {
            ToggleInventory();
        }

        if (!isOpen) return;

        HandleNavigation();
        HandleConsume();
    }

    void ToggleInventory()
    {
        isOpen = !isOpen;
        UpdateSelection();
    }

    void RefreshUI()
    {
        foreach (var slot in slots)
            Destroy(slot);

        slots.Clear();

        
        for (int i = 0; i < inventory.capacity; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);

            // Pega o componente de imagem do slot para atualizar o ícone
            Image iconImage = slot.transform.Find("Icon").GetComponent<Image>();

            if (iconImage == null)
            {
                Debug.LogError("Icon não encontrado no slot!");
            }

            if (i < inventory.items.Count)
            {
                // Extrai o sprite do item e atribui à imagem do slot
                var sprite = inventory.items[i].icon;

                Debug.Log("Sprite no inventário: " + sprite);

                iconImage.sprite = sprite;
                iconImage.enabled = true;
            }
            else
            {
                iconImage.enabled = false;
            }

            slots.Add(slot);
        }

        selectedIndex = Mathf.Clamp(selectedIndex, 0, slots.Count - 1);
        UpdateSelection();
    }
    

    private float navigationCooldown = 0.2f;
    private float navigationTimer = 0f;

    void HandleNavigation()
    {
        navigationTimer -= Time.deltaTime;

        if (navigationTimer > 0f)
            return;

        if (input.MoveInput.y > 0.5f)
        {
            selectedIndex--;
            navigationTimer = navigationCooldown;
        }
        else if (input.MoveInput.y < -0.5f)
        {
            selectedIndex++;
            navigationTimer = navigationCooldown;
        }

        selectedIndex = Mathf.Clamp(selectedIndex, 0, slots.Count - 1);
        UpdateSelection();
    }

    void UpdateSelection()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Outline outline = slots[i].GetComponent<Outline>();
            outline.enabled = isOpen && i == selectedIndex;
        }
    }

    void HandleConsume()
    {
        if (input.InteractPressed &&
            selectedIndex < inventory.items.Count)
        {
            inventory.ConsumeItem(selectedIndex);
            RefreshUI();
        }
    }
}
