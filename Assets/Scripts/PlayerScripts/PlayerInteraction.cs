using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private TorchInteract currentTorch;
    private Inventory inventory;
    private WorldItem currentItem;
    private PlayerInputHandler input;

    void Awake()
    {
        input = GetComponent<PlayerInputHandler>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        if (input.InteractPressed)
        {
            // Tocha
            if (currentTorch != null)
            {
                currentTorch.LightTorch();
            }

            // Item
            if (currentItem != null)
            {
                inventory.AddItem(currentItem.ToInventoryItem());
                Destroy(currentItem.gameObject);
                currentItem = null;
            }
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detecta quando o jogador entra na área de interação da tocha e armazena a referência para a tocha atual
        TorchInteract torch = other.GetComponent<TorchInteract>();

        if (torch != null)
        {
            currentTorch = torch;
        }

        //////////////////////////////////////////////////////////////
        /// 
        /// Aqui posso adicionar outras detecções de colisão para diferentes tipos de interações.
        /// 
        
        WorldItem item = other.GetComponent<WorldItem>();

        if (item != null)
        {
            currentItem = item;
        }
    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TorchInteract torch = other.GetComponent<TorchInteract>();

        if (torch != null && torch == currentTorch)
        {
            currentTorch = null;
        }

        WorldItem item = other.GetComponent<WorldItem>();

        if (item != null && item == currentItem)
        {
            currentItem = null;
        }
    }
}
