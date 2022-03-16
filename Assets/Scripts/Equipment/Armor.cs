using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item
{
    public enum ArmorType
    {
        HELMET,
        CHEST,
        PANTS,
        BOOTS
    }

    int hp;
    int phy_str;
    int psi_str;
    int phy_res;
    int psi_res;
    int speed;
    int mov;
    ArmorType armorType;

    public Armor(string name, string desc, ArmorType aType, int phyS, int psiS, int phyR, int psiR, int spe, int hp, int mov, int value)
    {
        armorType = aType;
        SetDescription(desc);
        SetItemType(ItemType.ARMOR);
        SetName(name);
        SetPhyStr(phyS);
        SetPsiStr(psiS);
        SetPhyRes(phyR);
        SetPsiRes(psiR);
        SetSpeed(spe);
        SetMov(mov);
        SetHP(hp);
        SetShopPrice(value);
        SetSellPrice((int)(value * 2 / 3));
    }

    protected void SetPhyStr(int str)
    {
        phy_str = str;
    }

    protected void SetPsiStr(int str)
    {
        psi_str = str;
    }

    protected void SetPhyRes(int res)
    {
        phy_res = res;
    }

    protected void SetPsiRes(int res)
    {
        psi_res = res;
    }

    protected void SetSpeed(int spe)
    {
        speed = spe;
    }
    protected void SetMov(int new_mov)
    {
        mov = new_mov;
    }

    protected void SetHP(int new_hp)
    {
        hp = new_hp;
    }

    public string GetName()
    {
        return item_name;
    }
    public void SetDescription(string s)
    {
        item_description = s;
    }
}
