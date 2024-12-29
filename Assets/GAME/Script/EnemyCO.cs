using System.Collections.Generic;
using UnityEngine;

public class EnemyCO : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileþeni
    public float rotationSpeed = 5f; // Dönüþ hýzý
    public float stoppingDistance = 3f; // Düþmanýn durma mesafesi (oyuncuya bu mesafede duracak)
    public float moveSpeed = 3f;
    public float minDistanceBetweenEnemies = 1f;

    private PlayerHealthCO playerHealthCO; // Oyuncunun saðlýk scriptine referans
    public float damageDistance = 2f; // Düþmanýn zarar vereceði mesafe
    public int damageAmount = 3;
    public float damageCooldown = 2f; // Hasar verme arasýndaki süre (saniye)

    private Dictionary<PlayerHealthCO, float> cooldowns = new Dictionary<PlayerHealthCO, float>(); // Her oyuncu için soðuma süreleri

    private void Awake()
    {
        // Oyuncuyu bul ve referans al
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerHealthCO = playerObj.GetComponent<PlayerHealthCO>();
        }
    }

    void Update()
    {
        RotateTowardsPlayer();
        ChasePlayer();
        AvoidOtherEnemies();
        DamagePlayerIfClose();
    }

    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Y ekseninde dönüþü engelle

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > stoppingDistance)
            {
                Vector3 moveDirection = (player.position - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }
    }

    void DamagePlayerIfClose()
    {
        if (player != null && playerHealthCO != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance < damageDistance)
            {
                if (!cooldowns.ContainsKey(playerHealthCO))
                {
                    cooldowns[playerHealthCO] = 0f;
                }

                if (Time.time >= cooldowns[playerHealthCO])
                {
                    playerHealthCO.TakeDamage(damageAmount);
                    cooldowns[playerHealthCO] = Time.time + damageCooldown; // Yeni hasar verme zamanýný belirle
                    Debug.Log($"Düþman oyuncuya {damageAmount} hasar verdi.");
                }
            }
        }
    }

    void AvoidOtherEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != this.gameObject)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < minDistanceBetweenEnemies)
                {
                    Vector3 fleeDirection = (transform.position - enemy.transform.position).normalized;
                    transform.position += fleeDirection * moveSpeed * Time.deltaTime;
                }
            }
        }
    }
}



