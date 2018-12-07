using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : MonoBehaviour {

    public Animator Animator;

    private void Awake () {
        EventDelegate.ExitTitle += BringPanelDown;
	}
	
    private void BringPanelDown () {
        Animator.SetBool("IsDown", true);
	}

    private void StartGame()
    {
        EventDelegate.OnStartGame();
    }
}
