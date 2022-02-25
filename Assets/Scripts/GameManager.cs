using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Unit> playerUnits = new List<Unit>();
    public List<Mission> missions = new List<Mission>();


    Mission[] Missions;
    void Start()
    {
        //Mission 1
        Mission introduction = CreateMission(1, 50);
        introduction.CreateEnemyUnit(ClassType.RECRUIT, 1);
        introduction.PlaceUnit(introduction.GetEnemyUnits()[0], new Vector2Int(2, 2));
        missions.Add(introduction);

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
