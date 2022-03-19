using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : Item
{
    //item image
    public enum ConsumibleType
    {
        HP_HEAL,
        PSI_HEAL,
        HP_DAMAGE,
        PSI_DAMAGE,
        PHY_STR_BOOST,
        PSI_STR_BOOST,
        PHY_RES_BOOST,
        PSI_RES_BOOST,
        PHY_STR_NERF,
        PSI_STR_NERF,
        PHY_RES_NERF,
        PSI_RES_NERF
    }

    int power; //raw power - heal = healing points // damage - damage points // boost - % boost
    ConsumibleType consumibleType;

    public Consumible(string name, string desc, ConsumibleType type, int pow,  int value)
    {
        SetItemType(ItemType.CONSUMIBLE);
        SetName(name);
        SetDescription(desc);
        SetShopPrice(value);
        SetSellPrice((int)(value * 2 / 3));
        consumibleType = type;
        power = pow;
    }
    public string GetName()
    {
        return item_name;
    }

    public string GetDescription()
    {
        return item_description;
    }

    public ConsumibleType GetConsumibleType()
    {
        return consumibleType;
    }

    public int GetPower()
    {
        return power;
    }
}
