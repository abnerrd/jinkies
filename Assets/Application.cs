using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entry to game flow.
/// </summary>
public class Application : MonoBehaviour
{
    private Mansion _mansionFacet;
    private ChoiceMaker _choicesFacet;
    private Player _playerFacet;
    private CreatureManager _creatureManagerFacet;

    [Header("Mansion Parameters")]
    public int Rows = 5;
    public int Columns = 5;

    [Header("Player debug options - pls kill")]
    [SerializeField]
    private Coordinate _playerCoordinates;

    private void Awake()
    {
        _mansionFacet = new Mansion();
        _choicesFacet = new ChoiceMaker();
        _playerFacet = new Player();
        _creatureManagerFacet = new CreatureManager();
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("Generate Mansion")]
    public void GenerateMansion()
    {
        _mansionFacet.GenerateRoomLayout(Rows, Columns);
    }

    [ContextMenu("Log Mansion")]
    public void LogMansionLayout()
    {
        _mansionFacet.LogLayout();
    }

    [ContextMenu("Log Player Options")]
    public void LogPlayerOptions()
    {
        if(!_mansionFacet.IsValidCoordinate(_playerCoordinates))
        {
            Debug.LogWarning("Player not in valid coordinates!");
            return;
        }

        var room = _mansionFacet.GetRoom(_playerCoordinates);

        if(room == null)
        {
            Debug.LogWarning("Room not found!");
            return;
        }

        Debug.LogFormat("At [{0}][{1}], player can: ", _playerCoordinates.X, _playerCoordinates.Y);

        var options = room.GetAvailableOptions();
        foreach(var opt in options)
        {
            Debug.LogFormat(opt.Name);
        }

    }
}
