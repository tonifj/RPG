using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassType
{
    RECRUIT,
    AGENT,
    SPECIALIST,
    RIOT,
    COMBAT_MEDIC,
    SNIPER,
    ARCANIST,
    GEO,
    SPY,
    WHITE_ARCANIST,
    MATTER_CONTROLLER,
    WEAPON_MASTER,
    GOD,
    EMPTY
}

public enum Genre
{
    MALE,
    FEMALE
}

public class Unit : MonoBehaviour
{
    string unit_name = "";
    GameObject model;

    public ClassType class_type;
    Genre genre;

    Vector2Int position;

    public bool is_player_unit;

    int movement_range;
    int jumpPower;
    int lvl;
    int exp;

    int max_hp;
    int hp;

    int max_psique;
    int psique;
    int physical_attack;
    int physical_resistance;
    int mental_attack;
    int mental_resistance;
    int speed;

    int attack_range = 1;
    void Start()
    {
        SetName(Globals.GenerateRandomName(genre));
        SetBaseStats();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LvlUp()
    {
        switch (class_type)
        {
            case ClassType.RECRUIT:
                {
                    max_hp += 6;
                    hp += 6;
                    max_psique += 1;
                    psique += 1;
                    physical_attack += 6;
                    physical_resistance += 5;
                    mental_attack += 4;
                    mental_resistance += 5;
                }
                break;

            case ClassType.AGENT:
                {
                    max_hp += 8;
                    max_psique += 1;
                    hp += 8;
                    psique += 1;
                    physical_attack += 7;
                    physical_resistance += 7;
                    mental_attack += 6;
                    mental_resistance += 7;

                }
                break;

            case ClassType.SPECIALIST:
                {
                    max_hp += 5;
                    max_psique += 4;
                    hp += 5;
                    psique += 4;
                    physical_attack += 6;
                    physical_resistance += 7;
                    mental_attack += 8;
                    mental_resistance += 8;
                }
                break;

            case ClassType.RIOT:
                {
                    max_hp += 7;
                    max_psique += 1;
                    hp += 7;
                    psique += 1;
                    physical_attack += 6;
                    physical_resistance += 8;
                    mental_attack += 5;
                    mental_resistance += 7;
                }
                break;

            case ClassType.SNIPER:
                {
                    max_hp += 6;
                    max_psique += 2;
                    hp += 6;
                    psique += 2;
                    physical_attack += 6;
                    physical_resistance += 7;
                    mental_attack += 6;
                    mental_resistance += 7;
                }
                break;

            case ClassType.COMBAT_MEDIC:
                {
                    max_hp += 6;
                    max_psique += 5;
                    hp += 6;
                    psique += 5;
                    physical_attack += 6;
                    physical_resistance += 7;
                    mental_attack += 8;
                    mental_resistance += 8;
                }
                break;

            case ClassType.ARCANIST:
                {
                    max_hp += 2;
                    max_psique += 4;
                    hp += 2;
                    psique += 4;
                    physical_attack += 6;
                    physical_resistance += 6;
                    mental_attack += 9;
                    mental_resistance += 8;
                }
                break;

            case ClassType.GEO:
                {
                    max_hp += 8;
                    max_psique += 1;
                    hp += 8;
                    psique += 1;
                    physical_attack += 7;
                    physical_resistance += 9;
                    mental_attack += 6;
                    mental_resistance += 7;
                }
                break;

            case ClassType.SPY:
                {
                    max_hp += 7;
                    max_psique += 2;
                    hp += 7;
                    psique += 2;
                    physical_attack += 8;
                    physical_resistance += 6;
                    mental_attack += 6;
                    mental_resistance += 5;
                }
                break;

            case ClassType.WHITE_ARCANIST:
                {
                    max_hp += 4;
                    max_psique += 3;
                    hp += 4;
                    psique += 3;
                    physical_attack += 6;
                    physical_resistance += 6;
                    mental_attack += 8;
                    mental_resistance += 9;
                }
                break;

            case ClassType.MATTER_CONTROLLER:
                {
                    max_hp += 4;
                    max_psique += 3;
                    hp += 4;
                    psique += 3;
                    physical_attack += 6;
                    physical_resistance += 6;
                    mental_attack += 10;
                    mental_resistance += 8;
                }
                break;

            case ClassType.WEAPON_MASTER:
                {
                    max_hp += 8;
                    max_psique += 2;
                    hp += 8;
                    psique += 2;
                    physical_attack += 9;
                    physical_resistance += 8;
                    mental_attack += 6;
                    mental_resistance += 6;
                }
                break;

            case ClassType.GOD:
                {
                    max_hp += 20;
                    max_psique += 7;
                    hp += 20;
                    psique += 7;
                    physical_attack += 9;
                    physical_resistance += 10;
                    mental_attack += 9;
                    mental_resistance += 10;
                }
                break;

        }
    }

    public void SetEnemyUnit()
    {
        is_player_unit = false;
    }

    public void SetPlayerUnit()
    {
        is_player_unit = true;
    }

    public bool IsPlayerUnit()
    {
        return is_player_unit;
    }

    public void SetBaseStats()
    {
        switch (class_type)
        {
            case ClassType.RECRUIT:
                {
                    max_hp = 30;
                    max_psique = 10;
                    hp = 30;
                    psique = 10;
                    physical_attack = 50;
                    physical_resistance = 50;
                    mental_attack = 50;
                    mental_resistance = 50;
                    speed = 100;
                    movement_range = 4;
                    jumpPower = 2;
                }
                break;
            case ClassType.AGENT:
                {
                    max_hp = 40;
                    max_psique = 14;
                    hp = 40;
                    psique = 14;
                    physical_attack = 70;
                    physical_resistance = 70;
                    mental_attack = 60;
                    mental_resistance = 70;
                    speed = 100;
                    movement_range = 4;
                    jumpPower = 2;
                }
                break;

            case ClassType.SPECIALIST:
                {
                    max_hp = 28;
                    max_psique = 36;
                    hp = 28;
                    psique = 36;
                    physical_attack = 64;
                    physical_resistance = 68;
                    mental_attack = 88;
                    mental_resistance = 96;
                    speed = 96;
                    movement_range = 3;
                    jumpPower = 1;
                }
                break;

            case ClassType.RIOT:
                {
                    max_hp = 36;
                    max_psique = 12;
                    hp = 36;
                    psique = 12;
                    physical_attack = 65;
                    physical_resistance = 80;
                    mental_attack = 56;
                    mental_resistance = 68;
                    speed = 104;
                    movement_range = 4;
                    jumpPower = 1;
                }
                break;

            case ClassType.SNIPER:
                {
                    max_hp = 36;
                    max_psique = 18;
                    hp = 36;
                    psique = 18;
                    physical_attack = 72;
                    physical_resistance = 72;
                    mental_attack = 60;
                    mental_resistance = 70;
                    speed = 106;
                    movement_range = 4;
                    jumpPower = 2;
                }
                break;

            case ClassType.COMBAT_MEDIC:
                {
                    max_hp = 30;
                    max_psique = 40;
                    hp = 30;
                    psique = 40;
                    physical_attack = 60;
                    physical_resistance = 73;
                    mental_attack = 84;
                    mental_resistance = 80;
                    speed = 108;
                    movement_range = 3;
                    jumpPower = 1;
                }
                break;

            case ClassType.ARCANIST:
                {
                    max_hp = 26;
                    max_psique = 38;
                    hp = 26;
                    psique = 38;
                    physical_attack = 60;
                    physical_resistance = 64;
                    mental_attack = 92;
                    mental_resistance = 84;
                    speed = 89;
                    movement_range = 3;
                    jumpPower = 1;
                }
                break;

            case ClassType.GEO:
                {
                    max_hp = 35;
                    max_psique = 12;
                    hp = 35;
                    psique = 12;
                    physical_attack = 70;
                    physical_resistance = 88;
                    mental_attack = 60;
                    mental_resistance = 75;
                    speed = 105;
                    movement_range = 3;
                    jumpPower = 2;
                }
                break;

            case ClassType.SPY:
                {
                    max_hp = 34;
                    max_psique = 26;
                    hp = 34;
                    psique = 26;
                    physical_attack = 80;
                    physical_resistance = 68;
                    mental_attack = 60;
                    mental_resistance = 80;
                    speed = 112;
                    movement_range = 4;
                    jumpPower = 2;
                }
                break;

            case ClassType.WHITE_ARCANIST:
                {
                    max_hp = 26;
                    max_psique = 38;
                    hp = 26;
                    psique = 38;
                    physical_attack = 60;
                    physical_resistance = 64;
                    mental_attack = 84;
                    mental_resistance = 92;
                    speed = 108;
                    movement_range = 3;
                    jumpPower = 1;
                }
                break;

            case ClassType.MATTER_CONTROLLER:
                {
                    max_hp = 28;
                    max_psique = 34;
                    hp = 28;
                    psique = 34;
                    physical_attack = 64;
                    physical_resistance = 64;
                    mental_attack = 100;
                    mental_resistance = 80;
                    speed = 90;
                    movement_range = 3;
                    jumpPower = 1;
                }
                break;

            case ClassType.WEAPON_MASTER:
                {
                    max_hp = 40;
                    max_psique = 18;
                    hp = 40;
                    psique = 18;
                    physical_attack = 94;
                    physical_resistance = 88;
                    mental_attack = 59;
                    mental_resistance = 64;
                    speed = 97;
                    movement_range = 4;
                    jumpPower = 2;
                }
                break;

            case ClassType.GOD:
                {
                    max_hp = 100;
                    max_psique = 70;
                    hp = 100;
                    psique = 70;
                    physical_attack = 90;
                    physical_resistance = 95;
                    mental_attack = 90;
                    mental_resistance = 95;
                    speed = 100;
                    movement_range = 4;
                    jumpPower = 3;
                }
                break;

        }
    }

    public void SetSpeed(int s)
    {
        speed = s;
    }

    public void SetClass(ClassType type)
    {
        class_type = type;
    }
    public void SetLvl(int level)
    {
        lvl = level;
        SetBaseStats();
        for (int i = 0; i < lvl; ++i)
        {
            LvlUp();
        }
    }

    public void SetPosition(Vector2Int new_pos)
    {
        position = new_pos;        
    }

    public void SetName(string new_name)
    {
        unit_name = new_name;
        this.name = unit_name;
    }
    public int GetSpeed()
    {
        return speed;
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    public Genre GetGenre()
    {
        return genre;
    }

    public int GetAttackRange()
    {
        return attack_range;
    }

    public int GetMovementRange()
    {
        return movement_range;
    }

    public int GetJumpRange()
    {
        return jumpPower;
    }

    public string GetName()
    {
        return unit_name;
    }

    public int GetMaxHP()
    {
        return max_hp;
    }

    public int GetCurrentHP()
    {
        return hp;
    }

    public int GetMaxPsique()
    {
        return max_psique;
    }

    public int GetCurrentPsique()
    {
        return psique;
    }
}