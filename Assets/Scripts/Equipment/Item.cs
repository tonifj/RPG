using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    protected enum ItemType
    {
        CONSUMIBLE,
        WEAPON,
        ARMOR,
        MISC
    }

    enum Element
    {
        //add here cool stuff
    }

    ItemType itemType;
    protected string item_name;
    protected string item_description;
    protected int shop_price;
    protected int sell_price;

    //protected int hp;
    //protected int phy_str;
    //protected int psi_str;
    //protected int phy_res;
    //protected int psi_res;
    //protected int speed;
    //protected int mov;

    protected void SetName(string new_name)
    {
        item_name = new_name;
    }

    //protected void SetPhyStr(int str)
    //{
    //    phy_str = str;
    //}

    //protected void SetPsiStr(int str)
    //{
    //    psi_str = str;
    //}

    //protected void SetPhyRes(int res)
    //{
    //    phy_res = res;
    //}

    //protected void SetPsiRes(int res)
    //{
    //    psi_res = res;
    //}

    //protected void SetSpeed(int spe)
    //{
    //    speed = spe;
    //}

    protected void SetItemType(ItemType new_type)
    {
        itemType = new_type;
    }

    //protected void SetMov(int new_mov)
    //{
    //    mov = new_mov;
    //}

    //protected void SetHP(int new_hp)
    //{
    //    hp = new_hp;
    //}

    protected void SetShopPrice(int sp)
    {
        shop_price = sp;
    }

    protected void SetSellPrice(int sp)
    {
        sell_price = sp;
    }

    protected void SetDescription(string s)
    {
        item_description = s;
    }

}