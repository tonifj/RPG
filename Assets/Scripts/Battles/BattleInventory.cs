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
        if (!created_item_buttons)
        {
            CreateItemButtons();
            created_item_buttons = true;
        }
    }

    void CreateItemButtons()
    {
        Dictionary<Consumible, int> num_of_items = new Dictionary<Consumible, int>();

        for (int i = 0; i < player.playerConsumibles.Count; ++i)
        {

            if (!num_of_items.ContainsKey(player.playerConsumibles[i]))
                num_of_items.Add(player.playerConsumibles[i], 1);

            else
                num_of_items[player.playerConsumibles[i]] = num_of_items[player.playerConsumibles[i]] + 1;
        }

        foreach (KeyValuePair<Consumible, int> item in num_of_items)
        {
            Button new_button = Instantiate(button_prefab);
            new_button.transform.SetParent(gameObject.transform);
            new_button.GetComponentInChildren<Text>().text = item.Key.GetName() + " x" + item.Value.ToString();
        }
            
    }
}
