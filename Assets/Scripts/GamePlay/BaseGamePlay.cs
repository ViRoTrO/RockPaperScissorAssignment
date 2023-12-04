using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseGamePlay : MonoBehaviour
{
    [SerializeField] protected GameData GameDataInfo;
    
    protected GamePlayService GameService = GamePlayService.Instance;

    protected virtual void Start()
    {
         GameService.HandClickedAction += OnHandClicked;
         GameService.RestartAction += Restart;
    }

    protected virtual void OnHandClicked(HandTypes handType)
    {
        
    }

    protected virtual void Restart()
    {
       
    }
}
