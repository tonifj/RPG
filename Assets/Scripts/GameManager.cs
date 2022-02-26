using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Unit> playerUnits = new List<Unit>();
    public List<Mission> missions = new List<Mission>();

    GameObject battleManagerGO;
    BattleManager battleManager;


    Mission[] Missions;
    void Start()
    {
        battleManagerGO = GameObject.FindGameObjectWithTag("battle manager");
        battleManager = battleManagerGO.GetComponent<BattleManager>();

        //Mission 1
        Mission introduction = CreateMission(1, 50);
        introduction.CreateEnemyUnit(ClassType.SNIPER, Genre.FEMALE, 1);
        introduction.PlaceUnit(introduction.GetEnemyUnits()[0], new Vector2Int(5, 4));
        introduction.CreateEnemyUnit(ClassType.SNIPER, Genre.FEMALE, 1);
        introduction.PlaceUnit(introduction.GetEnemyUnits()[1], new Vector2Int(5, 3));
        introduction.CreateEnemyUnit(ClassType.SNIPER, Genre.FEMALE, 1);
        introduction.PlaceUnit(introduction.GetEnemyUnits()[2], new Vector2Int(5, 2));
        missions.Add(introduction);

        battleManager.AddUnits(playerUnits, introduction.GetEnemyUnits());
 
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

    void PlayerWin()
    {

    }

    void PlayerLose()
    {

    }
  

}
