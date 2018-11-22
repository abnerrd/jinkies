using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class EventDelegate
{
    public delegate void LoadOptionChoices(List<Option> optionsList);
    public static event LoadOptionChoices LoadOptions;
    public static void OnLoadOptions(List<Option> optionsList)
    {
        if (LoadOptions != null)
            LoadOptions(optionsList);
    }
}


public class SelectOption : MonoBehaviour
{

    public RectTransform Hand;
    public RectTransform Option1;
    public RectTransform Option2;
    public RectTransform Option3;

    private Option[] _currentOptions;

    private Transform _selectedOption;
    private Option _selectOptionData;

    private void Awake()
    {
        _currentOptions = new Option[3];
        EventDelegate.LoadOptions += LoadOptions;
    }

    private void OnDestroy()
    {
        EventDelegate.LoadOptions -= LoadOptions;
    }

    private void LoadOptions(List<Option> optionsList)
    {
        optionsList.CopyTo(_currentOptions);
    }

    private void Update()
    {
        RectTransform tempOption = null;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            tempOption = Option1;
            _selectOptionData = _currentOptions[0];
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            tempOption = Option2;
            _selectOptionData = _currentOptions[1];
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tempOption = Option3;
            _selectOptionData = _currentOptions[2];
        }

        if(tempOption != null)
        {
            if(tempOption != _selectedOption)
            {
                var pos = tempOption.position;
                pos.x += tempOption.rect.width/2;
                pos.y += (tempOption.rect.height + 10);
                Hand.position = pos;
                Hand.gameObject.SetActive(true);
                _selectedOption = tempOption;
            }
            else
            {
                Hand.gameObject.SetActive(false);
                _selectedOption.gameObject.GetComponent<Animator>().SetBool("IsSelected", true);
                _selectedOption = null;

                EventDelegate.OptionSelected(_selectOptionData);
            }
        }
    }
}
