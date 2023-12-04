using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlayService
{

    public int Streak {set; get;}
    public HandTypes AIHandType {set; get;}

    public event Action GameStartedAction;
    public event Action TimerEndedAction;
    public event Action<HandTypes> HandClickedAction;
    public event Action<string> ShowPanelTextAction;
    public event Action ContinueClickedAction;
    public event Action RestartAction;

    private static GamePlayService instance;

    public static GamePlayService Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GamePlayService();
            }
            
            return instance;
        }
    }

    public void StartTimer() => GameStartedAction?.Invoke();
    public void TimerEnded() => TimerEndedAction?.Invoke();
    public void HandClicked(HandTypes handClicked) => HandClickedAction?.Invoke(handClicked);
    public void ShowPanelText(string text) => ShowPanelTextAction?.Invoke(text);
    public void ContinueClicked() => ContinueClickedAction?.Invoke();
    public void RestartGame() => RestartAction?.Invoke();

    private GamePlayService() { }


}
