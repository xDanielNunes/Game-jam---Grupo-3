using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public void Die()
    {
        Debug.Log("Executando morte...");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
