using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room
{
    public Coordinate Coordinates;
    public List<Connection> Connections;

    public List<InteractableData> Interactions;

    //  TODO aherrera : We need to cache Interactions that can happen here so that we don't do em again

    public Room()
    {
        Coordinates = new Coordinate();
        Connections = new List<Connection>();        
        Interactions = new List<InteractableData>();
    }

    public List<Option> GetAvailableOptions()
    {
        var roomData = (RoomData) Interactions.FirstOrDefault(i => i.GetType().IsAssignableFrom(typeof(RoomData)));
        var itemData = (ItemData) Interactions.FirstOrDefault(i => i.GetType().IsAssignableFrom(typeof(ItemData)));
        var creatureData = (CreatureData) Interactions.FirstOrDefault(i => i.GetType().IsAssignableFrom(typeof(CreatureData)));

        var optionList = new List<Option>();

        if (roomData != null)
        {
            if (roomData.IsWinState)
            {
                var winInteraction = new EscapeInteraction(roomData)
                {
                    RequiredItems = roomData.RequiredItems
                };

                optionList.Add(winInteraction.GetOption());
            }

            if (roomData.HasSecretPassage)
            {
                //  TODO aherrera : randomize room
                var room = Application.instance.MansionFacet.GetRoom(0, 0);
                var secretPassageInteraction = new Connection(room, roomData);

                //  TODO aherrera : I Don't think this data is correct
                var searchInteractable = new SearchInteraction(roomData)
                {
                    NextInteraction = secretPassageInteraction
                };

                optionList.Add(searchInteractable.GetOption());
            }
        }

        if(itemData != null)
        {
            //  INTERACTION
        }

        if(creatureData != null)
        {
            //  INTERACTION
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

        for(int i = 0; i < Interactions.Count; ++i)
        {
            text += Interactions[i].Description;
            if (i < Interactions.Count - 1)
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

    public override string ToString()
    {
        return string.Format("[{0},{1}]", X, Y);
    }
}

public class Connection : Interactable
{
    public Room ConnectionDestination;

    public Connection(Room connectedRoom) : base(null)
    {
        ConnectionDestination = connectedRoom;

        //  SETUP DEFAULT CONNECTION DATA
        Data = ScriptableObject.CreateInstance<InteractableData>();
        Data.ChoiceText = string.Format("Move To Room {0}", ConnectionDestination.Coordinates.ToString());
        Data.Description = "Dusty Corridors";
    }

    public Connection(Room connectedRoom, InteractableData data) : base(data)
    {
        ConnectionDestination = connectedRoom;
    }

    public override void Interact()
    {
        //  TODO aherrera,wspier : instead of directly setting player Coordinates, have a 'tryMovePlayer' that will 
        //                          move coordinates on success

        var playerModel = Application.instance.PlayerFacet;
        playerModel.Coordinates = ConnectionDestination.Coordinates;
    }
}
