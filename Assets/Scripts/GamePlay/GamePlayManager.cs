using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : BaseGamePlay
{
    [SerializeField] private HandView HandPrefab;
    [SerializeField] private Image AIHandImage;
    [SerializeField] private GameObject PlayerOptionsContainer;

    private List<HandTypes> AllHandTypes;

    protected override void Restart()
    {
        base.Restart();
    }

    private void Awake()
    {
        GameService.ContinueClickedAction += OnContinueClicked;
        GameService.TimerEndedAction += OnTimerEnd;
    }

    private void OnTimerEnd()
    {
        PlayerLost();
    }

    private void OnContinueClicked()
    {
        GameService.RestartGame();
        StartTheRound();
    }
    
    override protected void Start()
    {
        AddHandOptions();
        StartTheRound();

        base.Start();
    }

    private void StartTheRound()
    {
        AIHandPlay();
        GameService.StartTimer();
    }

    private void AddHandOptions()
    {
         AllHandTypes = GameDataInfo.GetAllHandTypes();
        
        AllHandTypes.ForEach( hand => {
            AddHandView(hand, PlayerOptionsContainer.transform);
        });
    }

    private void AIHandPlay()
    {
        var randomHand = Random.Range(0, AllHandTypes.Count);
        GameService.AIHandType = AllHandTypes[randomHand];
        AIHandImage.sprite = GameDataInfo.GetHandSprite(GameService.AIHandType);
    }

    private void AddHandView(HandTypes hand, Transform parent)
    {
        var handView = Instantiate(HandPrefab, parent);
        handView.SetData(hand);
    }

    protected override void OnHandClicked(HandTypes handType)
    {
        if(IsWinner(GameService.AIHandType, handType, out string resultText))
        {
            GameService.Streak ++;
            GameService.ShowPanelText($"{resultText}<br>{ReplaceTextWithScore(GameDataInfo.WinnerText)}");
        }
        else
        {
           PlayerLost();
        }

        base.OnHandClicked(handType);

    }

    private bool IsWinner(HandTypes handAI, HandTypes handPlayer, out string displayText)
    {  
        var handRules = GameDataInfo.GetRuleForHand(handPlayer);

        var info = handRules.WinsOver.Where(hand => hand.LostHand == handAI).ToArray();

        if(info != null && info.Any())
        {
            displayText = info.First().WinnerText;
            return true;
        }

        displayText = "";

        return false;
    }

    private string ReplaceTextWithScore(string text)
    {
        if(GameDataInfo.ReplaceWithScore == "")
            return text;

        return text.Replace(GameDataInfo.ReplaceWithScore, GameService.Streak.ToString());
    }

    private void PlayerLost()
    {
        GameService.ShowPanelText(ReplaceTextWithScore(GameDataInfo.LooserText));
        GameService.Streak = 0;
    }
}
