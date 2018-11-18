using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : IInteractable
{
    public string InteractableName = "Object";
    public string InteractionVerb = "Touch";

    public Option GetOption()
    {
        var debugString = string.Format("{0} {1}", InteractionVerb, InteractableName);
        return new Option()
        {
            Name = debugString,
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
