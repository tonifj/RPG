using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, Item> playerInventory = new Dictionary<string, Item>(); //make player class as singleton?
    public Dictionary<string, Consumible> gameConsumibles = new Dictionary<string, Consumible>();

    void Start()
    {
        CreateConsumibles();
        playerInventory.Add(gameConsumibles["Potion"].GetName(), gameConsumibles["Potion"]);

        Debug.Log(playerInventory["Potion"]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateConsumibles()
    {
        Consumible potion = new Consumible("Potion", 25, Consumible.ConsumibleType.HEAL, 20);
        gameConsumibles.Add(potion.GetName(), potion);
    }

}
