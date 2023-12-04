using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : BaseGamePlay
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI displayText;

    private bool _timerEnded;
    private float _timeLeft;
    private bool _pauseTimer;

    private void Awake()
    {
        GameService.GameStartedAction += OnGameStarted;
    }

    override protected void OnHandClicked(HandTypes handType)
    {
        base.OnHandClicked(handType);

        _pauseTimer = true;
    }

    private void OnGameStarted()
    {
        _timeLeft = GameDataInfo.StartTime(GameService.Streak);
        slider.minValue = 0;
        slider.maxValue = _timeLeft;
        _timerEnded = false;
        _pauseTimer = false;
    }

    private void Update()
    {
        if(_timerEnded || _pauseTimer)
            return;
                   
        UpdateTimer();

    }

    private void UpdateTimer()
    {
        if(_timeLeft > 0)
        {
            _timeLeft -= Time.deltaTime;
            displayText.text = _timeLeft.ToString("0.00");
            slider.value = _timeLeft;
            UpdateTextColor(GameDataInfo.StartTime(GameService.Streak), _timeLeft);
        }
        else
        {
            displayText.text = "0:00";
            _timerEnded = true;

            GameService.TimerEnded();
        }
    }

    private void UpdateTextColor(float totalTime, float timeLeft)
    {
        if(timeLeft < totalTime/2)
            displayText.color = Color.red;
        else
            displayText.color = Color.blue;
    }
}
