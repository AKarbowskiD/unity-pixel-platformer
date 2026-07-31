using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject StartMenuPanel; 
    private bool gameStarted = false;

    void Awake()
    {
        Time.timeScale = 0f;
        StartMenuPanel.SetActive(true);
    }

    void Update()
    {
        if (!gameStarted && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            StartGame();
        }
    }

    void StartGame()
    {
        gameStarted = true;
        Time.timeScale = 1f; 
        StartMenuPanel.SetActive(false); 
    }
}