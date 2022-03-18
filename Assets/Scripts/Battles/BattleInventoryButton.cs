using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInventoryButton : MonoBehaviour
{
    public GameObject help_panel;
    BattleManager bm;
    GameObject help_text;
    public string message;

    Consumible holded_item;

    void Start()
    {
        help_text = GameObject.FindGameObjectWithTag("help panel text");
        bm = GameObject.FindGameObjectWithTag("battle manager").GetComponent<BattleManager>();
        GetComponent<Button>().onClick.AddListener(SetItem);
    }

    void Update()
    {
        
    }

    void SetItem()
    {
        if(holded_item != null)
            bm.SetItemToBeUsed(holded_item);
    }

    public void ActivateHelpPanel()
    {
        if (help_panel.GetComponent<Image>().enabled == false)
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

    public void SetConsumible(Consumible consum)
    {
        holded_item = consum;
    }
}
