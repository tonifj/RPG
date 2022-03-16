using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<int, Weapon> weapons = new Dictionary<int, Weapon>();
    public Dictionary<int, Armor> armors = new Dictionary<int, Armor>();
    public Dictionary<int, Consumible> consumibles = new Dictionary<int, Consumible>();
    public Dictionary<int, Misc> misc = new Dictionary<int, Misc>();

    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerGO").GetComponent<Player>();
        CreateItemDB();
        player.AddConsumible(GetConsumible(0));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateItemDB()
    {
        consumibles = new Dictionary<int, Consumible>()
        {
            {0, new Consumible("Mineral water", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {1, new Consumible("Cola loca", "Sugary hudration! Heals 50 hp", Consumible.ConsumibleType.HP_HEAL, 50, 45) }
        };

        armors = new Dictionary<int, Armor>()
        {
            {2, new Armor("Training band", "Prepared to store sweat like a boss.", Armor.ArmorType.HELMET, 1, 0, 1, 0, 0, 0, 0, 30) }
        };

        weapons = new Dictionary<int, Weapon>()
        {
            {3, new Weapon(".38 airsoft replica", "Training gun.", Weapon.WeaponType.SINGLE_HANDED, 1, 0, 1, 0, 0, 0, 0, 3, 100) }

        };

        misc = new Dictionary<int, Misc>()
        {
            {4, new Misc("Rocc", "Has a curious shape.", 1 ) }
        };
    }

    Consumible GetConsumible(int id)
    {
        return consumibles[id];
    }

}
