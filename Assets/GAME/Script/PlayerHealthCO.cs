using UnityEngine;

public class PlayerHealthCO : MonoBehaviour
{
    public float maxHealth = 100f; // Maksimum sa�l�k
    private float currentHealth;

    [SerializeField] private Healthbar healthbar; // Sa�l�k bar�n� referans al

    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.UpdateHealthBar(maxHealth, currentHealth); // Ba�lang��ta sa�l�k bar�n� g�ncelle
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
            // Oyuncunun �lme durumunu burada i�leyebilirsin
        }
    }
}
