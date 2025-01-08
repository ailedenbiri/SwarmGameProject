using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{

    public GameObject speedFX;      // Karakterin alt�nda g�sterilecek dash VFX

    public float speedIncrease = 5f;   // H�z art�� miktar�
    public float speedDuration = 5f;   // H�z art�� s�resi
    private float originalSpeed;       // Karakterin orijinal h�z�
    private bool isSpeedBoostActive = false; // H�z art��� aktif mi kontrol�

    private void Start()
    {
        // Orijinal h�z� belirlemek i�in varsay�lan de�er
        originalSpeed = GetComponent<NinjaMovementCO>().moveSpeed;
        speedFX.SetActive(false); // Oyun ba�larken kapal�
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost")) // Kutunun "SpeedBoost" tag'� oldu�undan emin ol
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
            GetComponent<NinjaMovementCO>().moveSpeed += speedIncrease; // H�z� art�r
            speedFX.SetActive(true); // VFX'i aktif et

            // H�z art���n� zamanlay�c� ile geri d�nd�r
            Invoke("DeactivateSpeedBoost", speedDuration);
        }
    }

    private void DeactivateSpeedBoost()
    {
        GetComponent<NinjaMovementCO>().moveSpeed = originalSpeed; // H�z� orijinal haline getir
        speedFX.SetActive(false); // H�z art��� VFX'ini devre d��� b�rak
        isSpeedBoostActive = false;
    }
}

