using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayInfoPanel : BaseGamePlay
{
    [SerializeField] private TextMeshProUGUI InfoText;
    
    override protected void Start()
    {
        GameService.ShowPanelTextAction += ShowText;
        gameObject.SetActive(false);

        base.Start();
    }

    protected override void OnHandClicked(HandTypes handType)
    {
        base.OnHandClicked(handType);
        gameObject.SetActive(true);
    }

    public void OnContinueClicked()
    {
        gameObject.SetActive(false);
        GameService.ContinueClicked();
    }

    private void ShowText(string text)
    {
        gameObject.SetActive(true);
        InfoText.text = text;
    }
}
