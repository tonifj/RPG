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
        player.AddConsumible(GetConsumible(1));
        player.AddConsumible(GetConsumible(2));
        player.AddConsumible(GetConsumible(2));
        player.AddConsumible(GetConsumible(3));
        player.AddConsumible(GetConsumible(4));
        player.AddConsumible(GetConsumible(5));
        player.AddConsumible(GetConsumible(5));
        player.AddConsumible(GetConsumible(5));
        player.AddConsumible(GetConsumible(6));
        player.AddConsumible(GetConsumible(7));
        player.AddConsumible(GetConsumible(8));
        player.AddConsumible(GetConsumible(9));
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
            {1, new Consumible("Cola loca", "Sugary hudration! Heals 50 hp", Consumible.ConsumibleType.HP_HEAL, 50, 45) },
            {2, new Consumible("Aquarius", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {3, new Consumible("Cerveza", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {4, new Consumible("Aigongas", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {5, new Consumible("Saltwater", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {6, new Consumible("Agua de fiordo", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {7, new Consumible("Estreya damn", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {8, new Consumible("Moritz", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {9, new Consumible("Leche fresca", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },
            {10, new Consumible("Nestea", "Stay Hydrated. Helas 25 hp", Consumible.ConsumibleType.HP_HEAL, 25, 25) },

        };

        armors = new Dictionary<int, Armor>()
        {
            {0, new Armor("Training band", "Prepared to store sweat like a boss.", Armor.ArmorType.HELMET, 1, 0, 1, 0, 0, 0, 0, 30) }
        };

        weapons = new Dictionary<int, Weapon>()
        {
            {0, new Weapon(".38 airsoft replica", "Training gun.", Weapon.WeaponType.SINGLE_HANDED, 1, 0, 1, 0, 0, 0, 0, 3, 100) }

        };

        misc = new Dictionary<int, Misc>()
        {
            {0, new Misc("Rocc", "Has a curious shape.", 1 ) }
        };
    }

    Consumible GetConsumible(int id)
    {
        return consumibles[id];
    }

}
