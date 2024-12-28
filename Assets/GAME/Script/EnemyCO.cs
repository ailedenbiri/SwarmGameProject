using UnityEngine;

public class EnemyCO : MonoBehaviour
{
    public Transform player; // Oyuncunun Transform bileþeni
    public float rotationSpeed = 5f; // Dönüþ hýzý
    public float stoppingDistance = 3f; // Düþmanýn durma mesafesi (oyuncuya bu mesafede duracak)
    public float moveSpeed = 3f;
    public float minDistanceBetweenEnemies = 1f;    

    private void Awake()
    {
        // Oyuncuyu bul ve referans al
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        RotateTowardsPlayer();  
        ChasePlayer();          
        AvoidOtherEnemies();
    }

    void RotateTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // Y ekseninde dönüþü engelle (sadece yatay düzlemde bak)

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
            // Oyuncu ile düþman arasýndaki mesafeyi hesapla
            float distance = Vector3.Distance(transform.position, player.position);

            // Düþman, belirli bir mesafeye kadar oyuncuya yaklaþacak
            if (distance > stoppingDistance)
            {
                Vector3 moveDirection = (player.position - transform.position).normalized;
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }
        }
    }

    void AvoidOtherEnemies()
    {
        // Ayný etikete sahip tüm düþmanlarý bul
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            if (enemy != this.gameObject) // Kendisi hariç
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);

                if (distance < minDistanceBetweenEnemies)
                {
                    // Uzaklaþma yönünü hesapla
                    Vector3 fleeDirection = (transform.position - enemy.transform.position).normalized;
                    transform.position += fleeDirection * moveSpeed * Time.deltaTime;
                }
            }
        }
    }


}
