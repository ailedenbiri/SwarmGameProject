using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthCO : MonoBehaviour
{
    public float maxHealth = 100f; // Maksimum sa�l�k
    private float currentHealth;

    [SerializeField] private Healthbar healthbar; // Sa�l�k bar�n� referans al
    [SerializeField] private GameObject loseScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth); // Ba�lang��ta sa�l�k bar�n� g�ncelle

        if (loseScreen != null)
        {
            loseScreen.SetActive(false); // Kaybetme ekran�n� ba�lang��ta kapal� yap
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Sa�l�k 0 ile maksimum aras�nda s�n�rlan�r

        healthbar.UpdateHealthBar(maxHealth, currentHealth); // Sa�l�k bar�n� g�ncelle

        Debug.Log($"Can�m {damage} azald�. Kalan can: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Oyuncu �ld�!");
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Oyun durduruldu.");
        Time.timeScale = 0; // Oyunu durdur

        if (loseScreen != null)
        {
            loseScreen.SetActive(true); // Kaybetme ekran�n� g�ster
        }
    }

    // Butona ba�l� olacak bir metod
    public void RetryGame()
    {
        Time.timeScale = 1; // Oyunu tekrar ba�lat
        SceneManager.LoadScene(0); // Ge�erli sahneyi yeniden y�kle
    }
}

