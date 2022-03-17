using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleInventory : MonoBehaviour
{
    Player player;
    public Button button_prefab;

    bool created_item_buttons = false;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerGO").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!created_item_buttons)
        {
            CreateItemButtons();
            created_item_buttons = true;
        }    
    }

    void CreateItemButtons()
    {
        for (int i = 0; i < player.playerConsumibles.Count; ++i)
        {  
            Button new_button = Instantiate(button_prefab);
            new_button.transform.SetParent(gameObject.transform);
            new_button.GetComponentInChildren<Text>().text = player.playerConsumibles[i].GetName();
        }
    }
}