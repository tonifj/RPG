using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    public enum WeaponType
    {
        SINGLE_HANDED,
        DUAL_HANDED
    }

    int hp;
    int phy_str;
    int psi_str;
    int phy_res;
    int psi_res;
    int speed;
    int mov;
    int weapon_range;
    WeaponType weaponType;

    public Weapon(string name, string desc, WeaponType wType, int phyS, int psiS, int phyR, int psiR, int spe, int hp, int mov, int range, int value)
    {
        weaponType = wType;
        SetDescription(desc);
        SetItemType(ItemType.WEAPON);
        SetRange(range);
        SetName(name);
        SetPhyStr(phyS);
        SetPsiStr(psiS);
        SetPhyRes(phyR);
        SetPsiRes(psiR);
        SetSpeed(spe);
        SetMov(mov);
        SetHP(hp);
        SetShopPrice(value);
        SetSellPrice((int)(value * 2/3));
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

    public int GetPhyStr()
    {
        return phy_str;
    }
    public int GetPhyRes()
    {
        return phy_res;
    }
    public int GetPsiStr()
    {
        return psi_str;
    }
    public int GetPsiRes()
    {
        return psi_res;
    }
    public int GetHP()
    {
        return hp;
    }
    public int GetSpeed()
    {
        return speed;
    }
    public int GetMov()
    {
        return mov;
    }


    public void SetRange(int r)
    {
        weapon_range = r;
    }
}
