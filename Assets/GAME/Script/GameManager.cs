using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton yap�s�

    private void Awake()
    {
        // Singleton kontrol�
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Sahne de�i�iminde GameManager'� yok etme
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Bir sonraki sahneyi y�kle
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("Son sahne. Oyun bitti!");
            // Oyun sonu ekran� veya men�s� a��labilir
        }
    }
}


