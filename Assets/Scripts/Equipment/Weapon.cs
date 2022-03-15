using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    enum WeaponType //todo
    {
        SINGLE_HANDED,
        DUAL_HANDED
    }
    public Weapon(string name, int phyS, int psiS, int phyR, int psiR, int spe, int hp, int mov, int shop_price)
    {
        SetItemType(ItemType.WEAPON);
        SetName(name);
        SetPhyStr(phyS);
        SetPsiStr(psiS);
        SetPhyRes(phyR);
        SetPsiRes(psiR);
        SetSpeed(spe);
        SetMov(mov);
        SetHP(hp);
        SetShopPrice(shop_price);
        SetSellPrice((int)(shop_price*2/3));
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
}
