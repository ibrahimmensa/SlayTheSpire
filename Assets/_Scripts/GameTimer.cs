using UnityEngine;
using UnityEngine.UI;
using System;

public class GameTimer : MonoBehaviour
{
    public Text totalTimeText;
    public Text maxTimeText;
    public Text nowTimeText;

    public float startTime;
    public float savedTime;
    public float maxTime;

    private bool isRunning = false;

    private void Start()
    {
        savedTime = PlayerPrefs.GetFloat("Time",0);
        maxTime = PlayerPrefs.GetFloat("maxTime", 0);
        maxTimeText.text = FormatTime(Math.Max(savedTime, (Time.time - startTime)));
        StartGame();
    }

    private void Update()
    {
        if (isRunning)
        {
            double elapsedTime = Time.time - startTime;
            totalTimeText.text = FormatTime(savedTime + elapsedTime);
            //maxTimeText.text = FormatTime(Math.Max(maxTime, elapsedTime));
            nowTimeText.text = FormatTime(elapsedTime);
        }
    }

    public void StartGame()
    {
        if (!isRunning)
        {
            startTime = Time.time;
            isRunning = true;
        }
    }

    public void StopGame()
    {
        if (isRunning)
        {
            isRunning = false;
            float elapsedTime = Time.time - startTime;
            savedTime += elapsedTime;
            maxTime = Math.Max(maxTime, elapsedTime);

            PlayerPrefs.SetFloat("Time", savedTime);
            PlayerPrefs.SetFloat("maxTime", maxTime);
        }
    }

    private string FormatTime(double time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
    }
    private void OnApplicationQuit()
    {
        //StopGame();
        Debug.Log("app quit");
    }
}
