using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "Interactable/Room")]
public class RoomData : InteractableData
{
    public bool HasSecretPassage;

    public bool IsWinState;
    public List<ItemType> RequiredItems;
}
