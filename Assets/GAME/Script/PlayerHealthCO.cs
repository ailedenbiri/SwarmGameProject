using UnityEngine;

public class PlayerHealthCO : MonoBehaviour
{
    public float maxHealth = 100f; // Maksimum saðlýk
    private float currentHealth;

    [SerializeField] private Healthbar healthbar; // Saðlýk barýný referans al

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth); // Baþlangýçta saðlýk barýný güncelle
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
            // Oyuncunun ölme durumunu burada iþleyebilirsin
        }
    }
}
