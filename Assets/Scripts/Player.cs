using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Dictionary<Weapon, int> playerWeapons = new Dictionary<Weapon, int>();
    public Dictionary<Consumible, int> playerConsumibles = new Dictionary<Consumible, int>();
    public Dictionary<Armor, int> playerArmors = new Dictionary<Armor, int>();
    public Dictionary<Misc, int> playerMisc = new Dictionary<Misc, int>();
    public List<Unit> playerUnits = new List<Unit>();

    public bool battle_inv_updated = false;

    //public static Player instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //for(int i = 0; i < playerConsumibles.Count; ++i)
        //Debug.Log(playerConsumibles[i].GetName());
    }

    public void AddConsumible(Consumible new_item)
    {

        if (!playerConsumibles.ContainsKey(new_item))
            playerConsumibles.Add(new_item, 1);

        else
            playerConsumibles[new_item] = playerConsumibles[new_item] + 1;

    }

    public void AddWeapon(Weapon new_item)
    {
        if (!playerWeapons.ContainsKey(new_item))
            playerWeapons.Add(new_item, 1);

        else
            playerWeapons[new_item] = playerWeapons[new_item] + 1;
    }

    public void AddArmor(Armor new_item)
    {
        if (!playerArmors.ContainsKey(new_item))
            playerArmors.Add(new_item, 1);

        else
            playerArmors[new_item] = playerArmors[new_item] + 1;
    }

    public void AddMisc(Misc new_item)
    {
        if (!playerMisc.ContainsKey(new_item))
            playerMisc.Add(new_item, 1);

        else
            playerMisc[new_item] = playerMisc[new_item] + 1;
    }

    public void RemoveConsumible(Consumible consum)
    {
       // if(playerConsumibles.ContainsKey(consum))
       // {   
            if(playerConsumibles[consum] > 1)
                playerConsumibles[consum] = playerConsumibles[consum] - 1;

            else if (playerConsumibles[consum] == 1)
            {
                playerConsumibles.Remove(consum);
            }
       // }

        

        battle_inv_updated = true;
    }

    public int GetInventorySize()
    {
        return playerWeapons.Count + playerMisc.Count + playerArmors.Count + playerConsumibles.Count;
    }
}
