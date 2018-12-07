using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Text))]
public class TextTypewriter : MonoBehaviour
{
    [TextArea]
    public string Text;
    public float CharacterInterval;

    private string _partialText;
    private float _cumulativeDeltaTime;
    private bool _display;
    private bool _finished;
    private Text _text;

    private Action _finishedCallback;

    private void Awake()
    {
        _text = GetComponent<Text>();

        ResetText();
    }

    private void Update()
    {
        if (!_display && !_finished)
        {
            return;
        }

        _cumulativeDeltaTime += Time.deltaTime;
        while(_cumulativeDeltaTime >= CharacterInterval && _partialText.Length < Text.Length)
        {
            _partialText += Text[_partialText.Length];
            _cumulativeDeltaTime -= CharacterInterval;
        }
        _text.text = _partialText;

        if(_partialText.Length == Text.Length)
        {
            _finished = true;
            if(_finishedCallback != null)
            {
                _finishedCallback();
                _finishedCallback = null;

                _display = false;
                _finished = false;
            }
        }
    }

    public void Display(Action FinishedCallback = null)
    {
        _display = true;
        ResetText();

        _finishedCallback = FinishedCallback;
    }

    public void ResetText()
    {
        _finished = false;
        _partialText = "";
        _text.text = "";
        _cumulativeDeltaTime = 0;
    }
}
