using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumible : Item
{
    //item image
    public enum ConsumibleType //todo
    {
        HEAL,
        DAMAGE,
        BOOST,
        NERF
    }

    int power; //raw power
    ConsumibleType consumibleType;

    public Consumible(string name, int pow, ConsumibleType type, int shop_price)
    {
        SetName(name);
        SetShopPrice(shop_price);
        SetSellPrice((int)(shop_price * 2 / 3));
        consumibleType = type;
    }

    public string GetName()
    {
        return item_name;
    }
}
