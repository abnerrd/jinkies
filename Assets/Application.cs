using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class EventDelegate
{
    public delegate void StartGameHandler();
    public static event StartGameHandler StartGame;
    public static void OnStartGame()
    {
        if (StartGame != null)
            StartGame();
    }
}

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
    public Coordinate StartPoint;
    public bool GameStarted;

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
        EventDelegate.StartGame += OnGameStart;
    }

    private void OnGameStart()
    {
        GenerateMansion();
        //  TODO aherrera : INTIALIZE ROOMS -- done in Mansion?

        _playerFacet.ResetPlayer();
        if (!SetPlayerLocation(StartPoint))
        {
            SetPlayerLocation(new Coordinate(0, 0));
        }
        _choicesFacet.NewGameStart();
    }

    [ContextMenu("Generate Mansion")]
    public void GenerateMansion()
    {
        _mansionFacet.CreateNewMansion(Rows, Columns);
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
        Debug.LogFormat("At [{0}][{1}] : ", playerCoordinates.X, playerCoordinates.Y);

        var options = GetCurrentPlayerOptions();

        if(options == null)
        {
            Debug.Log("No available options!");
            return;
        }

        foreach(var opt in options)
        {
            Debug.LogFormat(opt.Text);
        }
    }

    [ContextMenu("Do First Available Action")]
    public void TakeFirstAvailableAction()
    {
        var options = GetCurrentPlayerOptions();

        if(options == null)
        {
            Debug.Log("No available options!");
            return;
        }

        options[0].ActionCallback.Invoke();
    }

    private bool SetPlayerLocation(Coordinate location)
    {
        if (!_mansionFacet.IsValidCoordinate(location))
        {
            return false;
        }

        _playerFacet.Coordinates = location;
        return true;
    }

    //  Gross that this is here, but for now centralizes logic on how we do this
    public List<Option> GetCurrentPlayerOptions()
    {
        var playerCoordinates = _playerFacet.Coordinates;

        if (!_mansionFacet.IsValidCoordinate(playerCoordinates))
        {
            Debug.LogWarning("Player not in valid coordinates!");
            return null;
        }

        var room = _mansionFacet.GetRoom(playerCoordinates);

        if (room == null)
        {
            Debug.LogWarning("Room not found!");
            return null;
        }
        return room.GetAvailableOptions();
    }

    public Room GetPlayerCurrentRoom()
    {
        var playerCoordinates = _playerFacet.Coordinates;
        return _mansionFacet.GetRoom(playerCoordinates);
    }
}
