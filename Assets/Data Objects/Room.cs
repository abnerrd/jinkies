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

        //  TODO aherrera : returns top 3 options; Interactables have priority
        while (optionList.Count > 3)
        {
            optionList.RemoveAt(optionList.Count - 1);
        }

        return optionList;
    }

    public string GetRoomDescription()
    {
        var text = "You enter a room with ";
        var options = GetAvailableOptions();
        for (var i = 0; i < options.Count; i++)
        {
            text += options[i].Name;
            if(i < options.Count-1)
            { 
                text += " and ";
            }
            else
            {
                text += ".";
            }
        }
        return text;
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
        //  TODO aherrera,wspier : instead of directly setting player Coordinates, have a 'tryMovePlayer' that will 
        //                          move coordinates on success

        var playerModel = Application.instance.PlayerFacet;
        playerModel.Coordinates = ConnectionDestination.Coordinates;
    }
}
