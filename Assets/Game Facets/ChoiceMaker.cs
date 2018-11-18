using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceMaker {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

/// <summary>
/// Option data structure
/// </summary>
public struct Option
{
    public string Name;
    public Action ActionCallback;
}
