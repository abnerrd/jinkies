using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectOption : MonoBehaviour {

    public RectTransform Hand;
    public RectTransform Option1;
    public RectTransform Option2;
    public RectTransform Option3;

    private Transform _selectedOption;

    private void Update()
    {
        RectTransform tempOption = null;
        if(Input.GetKeyDown(KeyCode.Q))
        {
            tempOption = Option1;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            tempOption = Option2;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            tempOption = Option3;
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

                //  INVOKE AN OPTION WAS SELECTED
            }
        }
    }
}
