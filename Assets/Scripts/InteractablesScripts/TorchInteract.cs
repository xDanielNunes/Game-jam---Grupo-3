using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TorchInteract : MonoBehaviour
{
    public Light2D torchLight;
    private bool isLit;

    private void Awake()
    {
        torchLight.enabled = false;
        isLit = false;
    }
    
    
    public void LightTorch()
    {
        if (isLit) return;

        isLit = true;
        torchLight.enabled = true;
    }
}

