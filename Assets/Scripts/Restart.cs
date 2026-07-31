using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; 

public class GameReloader : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.rKey.isPressed)
        {
            RestartScene();
        }
    }

    public static void RestartScene()
    {
        Time.timeScale = 0f;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        
    }
}