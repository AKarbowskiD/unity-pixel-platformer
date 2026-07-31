using UnityEngine;

public class Timer : MonoBehaviour
{
    private float timer;
    public static float timeLimit;
    public static int displayTime;
    public static bool havePlayerLost=false;
    public GameObject player;
    
    void Start()
    {
        timer = 0;
    }
    void FixedUpdate()
    {
        if (Player.movement == 0)
        {
            timer += Time.deltaTime;
            displayTime = Mathf.FloorToInt(timer);
            CalculateTimeLimit();
        }
        else { timer = 0; }

        if (timer > timeLimit)
        {
            havePlayerLost = true;
        }
    }

    void CalculateTimeLimit()
    {
        float h = PolesGeneration.height;

        if (h <= 100)
            timeLimit = (h / 50f) * 24f + 5f;
        else if(h <= 200)
            timeLimit = (h / 50f) * 22f;
        else if (h <= 400)
            timeLimit = (h / 50f) * 20f;
        else if (h <= 600)
            timeLimit = (h / 50f) * 18f;
        else if (h <= 800)
            timeLimit = (h / 50f) * 16f;
        else if (h <= 1000)
            timeLimit = (h / 50f) * 15f;
        else
        {
            float extraDifficulty = ((h - 1000f) / 100f) * 0.2f;
            timeLimit = (h / 50f) * (15f-extraDifficulty);
        }
    }
}
