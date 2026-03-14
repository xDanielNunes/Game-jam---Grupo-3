using UnityEngine;
using UnityEngine.UI;

public class PlayerSurvival : MonoBehaviour
{
    [Header("Status")]
    public float maxHunger = 100f;
    public float maxThirst = 100f;

    public float hunger;
    public float thirst;

    [Header("Random Drain Range")]
    public float minDrain = 1f;
    public float maxDrain = 4f;

    private float hungerDrainRate;
    private float thirstDrainRate;

    [Header("Accelerated Drain")]
    public float acceleratedMultiplier = 2f;

    [Header("UI")]
    public Slider hungerBar;
    public Slider thirstBar;

    private PlayerDeath playerDeath;

    private void Start()
    {
        hunger = maxHunger;
        thirst = maxThirst;

        playerDeath = GetComponent<PlayerDeath>();

        hungerBar.maxValue = maxHunger;
        thirstBar.maxValue = maxThirst;

        GenerateRandomDrainRates();
    }

    private void Update()
    {
        HandleDrain();
        UpdateUI();
        CheckDeath();
    }

    // Gera um valor aleatório para as taxas de drenagem de fome e sede dentro do intervalo definido
    private void GenerateRandomDrainRates()
    {
        hungerDrainRate = Random.Range(minDrain, maxDrain);
        thirstDrainRate = Random.Range(minDrain, maxDrain);

        Debug.Log("Hunger Drain: " + hungerDrainRate);
        Debug.Log("Thirst Drain: " + thirstDrainRate);
    }

    // Aplica a drenagem de fome e sede, acelerando a drenagem se um dos status estiver em zero
    private void HandleDrain()
    {
        float currentHungerDrain = hungerDrainRate;
        float currentThirstDrain = thirstDrainRate;

        if (hunger <= 0 && thirst > 0)
            currentThirstDrain *= acceleratedMultiplier;

        if (thirst <= 0 && hunger > 0)
            currentHungerDrain *= acceleratedMultiplier;

        hunger -= currentHungerDrain * Time.deltaTime;
        thirst -= currentThirstDrain * Time.deltaTime;

        hunger = Mathf.Clamp(hunger, 0, maxHunger);
        thirst = Mathf.Clamp(thirst, 0, maxThirst);
    }

    private void UpdateUI()
    {
        hungerBar.value = hunger;
        thirstBar.value = thirst;
    }

    private void CheckDeath()
    {
        if (hunger <= 0 && thirst <= 0)
        {
            playerDeath.Die();
        }
    }

    public void RestoreHunger(float amount)
    {
        hunger += amount;
        hunger = Mathf.Clamp(hunger, 0, maxHunger);
    }

    public void RestoreThirst(float amount)
    {
        thirst += amount;
        thirst = Mathf.Clamp(thirst, 0, maxThirst);
    }
}
