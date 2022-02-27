using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Globals : MonoBehaviour
{
    public static int TILE_SIZE = 3;
    public static int MAX_BATTLE_UNITS = 6;

    public static string GenerateRandomName(Genre genre)
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

                    return male_names[Random.Range(0, male_names.Length)];
                }

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

                    return female_names[Random.Range(0, female_names.Length)];

                }

            default:
                return "";
        }

    }
}
