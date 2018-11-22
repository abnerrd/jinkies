using UnityEngine;
using System.Collections.Generic;
using System;

public static partial class EventDelegate
{
    public delegate void DisplayTextHandler(string text, Action finishCallback = null);
    public static event DisplayTextHandler DisplayText;
    public static void OnDisplayText(string text, Action finishCallback = null)
    {
        if (DisplayText != null)
            DisplayText(text, finishCallback);
    }

    public delegate void DisplayOptionsHandler(List<string> options, Action finishCallback = null);
    public static event DisplayOptionsHandler DisplayOptions;
    public static void OnDisplayOptions(List<string> options, Action finishCallback = null)
    {
        if (DisplayOptions != null)
            DisplayOptions(options, finishCallback);
    }
}

public class DisplayTextAndOptions : MonoBehaviour {

    public TextTypewriter Description;
    public TextTypewriter Option1;
    public TextTypewriter Option2;
    public TextTypewriter Option3;

    private void Awake()
    {
        EventDelegate.DisplayText += DisplayText;
        EventDelegate.DisplayOptions += DisplayOptions;
    }

    private void OnDestroy()
    {
        EventDelegate.DisplayText -= DisplayText;
        EventDelegate.DisplayOptions -= DisplayOptions;
    }

    private void Display () {
        Description.Display(() => Option1.Display(() => Option2.Display(() => Option3.Display())));
	}

    private void DisplayText(string text, Action finishCallback = null) {
        Description.Text = text;
        Description.Display(() => {
            if (finishCallback != null)
            {
                finishCallback();
            }
        });
    }

    private void DisplayOptions(List<string> options, Action finishCallback = null){
        if(options.Count >0)
        {
            Option1.Text = options[0];
            Option1.Display(() =>
            {
                if (options.Count > 1)
                {
                    Option2.Text = options[1];
                    Option2.Display(() =>
                    {
                        if (options.Count > 2)
                        {
                            Option3.Text = options[2];
                            Option3.Display(() =>
                            {
                                if(finishCallback != null)
                                {
                                    finishCallback();
                                }
                            });
                        }
                    });
                }
            });
        }
    }
}
