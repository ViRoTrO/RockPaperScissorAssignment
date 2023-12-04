using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandView : BaseGamePlay
{
    [SerializeField] private Image HandImage;
    [SerializeField] private TextMeshProUGUI HandNameText;
    [SerializeField] protected Button button;

    private HandTypes _handType;

    public void SetData(HandTypes handType)
    {
        _handType = handType; 
        HandImage.sprite = GameDataInfo.GetHandSprite(handType);
        HandNameText.text = GameDataInfo.GetHandName(handType);
    }

    public void Awake()
    {
        GameService.GameStartedAction += OnGameStart;
    }

    override protected void OnHandClicked(HandTypes handType)
    {
        button.enabled = false;

        base.OnHandClicked(handType);
    }

    public void OnClicked()
    {
        Debug.Log("Clicked " + _handType.ToString());
        GameService.HandClicked(_handType);
    }

    private void OnGameStart()
    {
        button.enabled = true;
    }
}
