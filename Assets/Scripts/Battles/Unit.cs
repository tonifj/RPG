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
    public string name;
    GameObject model;

    ClassType class_type;
    Genre genre;

    Vector2Int position;

    public bool is_player_unit;

    int movement_range;
    int lvl;
    int exp;

    int hp;
    int psique;
    int physical_attack;
    int physical_resistance;
    int mental_attack;
    int mental_resistance;
    int speed;
    void Start()
    {
        is_player_unit = true;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(position.x * Globals.TILE_SIZE, 0, position.y * Globals.TILE_SIZE);
    }
    void LvlUp()
    {
        switch (class_type)
        {
            case ClassType.RECRUIT:
                {
                    hp += 6;
                    psique += 1;
                    physical_attack += 6;
                    physical_resistance += 5;
                    mental_attack += 4;
                    mental_resistance += 5;
                }
                break;

            case ClassType.AGENT:
                {
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

    public bool IsPlayerUnit()
    {
        return is_player_unit;
    }

    void SetBaseStats()
    {
        switch (class_type)
        {
            case ClassType.RECRUIT:
                {
                    hp = 30;
                    psique = 10;
                    physical_attack = 50;
                    physical_resistance = 50;
                    mental_attack = 50;
                    mental_resistance = 50;
                    speed = 100;
                    movement_range = 4;
                }
                break;
            case ClassType.AGENT:
                {
                    hp = 40;
                    psique = 14;
                    physical_attack = 70;
                    physical_resistance = 70;
                    mental_attack = 60;
                    mental_resistance = 70;
                    speed = 100;
                    movement_range = 4;
                }
                break;

            case ClassType.SPECIALIST:
                {
                    hp = 28;
                    psique = 36;
                    physical_attack = 64;
                    physical_resistance = 68;
                    mental_attack = 88;
                    mental_resistance = 96;
                    speed = 96;
                    movement_range = 3;
                }
                break;

            case ClassType.RIOT:
                {
                    hp = 36;
                    psique = 12;
                    physical_attack = 65;
                    physical_resistance = 80;
                    mental_attack = 56;
                    mental_resistance = 68;
                    speed = 104;
                    movement_range = 4;
                }
                break;

            case ClassType.SNIPER:
                {
                    hp = 36;
                    psique = 18;
                    physical_attack = 72;
                    physical_resistance = 72;
                    mental_attack = 60;
                    mental_resistance = 70;
                    speed = 106;
                    movement_range = 4;
                }
                break;

            case ClassType.COMBAT_MEDIC:
                {
                    hp = 30;
                    psique = 40;
                    physical_attack = 60;
                    physical_resistance = 73;
                    mental_attack = 84;
                    mental_resistance = 80;
                    speed = 108;
                    movement_range = 3;
                }
                break;

            case ClassType.ARCANIST:
                {
                    hp = 26;
                    psique = 38;
                    physical_attack = 60;
                    physical_resistance = 64;
                    mental_attack = 92;
                    mental_resistance = 84;
                    speed = 89;
                    movement_range = 3;
                }
                break;

            case ClassType.GEO:
                {
                    hp = 35;
                    psique = 12;
                    physical_attack = 70;
                    physical_resistance = 88;
                    mental_attack = 60;
                    mental_resistance = 75;
                    speed = 105;
                    movement_range = 3;
                }
                break;

            case ClassType.SPY:
                {
                    hp = 34;
                    psique = 26;
                    physical_attack = 80;
                    physical_resistance = 68;
                    mental_attack = 60;
                    mental_resistance = 80;
                    speed = 112;
                    movement_range = 4;
                }
                break;

            case ClassType.WHITE_ARCANIST:
                {
                    hp = 26;
                    psique = 38;
                    physical_attack = 60;
                    physical_resistance = 64;
                    mental_attack = 84;
                    mental_resistance = 92;
                    speed = 108;
                    movement_range = 3;
                }
                break;

            case ClassType.MATTER_CONTROLLER:
                {
                    hp = 28;
                    psique = 34;
                    physical_attack = 64;
                    physical_resistance = 64;
                    mental_attack = 100;
                    mental_resistance = 80;
                    speed = 90;
                    movement_range = 3;
                }
                break;

            case ClassType.WEAPON_MASTER:
                {
                    hp = 40;
                    psique = 18;
                    physical_attack = 94;
                    physical_resistance = 88;
                    mental_attack = 59;
                    mental_resistance = 64;
                    speed = 97;
                    movement_range = 4;
                }
                break;

            case ClassType.GOD:
                {
                    hp = 100;
                    psique = 70;
                    physical_attack = 90;
                    physical_resistance = 95;
                    mental_attack = 90;
                    mental_resistance = 95;
                    speed = 100;
                    movement_range = 4;
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
    public int GetSpeed()
    {
        return speed;
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    public void GenerateRandomName(Genre genre)
    {
        switch (genre)
        {
            case Genre.MALE:
                {
                    string[] male_names =
                    {
                        "Hugo","Mart�n","Lucas","Mateo","Lle�","Daniel","Alejandro","Pablo","Manuel","�lvaro","Adri�n","David","Mario",
                         "Enzo","Diego","Marcos","Izan","Javier","Marco","�lex","Bruno","Oliver","Miquel","Thiago","Antonio","Marc","Carlos",
                         "�ngel","Juan","Gonzalo","Gael", "Sergio", "Nico" ,"Tob�as","Gabi","Jorge","Jos�","Adam","Liam","Eric","Samuel","Dar�o","H�ctor",
                         "Luca","Iker","Amir","Rodrigo","Sa�l","V�ctor","Francisco","Iv�n","Jes�s","Jaime","Aar�n","Rub�n","Ian","Guille","Erik","Mohamed",
                         "Julen","Lu�s","Pau","Unai","Rafa","Joel","Alberto","Pedro","Ra�l","Aitor","Santi","Roberto","Pol","Nil","Noah","Jan","Asier","Fer",
                         "Alonso","Mat�as","Biel","Andr�s","Axel","Ismael","Mart�","Arnau","Imran","Luka","Ignacio","Aleix","Alan","El�as","Omar","Isaac",
                         "Pepe","Jon","Teo","Mauro","�scar","Cristian","Leo"
                    };

                    name = male_names[Random.Range(0, male_names.Length)];
                }

                break;

            case Genre.FEMALE:
                {
                    string[] female_names =
                        {
                            "Luc�a","Sof�a","Martina","Mar�a","Julia","Paula","Valeria","Emma","Daniela","Carla","Alba","Noa","Alma","Sara",
                             "Carmen","Vega","Lara","Mia","Valentina","Olivia ","Claudia","Jimena","Lola","Chl�e","Aitana","Abril","Ana","Laia",
                             "Aura","Candela","Alejandra","Elena","Vera","Manuela","Adriana","In�s","Marta","Carlota","Irene","Vicky","Blanca",
                             "Marina","Laura","Roc�o","Alicia","Clara","Nora","L�a","Ariadna","Zoe","Samira","Marga","Celia","Leire","Eva","�ngela",
                             "Andrea","�frica","Luna","Ainhoa","Ainara","India","Nerea","Ona","Elsa","Isabel","Leyre","Gabriela","Aina","Aida","Iria",
                             "Ona","Mar","Goretti","Lina","Mariona","Adara","Naia","Iris","Maria","Mara","Elena","Yasmina","Natalia","Arlet","Diana",
                             "Aroa","Amaia","Cristina","Nahia","Isabella","Malak","Elia","Carolina","Berta","F�tima","Nuria","Azahara","Macarena","Aurora",
                             "Gaia", "Gina", "Irati", "Jade","Lila", "Eris"
                    };

                    name = female_names[Random.Range(0, female_names.Length)];

                }
                break;
        }

    }

}