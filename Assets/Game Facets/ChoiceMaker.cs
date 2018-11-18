using System;
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

public class ChoiceMaker
{
    public void NewGameStart()
    {
        PresentOptions();
    }

    private void PresentOptions()
    {
        //  Get all available options
        var playerOptions = Application.instance.GetCurrentPlayerOptions();

        foreach(var o in playerOptions)
        {
            Debug.Log(o.Name);
        }

        //  TODO aherrera,wspier : Set them on buttons and such

        //  TODO aherrera,wspier : Update Display
    }

    public void OptionSelected(Option opt)
    {
        //  TODO aherrera : this will be called from SelectOption -- hwo will we track which option is selected?

        //  Do Option Interaction & Result

        bool didWin = false;
        bool didLose = false;
        if(didLose)
        {
            //  You lose before you win
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
    public string Name;
    public Action ActionCallback;
}
