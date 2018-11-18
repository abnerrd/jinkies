using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data handler for accessing and modifying all rooms.
/// </summary>
public class Mansion
{
    [SerializeField]
    private Room[][] _mansionLayout;

    public void CreateNewMansion(int rowCount, int columnCount)
    {
        GenerateLayout(rowCount, columnCount);
        EstablishRooms();
    }

    public void GenerateLayout(int rowCount, int columnCount)
    {
        _mansionLayout = new Room[rowCount][];

        for(int r = 0; r < rowCount; ++r)
        {
            _mansionLayout[r] = new Room[columnCount];

            for (int c = 0; c < columnCount; ++c)
            {
                var newRoom = new Room();

                newRoom.Coordinates.X = c;
                newRoom.Coordinates.Y = r;

                _mansionLayout[r][c] = newRoom;
            }
        }
    }

    public void EstablishRooms()
    { 
        for(int r = 0 ; r < _mansionLayout.Length; ++r)
        {
            var row = _mansionLayout[r];
            for(int c = 0; c < row.Length; ++c)
            {
                var currentRoom = row[c];

                //  TODO aherrera : Generate Random room data
                InteractableData data;
                var rng = UnityEngine.Random.Range(0, 3);
                switch(rng)
                {
                    case 0: data = InteractableDatabase.instance.GetInteractableObject<RoomData>();
                        break;
                    case 1: data = InteractableDatabase.instance.GetInteractableObject<ItemData>();
                        break;
                    case 2:
                    default: data = InteractableDatabase.instance.GetInteractableObject<CreatureData>();
                        break;
                }

                currentRoom.Interactions.Add(data);

                //  Establish connections  
                //  We make the connections via backwards-checking
                var topCoordinate = new Coordinate(r, c - 1);
                var leftCoordinate = new Coordinate(r - 1, c);

                if (IsValidCoordinate(topCoordinate))
                {
                    var roomAtCoordinate = _mansionLayout[topCoordinate.X][topCoordinate.Y];

                    //  TODO aherrera : add list extension, 'AddUnique'
                    roomAtCoordinate.Connections.Add(new Connection(currentRoom));

                    currentRoom.Connections.Add(new Connection(roomAtCoordinate));

                }

                if (IsValidCoordinate(leftCoordinate))
                {
                    var roomAtCoordinate = _mansionLayout[leftCoordinate.X][leftCoordinate.Y];

                    //  TODO aherrera : add list extension, 'AddUnique'
                    roomAtCoordinate.Connections.Add(new Connection(currentRoom));

                    currentRoom.Connections.Add(new Connection(roomAtCoordinate));
                }

                _mansionLayout[r][c] = currentRoom;
            }
        }
    }

    public void LogLayout()
    {
        for (int r = 0; r < _mansionLayout.Length; ++r)
        {
            var row = _mansionLayout[r];
            for(int c = 0; c < row.Length; ++c)
            {
                //  Idk stuff to debug, like deets

                Debug.Log(string.Format("{0},{1} is a Room", r, c));
            }
        }
    }

    public bool IsValidCoordinate(Coordinate coordinate)
    {
        return IsValidCoordinate(coordinate.X, coordinate.Y);
    }

    public bool IsValidCoordinate(int xCoordinate, int yCoordinate)
    {
        return (xCoordinate >= 0 && xCoordinate < _mansionLayout.Length)
            && (yCoordinate >= 0 && _mansionLayout[xCoordinate] != null && yCoordinate < _mansionLayout[xCoordinate].Length);
    }

    public Room GetRoom(Coordinate coordinates)
    {
        return GetRoom(coordinates.X, coordinates.Y);
    }

    public Room GetRoom(int xCoordinate, int yCoordinate)
    {
        try
        {
            return _mansionLayout[xCoordinate][yCoordinate];
        }
        catch
        {
            Debug.LogErrorFormat("Cannot get Room[{0}][{1}]", xCoordinate, yCoordinate);
        }
        return null;
    }
}
