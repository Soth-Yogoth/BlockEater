using System;
using TMPro;
using UnityEngine;

public class TimerView : MonoBehaviour
{
    private TMP_Text timerText;
    private Timer timer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        timerText = GetComponent<TMP_Text>();
        timer = GetComponent<Timer>();
    }
    
    private void OnEnable()
    {
        timer.OnTimeChange += UpdateTimerView;
    }

    private void UpdateTimerView(float time)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        timerText.text = timeSpan.ToString(@"mm\:ss");
    }
    
    private void OnDisable()
    {
        timer.OnTimeChange -= UpdateTimerView;
    }
}
