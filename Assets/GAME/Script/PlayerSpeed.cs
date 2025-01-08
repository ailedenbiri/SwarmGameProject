using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{

    public GameObject speedFX;      // Karakterin altýnda gösterilecek dash VFX

    public float speedIncrease = 5f;   // Hýz artýþ miktarý
    public float speedDuration = 5f;   // Hýz artýþ süresi
    private float originalSpeed;       // Karakterin orijinal hýzý
    private bool isSpeedBoostActive = false; // Hýz artýþý aktif mi kontrolü

    private void Start()
    {
        // Orijinal hýzý belirlemek için varsayýlan deðer
        originalSpeed = GetComponent<NinjaMovementCO>().moveSpeed;
        speedFX.SetActive(false); // Oyun baþlarken kapalý
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost")) // Kutunun "SpeedBoost" tag'ý olduðundan emin ol
        {
            ActivateSpeedBoost();
            Destroy(other.gameObject); // Kutuyu yok et
        }
    }

    private void ActivateSpeedBoost()
    {
        if (!isSpeedBoostActive)
        {
            isSpeedBoostActive = true;
            GetComponent<NinjaMovementCO>().moveSpeed += speedIncrease; // Hýzý artýr
            speedFX.SetActive(true); // VFX'i aktif et

            // Hýz artýþýný zamanlayýcý ile geri döndür
            Invoke("DeactivateSpeedBoost", speedDuration);
        }
    }

    private void DeactivateSpeedBoost()
    {
        GetComponent<NinjaMovementCO>().moveSpeed = originalSpeed; // Hýzý orijinal haline getir
        speedFX.SetActive(false); // Hýz artýþý VFX'ini devre dýþý býrak
        isSpeedBoostActive = false;
    }
}

