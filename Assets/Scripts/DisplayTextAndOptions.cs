using System;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTextAndOptions : MonoBehaviour {

    public TextTypewriter Description;
    public TextTypewriter Option1;
    public TextTypewriter Option2;
    public TextTypewriter Option3;

	void Start () {
        Description.Display(() => Option1.Display(() => Option2.Display(() => Option3.Display())));
	}
}
