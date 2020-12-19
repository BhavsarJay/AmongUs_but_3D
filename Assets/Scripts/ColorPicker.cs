using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private bool open;
    public GameObject ColorPanel;
    public Button UseBtn;

    public string wantedColor;

    public void OnClick_UseBtn()
    {
        //Show Color Picker Panel
        if (!open)
        {
            ColorPanel.SetActive(true);
            open = true;

            PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = false;
        }
        else
        {
            ColorPanel.SetActive(false);
            open = false;

            PlayersList.GetMyPlayer().GetComponent<PlayerMovement>().canMove = true;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == PlayersList.GetMyPlayer())
        {
            UseBtn.interactable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == PlayersList.GetMyPlayer())
        {
            UseBtn.interactable = false;
        }
    }

    public void OnClick_Color(string colorName)
    {
        wantedColor = colorName;
        PlayersList.GetMyPlayer().GetComponent<ColorManager>().CheckForColor(wantedColor);
    }
}
