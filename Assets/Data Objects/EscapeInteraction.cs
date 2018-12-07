using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeInteraction : Interactable
{
    public List<ItemType> RequiredItems;

    public EscapeInteraction(RoomData data) : base(data)
    {

    }

    public override void Interact()
    {
        var playerModel = Application.instance.PlayerFacet;

        bool didEscape = true;
        foreach(var item in RequiredItems)
        {
            if(!playerModel.HasItem(item))
            {
                didEscape = false;
                break;
            }
        }

        if(didEscape)
        {
            playerModel.CurrentState = Player.State.HasEscaped;
        }
    }
}
