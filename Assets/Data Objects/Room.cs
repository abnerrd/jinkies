using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Coordinate RoomCoordinate;

    //  list of connections
    
    //  list of storages/interactables

    //  list of apparent enemies
}

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
