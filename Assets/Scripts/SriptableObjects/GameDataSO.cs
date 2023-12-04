using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject
{
    [SerializeField] private GammeRules[] HandRules;
    [SerializeField] private Hand[] HandInfo;
    [SerializeField] private float startTime;
    [SerializeField] private float streakTimeDecrement;
    [SerializeField] private float minimumTime;
    [SerializeField] private int scoreMultiplier;
    [SerializeField] private string winnerText;
    [SerializeField] private string looserText;
    [SerializeField] private string replaceWithScore;

    public int ScoreMultiplier => scoreMultiplier;
    public string WinnerText => winnerText;
    public string LooserText => looserText;
    public string ReplaceWithScore => replaceWithScore;

    public List<HandTypes> GetAllHandTypes()
    {
        return HandRules.Select(item => item.Id).ToList();
    }

    public GammeRules GetRuleForHand(HandTypes handType)
    {
        var hand = HandRules.First(item => item.Id == handType);

        if(hand == null)
            Logger.Log("GameData", "GetRuleForHand", $"{handType.ToString()} is not present!!");

        
        return hand;
    }

    public string GetHandName(HandTypes handType)
    {
        var handInfo = HandInfo.First(item => item.Id == handType);

        if(handInfo == null)
        {
            Logger.Log("GameData", "GetHandForTHeText", $"{handType.ToString()} is not present!!");
            return "";
        }
            
        return handInfo.NameText;
    }

    public Sprite GetHandSprite(HandTypes handType)
    {
        return HandInfo.First(item => item.Id == handType).Sprite;
    }

    public float StartTime(int streak)
    {
        return Mathf.Clamp(startTime - streakTimeDecrement * streak, minimumTime, startTime);
    }
}

[Serializable]
public class GammeRules
{
    [SerializeField] public HandTypes Id;
    [SerializeField] public LostHandInfo[] WinsOver;
}

[Serializable]
public class Hand
{
    [SerializeField] public HandTypes Id;
    [SerializeField] public string NameText;
    [SerializeField] public Sprite Sprite;
}

[Serializable]
public class LostHandInfo
{
    [SerializeField] public HandTypes LostHand;
    [SerializeField] public string WinnerText;
}