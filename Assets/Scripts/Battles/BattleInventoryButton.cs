using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInventoryButton : MonoBehaviour
{
    GameObject help_panel;
    Text help_text;

    private void Start()
    {
        help_panel = GameObject.FindGameObjectWithTag("help panel");
        help_text = help_panel.GetComponentInChildren<Text>();
    }
    void ActivateHelpPanel()
    {
        help_panel.SetActive(true);

    }

    void HideHelpPanel()
    {
        help_panel.SetActive(false);
    }
}
