using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> playerUnits = new List<GameObject>();
    public List<Mission> missions = new List<Mission>();

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    GameObject battleManagerGO;
    BattleManager battleManager;


    Mission[] Missions;
    void Start()
    {


        battleManagerGO = GameObject.FindGameObjectWithTag("battle manager");
        battleManager = battleManagerGO.GetComponent<BattleManager>();

        //Mission 1
        //Mission introduction = CreateMission(1, 50);
        //introduction.CreateEnemyUnit(ClassType.SNIPER, Genre.FEMALE, 1);
        //introduction.PlaceUnit(introduction.GetEnemyUnits()[0], new Vector2Int(2, 2));
        //introduction.CreateEnemyUnit(ClassType.SNIPER, Genre.FEMALE, 1);
        //introduction.PlaceUnit(introduction.GetEnemyUnits()[1], new Vector2Int(5, 3));
        //introduction.CreateEnemyUnit(ClassType.SNIPER, Genre.FEMALE, 1);
        //introduction.PlaceUnit(introduction.GetEnemyUnits()[2], new Vector2Int(5, 2));

        //CreatePlayerUnit(ClassType.RECRUIT, Genre.MALE, 1);

        //for (int i = 0; i < playerUnits.Count; ++i)
            //introduction.PlaceUnit(playerUnits[i], new Vector2Int(i + 1, 0));

       // missions.Add(introduction);

        //battleManager.AddUnits(playerUnits);
        //battleManager.AddUnits(introduction.GetEnemyUnits());
    }

    // Update is called once per frame
    void Update()
    {

    }

    Mission CreateMission(int id, int pesetas_reward)
    {
        Mission mission = new Mission();
        mission.SetMission(id, pesetas_reward);
        return mission;
    }

    void CreatePlayerUnit(ClassType type, Genre genre, int lvl)
    {
        GameObject new_unit = playerPrefab;
        new_unit.GetComponent<Unit>().SetSpeed(190);
        new_unit.GetComponent<Unit>().SetName(Globals.GenerateRandomName(genre));
        playerUnits.Add(new_unit);
    }



}
