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
                        "Hugo","Martín","Lucas","Mateo","Lleó","Daniel","Alejandro","Pablo","Manuel","Álvaro","Adrián","David","Mario",
                         "Enzo","Diego","Marcos","Izan","Javier","Marco","Álex","Bruno","Oliver","Miquel","Thiago","Antonio","Marc","Carlos",
                         "Ángel","Juan","Gonzalo","Gael", "Sergio", "Nico" ,"Tobías","Gabi","Jorge","José","Adam","Liam","Eric","Samuel","Darío","Héctor",
                         "Luca","Iker","Amir","Rodrigo","Saúl","Víctor","Francisco","Iván","Jesús","Jaime","Aarón","Rubén","Ian","Guille","Erik","Mohamed",
                         "Julen","Luís","Pau","Unai","Rafa","Joel","Alberto","Pedro","Raúl","Aitor","Santi","Roberto","Pol","Nil","Noah","Jan","Asier","Fer",
                         "Alonso","Matías","Biel","Andrés","Axel","Ismael","Martí","Arnau","Imran","Luka","Ignacio","Aleix","Alan","Elías","Omar","Isaac",
                         "Pepe","Jon","Teo","Mauro","Óscar","Cristian","Leo"
                    };

                    return male_names[Random.Range(0, male_names.Length)];
                }

            case Genre.FEMALE:
                {
                    string[] female_names =
                        {
                            "Lucía","Sofía","Martina","María","Julia","Paula","Valeria","Emma","Daniela","Carla","Alba","Noa","Alma","Sara",
                             "Carmen","Vega","Lara","Mia","Valentina","Olivia ","Claudia","Jimena","Lola","Chlóe","Aitana","Abril","Ana","Laia",
                             "Aura","Candela","Alejandra","Elena","Vera","Manuela","Adriana","Inés","Marta","Carlota","Irene","Vicky","Blanca",
                             "Marina","Laura","Rocío","Alicia","Clara","Nora","Lía","Ariadna","Zoe","Samira","Marga","Celia","Leire","Eva","Ángela",
                             "Andrea","África","Luna","Ainhoa","Ainara","India","Nerea","Ona","Elsa","Isabel","Leyre","Gabriela","Aina","Aida","Iria",
                             "Ona","Mar","Goretti","Lina","Mariona","Adara","Naia","Iris","Maria","Mara","Elena","Yasmina","Natalia","Arlet","Diana",
                             "Aroa","Amaia","Cristina","Nahia","Isabella","Malak","Elia","Carolina","Berta","Fátima","Nuria","Azahara","Macarena","Aurora",
                             "Gaia", "Gina", "Irati", "Jade","Lila", "Eris"
                    };

                    return female_names[Random.Range(0, female_names.Length)];

                }

            default:
                return "";
        }

    }
}
