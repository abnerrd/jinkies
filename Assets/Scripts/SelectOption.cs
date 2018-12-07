using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class EventDelegate
{
    public delegate void LoadOptionsHandler(List<Option> optionsList);
    public static event LoadOptionsHandler LoadOptions;
    public static void OnLoadOptions(List<Option> optionsList)
    {
        if (LoadOptions != null)
            LoadOptions(optionsList);
    }

    public delegate void ToggleOptionSelectHandler(bool visible);
    public static event ToggleOptionSelectHandler ToggleOptionsSelectVisible;
    public static void OnToggleOptionSelectVisible(bool visible)
    {
        if (ToggleOptionsSelectVisible != null)
            ToggleOptionsSelectVisible(visible);
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
    private bool _selectVisible;

    private void Awake()
    {
        _currentOptions = new Option[3];
        EventDelegate.LoadOptions += LoadOptions;
        EventDelegate.ToggleOptionsSelectVisible += ToggleSelectVisible;
    }

    private void OnDestroy()
    {
        EventDelegate.LoadOptions -= LoadOptions;
        EventDelegate.ToggleOptionsSelectVisible -= ToggleSelectVisible;
    }

    private void LoadOptions(List<Option> optionsList)
    {
        optionsList.CopyTo(_currentOptions);
    }

    private void ToggleSelectVisible(bool visible)
    {
        _selectVisible = visible;
        if(visible == true)
        {
            var pos = Option1.position;
            pos.x += Option1.rect.width / 2;
            pos.y += (Option1.rect.height + 10);
            Hand.position = pos;
            Hand.gameObject.SetActive(true);
        }
        else
        {
            Hand.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(!_selectVisible)
        {
            return;
        }

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

                EventDelegate.OnOptionSelected(_selectOptionData);
                EventDelegate.OnClearOptions();
                ToggleSelectVisible(false);
            }
        }
    }
}
