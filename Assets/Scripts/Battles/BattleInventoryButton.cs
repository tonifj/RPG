using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInventoryButton : MonoBehaviour
{
    public GameObject help_panel;
    GameObject help_text;
    public string message;

    void Start()
    {
        help_text = GameObject.FindGameObjectWithTag("help panel text");
    }

    void Update()
    {

        Debug.Log(help_panel);
    }

    public void ActivateHelpPanel()
    {
        if(help_panel.GetComponent<Image>().enabled == false)
        {
            help_panel.GetComponent<Image>().enabled = true;
            help_text.GetComponent<Text>().enabled = true;
        }

        help_text.GetComponent<Text>().text = message;
        help_panel.SetActive(true);
    }

    public void HideHelpPanel()
    {
        help_panel.SetActive(false);
    }
}
