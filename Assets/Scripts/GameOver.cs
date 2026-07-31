using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject GameOverPanelObject;

    void Awake()
    {
        GameOverPanelObject.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        if (Timer.havePlayerLost==true)
        {
            GameOverScreen();
        }
    }

    void GameOverScreen()
    {
        GameOverPanelObject.SetActive(true);

        if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;
                SceneManager.LoadScene(currentSceneName);
                Timer.havePlayerLost = false;


            }
        

    }
}