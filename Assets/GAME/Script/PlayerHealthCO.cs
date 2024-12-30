using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthCO : MonoBehaviour
{
    public float maxHealth = 100f; // Maksimum saðlýk
    private float currentHealth;

    [SerializeField] private Healthbar healthbar; // Saðlýk barýný referans al
    [SerializeField] private GameObject loseScreen;

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth); // Baþlangýçta saðlýk barýný güncelle

        if (loseScreen != null)
        {
            loseScreen.SetActive(false); // Kaybetme ekranýný baþlangýçta kapalý yap
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Saðlýk 0 ile maksimum arasýnda sýnýrlanýr

        healthbar.UpdateHealthBar(maxHealth, currentHealth); // Saðlýk barýný güncelle

        Debug.Log($"Caným {damage} azaldý. Kalan can: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Oyuncu öldü!");
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Oyun durduruldu.");
        Time.timeScale = 0; // Oyunu durdur

        if (loseScreen != null)
        {
            loseScreen.SetActive(true); // Kaybetme ekranýný göster
        }
    }

    // Butona baðlý olacak bir metod
    public void RetryGame()
    {
        Time.timeScale = 1; // Oyunu tekrar baþlat
        SceneManager.LoadScene(0); // Geçerli sahneyi yeniden yükle
    }
}

