using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    public enum State
    {
        Healthy,
        Injured,
        Dead,
        HasEscaped
    }
    public State CurrentState;

    public Coordinate Coordinates;

    private List<ItemData> _inventory = new List<ItemData>();

    public bool HasEscaped
    {
        get { return CurrentState == State.HasEscaped; }
    }

    public bool IsDead
    {
        get { return CurrentState == State.Dead; }
    }

    public void ResetPlayer()
    {
        _inventory.Clear();
        CurrentState = State.Healthy;
    }

    public void GiveItem(ItemData newItem)
    {
        _inventory.Add(newItem);
    }

    public bool HasItem(ItemType type)
    {
        return _inventory.Any(i => i.Type == type);
    }
}
