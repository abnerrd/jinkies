using System;
using UnityEngine;

public enum ItemType
{
    Useless,
    Key,
    Weapon
}

[CreateAssetMenu(fileName = "Item", menuName = "Interactable/Item")]
public class ItemData : InteractableData
{
    public ItemType Type;
}
