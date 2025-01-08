using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuı : MonoBehaviour
{
    // Start butonu fonksiyonu
    public void StartGame()
    {
        // Oyun sahnesini yükle ("GameScene" sahnesinin adını değiştirebilirsiniz)
        SceneManager.LoadScene("1");
    }

    // Quit butonu fonksiyonu
    public void QuitGame()
    {
        // Oyunu kapat
        Application.Quit();
    }
}
