using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoreLogic : MonoBehaviour
{
    public TextMeshProUGUI HeightText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI TimerText;

    private float CurrentHeight = 0;
    public GameObject player;
    private bool IsGameStarted = false;
    public static int lastAddition = 0;

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            IsGameStarted = true;
        }
          
        if(IsGameStarted==true)
        {
            int currentAddition = Mathf.FloorToInt(CurrentHeight / 10f);

            if (currentAddition > lastAddition)
            {
                Player.CurrentScore+=10;
                lastAddition = currentAddition; 
            }

            var Timeleft =  Timer.timeLimit - Timer.displayTime;

            CurrentHeight = player.transform.position.y + 2;

            HeightText.text = CurrentHeight.ToString("F0") + "m";

            ScoreText.text = "SCORE: " + Player.CurrentScore;

            TimeText.text = "TIME: " + Time.time.ToString("F0") + "s";

            TimerText.text = Timeleft.ToString("F0") + "s";


        }
    }
}