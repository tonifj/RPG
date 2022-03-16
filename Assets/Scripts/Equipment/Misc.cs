using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misc : Item
{
    public Misc(string name, string desc, int value)
    {
        SetName(name);
        SetDescription(desc);
        SetShopPrice(value);
        SetSellPrice((int)(value * 2 / 3));
    }
}
