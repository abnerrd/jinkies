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
        return new Option()
        {
            Name = string.Format("{0} {1}", InteractionVerb, InteractableName),
            ActionCallback = Interact
        };
    }

    public abstract void Interact();
}

public interface IInteractable
{
    Option GetOption();
    void Interact();
}
