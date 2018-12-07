using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static partial class EventDelegate
{
    public delegate void ExitTitleHandler();
    public static event ExitTitleHandler ExitTitle;
    public static void OnExitTitle()
    {
        if (ExitTitle != null)
            ExitTitle();
    }
}

public class TitleScreen : MonoBehaviour {

    public Animator Animator;
    public GameObject Q;
    public GameObject W;
    public GameObject E;

	// Update is called once per frame
	void Update () {
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Q.SetActive(true);   
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            W.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            E.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            Q.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            W.SetActive(false);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            E.SetActive(false);
        }

        if(Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.E))
        {
            Animator.SetBool("Fade", true);
            EventDelegate.OnExitTitle();
        }
	}
}
