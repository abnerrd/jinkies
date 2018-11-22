using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// The 'ChoiceMaker' is actually gonna be our GameSystem - "JinkieSystem"
/// 
///     - Present Options
///     - Option Select, Option Interact, Option Result
///     - Win? Lose?
///     - Else REPEAT
/// 
/// </summary>

public static partial class EventDelegate
{
    public delegate void OptionSelectedHandler(Option opt);
    public static event OptionSelectedHandler OptionSelect;
    public static void OptionSelected(Option opt)
    {
        if (OptionSelect != null)
            OptionSelect(opt);
    }
}

public class ChoiceMaker
{
    public ChoiceMaker()
    {
        EventDelegate.OptionSelect += OnOptionSelected;
    }

    //  TODO aherrera : there should be a de-listen

    public void NewGameStart()
    {
        PresentOptions();
    }

    private void PresentOptions()
    {
        // Get current room
        var playerRoom = Application.instance.GetPlayerCurrentRoom();

        //  Get all available options
        var playerOptions = Application.instance.GetCurrentPlayerOptions();

        //Trigger event for displaying text. On finish trigger event for displaying options
        EventDelegate.OnDisplayText(playerRoom.GetRoomDescription(), () => {
            EventDelegate.OnDisplayOptions(playerOptions.Select(o => o.Text).ToList());
        });

        //foreach(var o in playerOptions)
        //{
        //    Debug.Log(o.Name);
        //}
        
        EventDelegate.OnLoadOptions(playerOptions);
    }

    public void OnOptionSelected(Option opt)
    {
        var playerModel = Application.instance.PlayerFacet;
        
        opt.ActionCallback.Invoke();

        //  TODO aherrera : read out Option result and such

        bool didWin = playerModel.HasEscaped;
        bool didLose = playerModel.IsDead;
        if(didLose)
        {
            //  Play out lose state
        }
        else if(didWin)
        {
            //  Play out win state
        }
        else
        {
            PresentOptions();
        }
    }

}

/// <summary>
/// Option data structure
/// </summary>
public struct Option
{
    public string Text;
    public Action ActionCallback;
}
