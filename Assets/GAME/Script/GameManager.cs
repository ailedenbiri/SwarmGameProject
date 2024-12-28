using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    public TMP_Text dashMessageText; // TMP_Text bileþeni

    private void Awake()
    {
        if (dashMessageText != null)
        {
            dashMessageText.gameObject.SetActive(false); // Baþlangýçta metni gizle
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
            isVisible = !isVisible; // Görünürlüðü deðiþtir
            dashMessageText.gameObject.SetActive(isVisible);

            yield return new WaitForSeconds(0.5f); // Yanýp sönme aralýðý

            elapsedTime += 0.5f;
        }

        dashMessageText.gameObject.SetActive(false); // Süre bitiminde metni gizle
    }
}

