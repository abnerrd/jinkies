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
    public int XLayout = 5;
    public int YLayout = 5;

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
        _mansionFacet.GenerateRoomLayout(XLayout, YLayout);
    }

    [ContextMenu("Log Mansion")]
    public void LogMansionLayout()
    {
        _mansionFacet.LogLayout();
    }
}
