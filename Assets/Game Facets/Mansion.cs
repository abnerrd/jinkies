using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data handler for accessing and modifying all rooms.
/// </summary>
public class Mansion
{
    private Room[][] _mansionLayout;

    //  Putting generator in this class for now
    public void GenerateRoomLayout(int layoutWidth, int layoutHeight)
    {
        _mansionLayout = new Room[layoutHeight][];

        for(int r = 0; r < layoutHeight; ++r)
        {
            _mansionLayout[r] = new Room[layoutWidth];

            for (int c = 0; c < layoutWidth; ++c)
            {
                var newRoom = new Room();

                //  initialize newRoom

                _mansionLayout[r][c] = newRoom;
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
                var column = _mansionLayout[r][c];

                //  Idk stuff to debug, like deets

                Debug.Log(string.Format("{0},{1} is a Room", r, c));
            }
        }
    }
}
