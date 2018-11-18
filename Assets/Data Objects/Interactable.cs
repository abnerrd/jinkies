using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : IInteractable
{
    public InteractableData Data;

    public Interactable(InteractableData data)
    {
        Data = data;
    }

    public virtual Option GetOption()
    {
        var debugString = Data.ChoiceText;
        return new Option()
        {
            Text = debugString,
            ActionCallback = () => { Debug.Log(debugString); Interact(); }
        };
    }

    public abstract void Interact();
}

public interface IInteractable
{
    Option GetOption();
    void Interact();
}
