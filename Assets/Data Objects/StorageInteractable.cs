using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInteractable : Interactable
{
    public List<Interactable> StorageContents;

    public override void Interact()
    {
        //  TODO aherrera : (no contents?) "Empty dresser, assess options"
        //                  (some contents) add option to take item into options
    }
}
