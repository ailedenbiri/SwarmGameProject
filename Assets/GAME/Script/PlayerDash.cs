using System;
using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    private float dashTime;
    private Animator dashAnimator;

    public GameObject dashVFX;      // Karakterin alt�nda g�sterilecek dash VFX
    public GameObject dashTextVFX;  // DashObject alt�ndaki VFX

    private bool canDash = false;
    private float dashEnableTime = 3f;
    private Coroutine dashTimerCoroutine;

    public GameManager gameManager; // GameManager referans�
    public UIManager uiManager;

    private void Awake()
    {
        dashAnimator = GetComponent<Animator>();

        // Dash VFX'leri ba�lang��ta devre d��� b�rak�yoruz
        if (dashVFX != null)
        {
            dashVFX.SetActive(false);
        }

        if (dashTextVFX != null)
        {
            dashTextVFX.SetActive(false);
        }
    }

    void Update()
    {
        if (canDash)
        {
            HandleDash();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DashTrigger")) // Dash nesnesine de�di�imizde
        {
            ActivateDashAbility();

            // DashObject alt�ndaki VFX'i ba��ms�z hale getir
            if (dashTextVFX != null)
            {
                dashTextVFX.SetActive(true);
                dashTextVFX.transform.SetParent(null, true); // DashObject ile ba��ms�z hale getir
            }

            // Performans� art�rmak i�in Destroy yerine devre d��� b�rakmay� d���nebilirsiniz
             other.gameObject.SetActive(false);
        }
    }

    void ActivateDashAbility()
    {
        canDash = true;

        if (gameManager != null)
        {
            uiManager.ShowDashMessage(dashEnableTime); // Dash s�resi boyunca mesaj� g�ster
        }

        if (dashTimerCoroutine != null)
        {
            StopCoroutine(dashTimerCoroutine);
        }

        dashTimerCoroutine = StartCoroutine(DeactivateDashAbilityAfterTime());
    }

    IEnumerator DeactivateDashAbilityAfterTime()
    {
        yield return new WaitForSeconds(dashEnableTime);

        if (!isDashing)
        {
            canDash = false;
        }
    }

    void HandleDash()
    {
        if (!isDashing)
        {
            if (Input.GetKey(KeyCode.D) && Input.GetKeyDown(KeyCode.Space))
            {
                dashDirection = transform.right;
                StartDash("DashRight");
            }
            else if (Input.GetKey(KeyCode.A) && Input.GetKeyDown(KeyCode.Space))
            {
                dashDirection = -transform.right;
                StartDash("DashLeft");
            }
        }

        if (isDashing)
        {
            dashTime += Time.deltaTime;
            transform.Translate(dashDirection * dashSpeed * Time.deltaTime, Space.World);

            if (dashTime >= dashDuration)
            {
                isDashing = false;
                canDash = false;

                // Dash tamamland���nda karakterin VFX'ini devre d��� b�rak
                if (dashVFX != null)
                {
                    dashVFX.SetActive(false);
                }
            }
        }
    }

    void StartDash(string animationName)
    {
        isDashing = true;
        dashTime = 0f;
        dashAnimator.Play(animationName);

        // Dash ba�lad���nda karakterin VFX'ini etkinle�tir
        if (dashVFX != null)
        {
            dashVFX.SetActive(true);
        }

        if (dashTimerCoroutine != null)
        {
            StopCoroutine(dashTimerCoroutine);
        }
    }
}




