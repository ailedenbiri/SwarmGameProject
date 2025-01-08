using UnityEngine;

public class BulletCO : MonoBehaviour
{
    public GameObject smokePrefab;
    private void Start()
    {
      transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 smokePosition = other.transform.position;
            smokePosition.y += 5;

            Instantiate(smokePrefab, other.transform.position, Quaternion.identity);

            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
