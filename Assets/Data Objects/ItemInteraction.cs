using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interactable
{
    private bool ItemTaken = false;

    public ItemInteraction(ItemData data) : base(data)
    {
    }

    public override void Interact()
    {
        if(!ItemTaken)
        {
            ItemTaken = true;

            var playerModel = Application.instance.PlayerFacet;
            playerModel.GiveItem(((ItemData)Data));
        }
    }
}
