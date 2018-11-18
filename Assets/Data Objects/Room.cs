using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Coordinate Coordinates;
    public List<Connection> Connections;
    public List<Interactable> Interactables;

    //  list of apparent enemies

    public Room()
    {
        Coordinates = new Coordinate();
        Connections = new List<Connection>();
        Interactables = new List<Interactable>();
    }

    public List<Option> GetAvailableOptions()
    {
        var optionList = new List<Option>();

        foreach(var i in Interactables)
        {
            optionList.Add(i.GetOption());
        }

        foreach(var c in Connections)
        {
            optionList.Add(c.GetOption());
        }

        while(optionList.Count > 3)
        {
            optionList.RemoveAt(optionList.Count - 1);
        }

        //  TODO aherrera : returns top 3 options; Interactables have priority
        return optionList;
    }
}

[Serializable]
public struct Coordinate
{
    public int X;
    public int Y;

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Connection : Interactable
{
    public Room ConnectionDestination;

    public override void Interact()
    {
        //  TODO aherrera : Move to new coordinates

        throw new NotImplementedException();
    }
}
