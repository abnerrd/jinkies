using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entry to game flow.
/// </summary>
public class Application : MonoBehaviour
{
    public static Application instance = null;
    
    private Mansion _mansionFacet;
    private ChoiceMaker _choicesFacet;
    private Player _playerFacet;
    private CreatureManager _creatureManagerFacet;

    public Mansion MansionFacet { get { return _mansionFacet; } }
    public Player PlayerFacet { get { return _playerFacet; } }

    [Header("Startup Parameters")]
    public bool GenerateMansionOnStartup;
    public bool SetPlayerAtStartPoint;
    public Coordinate StartPoint;

    [Header("Mansion Parameters")]
    public int Rows = 5;
    public int Columns = 5;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        _mansionFacet = new Mansion();
        _choicesFacet = new ChoiceMaker();
        _playerFacet = new Player();
        _creatureManagerFacet = new CreatureManager();
    }

    // Use this for initialization
    void Start ()
    {
        if (GenerateMansionOnStartup)
        {
            GenerateMansion();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [ContextMenu("Generate Mansion")]
    public void GenerateMansion()
    {
        _mansionFacet.GenerateRoomLayout(Rows, Columns);

        if(SetPlayerAtStartPoint)
        {
            if(_mansionFacet.IsValidCoordinate(StartPoint))
            {
                _playerFacet.Coordinates = StartPoint;
            }
        }
    }

    [ContextMenu("Log Mansion")]
    public void LogMansionLayout()
    {
        _mansionFacet.LogLayout();
    }

    [ContextMenu("Log Player Options")]
    public void LogPlayerOptions()
    {
        var playerCoordinates = _playerFacet.Coordinates;

        if(!_mansionFacet.IsValidCoordinate(playerCoordinates))
        {
            Debug.LogWarning("Player not in valid coordinates!");
            return;
        }

        var room = _mansionFacet.GetRoom(playerCoordinates);

        if(room == null)
        {
            Debug.LogWarning("Room not found!");
            return;
        }

        Debug.LogFormat("At [{0}][{1}], player can: ", playerCoordinates.X, playerCoordinates.Y);

        var options = room.GetAvailableOptions();
        foreach(var opt in options)
        {
            Debug.LogFormat(opt.Name);
        }
    }

    [ContextMenu("Do First Available Action")]
    public void TakeFirstAvailableAction()
    {
        var playerCoordinates = _playerFacet.Coordinates;

        if (!_mansionFacet.IsValidCoordinate(playerCoordinates))
        {
            Debug.LogWarning("Player not in valid coordinates!");
            return;
        }

        var room = _mansionFacet.GetRoom(playerCoordinates);

        if (room == null)
        {
            Debug.LogWarning("Room not found!");
            return;
        }

        var options = room.GetAvailableOptions();
        options[0].ActionCallback.Invoke();
    }
}
