using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInventory : MonoBehaviour
{
    Player player;
    public Button button_prefab;
    Button new_button;
    bool created_item_buttons = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerGO").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!created_item_buttons)
        {
            CreateItemButtons();
            created_item_buttons = true;
        }
    }

    void CreateItemButtons()
    {

        foreach (KeyValuePair<Consumible, int> item in player.playerConsumibles)
        {
            new_button = Instantiate(button_prefab);
            new_button.transform.SetParent(gameObject.transform);
            new_button.GetComponent<BattleInventoryButton>().help_panel = GameObject.FindGameObjectWithTag("help panel");
            new_button.GetComponent<BattleInventoryButton>().SetConsumible(item.Key);
            new_button.GetComponent<BattleInventoryButton>().message = item.Key.GetDescription();
            new_button.GetComponentInChildren<Text>().text = item.Key.GetName() + " x" + item.Value.ToString();
        }      
    }   
}
