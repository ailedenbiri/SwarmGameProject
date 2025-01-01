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

    public GameObject dashVFX;      // Karakterin altýnda gösterilecek dash VFX
    public GameObject dashTextVFX;  // DashObject altýndaki VFX

    private bool canDash = false;
    private float dashEnableTime = 3f;
    private Coroutine dashTimerCoroutine;

    public GameManager gameManager; // GameManager referansý
    public UIManager uiManager;

    private void Awake()
    {
        dashAnimator = GetComponent<Animator>();

        // Dash VFX'leri baþlangýçta devre dýþý býrakýyoruz
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
        if (other.CompareTag("DashTrigger")) // Dash nesnesine deðdiðimizde
        {
            ActivateDashAbility();

            // DashObject altýndaki VFX'i baðýmsýz hale getir
            if (dashTextVFX != null)
            {
                dashTextVFX.SetActive(true);
                dashTextVFX.transform.SetParent(null, true); // DashObject ile baðýmsýz hale getir
            }

            // Performansý artýrmak için Destroy yerine devre dýþý býrakmayý düþünebilirsiniz
             other.gameObject.SetActive(false);
        }
    }

    void ActivateDashAbility()
    {
        canDash = true;

        if (gameManager != null)
        {
            uiManager.ShowDashMessage(dashEnableTime); // Dash süresi boyunca mesajý göster
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

                // Dash tamamlandýðýnda karakterin VFX'ini devre dýþý býrak
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

        // Dash baþladýðýnda karakterin VFX'ini etkinleþtir
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




