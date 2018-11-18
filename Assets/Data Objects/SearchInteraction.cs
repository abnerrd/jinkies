using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchInteraction : Interactable
{
    public bool IsSearched;
    public Interactable NextInteraction;

    public override Option GetOption()
    {
        if(IsSearched)
        {
            return NextInteraction.GetOption();
        }

        return base.GetOption();
    }

    public override void Interact()
    {
        if(!IsSearched)
        {
            IsSearched = true;
        }
        else
        {
            NextInteraction.Interact();
        }
    }
}
