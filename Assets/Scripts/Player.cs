using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Weapon> playerWeapons = new List<Weapon>();
    public List<Consumible> playerConsumibles = new List<Consumible>();
    public List<Armor> playerArmors = new List<Armor>();
    public List<Misc> playerMisc = new List<Misc>();
    public List<Unit> playerUnits = new List<Unit>();

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
        playerConsumibles.Add(new_item);
    }

    public void AddWeapon(Weapon new_item)
    {
        playerWeapons.Add(new_item);
    }

    public void AddArmor(Armor new_item)
    {
        playerArmors.Add(new_item);
    }

    public void AddMisc(Misc new_item)
    {
        playerMisc.Add(new_item);
    }

    public int GetInventorySize()
    {
        return playerWeapons.Count + playerMisc.Count + playerArmors.Count + playerConsumibles.Count;
    }
}
