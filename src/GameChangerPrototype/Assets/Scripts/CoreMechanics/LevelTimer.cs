using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    public float elapsedTime;
    public string timerFormatted;
    public Text timerDisplay;

    void Update() {
        elapsedTime += Time.deltaTime;
        timerFormatted = FormatTime();
        timerDisplay.text = timerFormatted;
    }

    string FormatTime() {
        float minutes = Mathf.Floor(elapsedTime / 60);
        float seconds = Mathf.Floor(elapsedTime - (minutes * 60));
        float milliseconds = Mathf.Round((elapsedTime - (minutes * 60) - seconds) * 1000f);
        return $"{minutes}:{seconds}:{milliseconds}";
    }
}
