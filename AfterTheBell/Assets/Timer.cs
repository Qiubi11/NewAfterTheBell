using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 60; // Set your starting time here
    public TextMeshProUGUI timerText; // Drag your UI text here in the Inspector
    private bool timerIsRunning = true;

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Gradually decrease time based on real-world seconds
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // Formats time into Minutes:Seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}