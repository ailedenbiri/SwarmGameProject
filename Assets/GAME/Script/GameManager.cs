using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TMP_Text dashMessageText; // TMP_Text bile�eni

    private void Awake()
    {
        if (dashMessageText != null)
        {
            dashMessageText.gameObject.SetActive(false); // Ba�lang��ta metni gizle
        }
    }

    public void ShowDashMessage(float duration)
    {
        if (dashMessageText != null)
        {
            StartCoroutine(FlashDashText(duration));
        }
    }

    private IEnumerator FlashDashText(float duration)
    {
        float elapsedTime = 0f;
        bool isVisible = false;

        while (elapsedTime < duration)
        {
            isVisible = !isVisible; // G�r�n�rl��� de�i�tir
            dashMessageText.gameObject.SetActive(isVisible);

            yield return new WaitForSeconds(0.5f); // Yan�p s�nme aral���

            elapsedTime += 0.5f;
        }

        dashMessageText.gameObject.SetActive(false); // S�re bitiminde metni gizle
    }
}

