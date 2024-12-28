using UnityEngine;

public class BulletCO : MonoBehaviour
{
    private void Start()
    {
      transform.rotation = Quaternion.Euler(90, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
