using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    int MAX_UNITS = 6;

    public List<Unit> TurnOrder;
    BattleMap battleMap = new BattleMap();
    GameObject gameManagerGO;
    GameManager gameManager;
    Mission current_mission;

    public GameObject PlayerUnitPrefab;
    public GameObject EnemyUnitPrefab;

    enum BattleState
    {
        SET,
        START,
        BATTLE,
        VICTORY,
        DEFEAT
    }

    BattleState battleState;

    void Start()
    {
        battleState = BattleState.SET;
        battleMap.SetSize(6, 5);
        battleMap.SetMap();
        SetTurnOrder();

        for (int i = 0; i < TurnOrder.Count; ++i)
        {

            Vector3 new_position = new Vector3(TurnOrder[i].GetPosition().x * Globals.TILE_SIZE, 0, TurnOrder[i].GetPosition().y * Globals.TILE_SIZE);
            if (TurnOrder[i].IsPlayerUnit())
                Instantiate(PlayerUnitPrefab, new_position, Quaternion.identity);

            else
                Instantiate(EnemyUnitPrefab, new_position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetTurnOrder()
    {
        for (int i = 0; i < TurnOrder.Count; ++i)
        {
            if (i < TurnOrder.Count - 1 && TurnOrder[i].GetSpeed() < TurnOrder[i + 1].GetSpeed())
            {
                Unit temp = TurnOrder[i];
                TurnOrder[i] = TurnOrder[i + 1];

                TurnOrder[i + 1] = temp;
                i = 0;
            }
        }
    }

    public void AddUnits(List<Unit> playerUnits, List<Unit> enemyUnits)
    {
        for (int i = 0; i < playerUnits.Count; ++i)
        {
            playerUnits[i].SetPlayerUnit();
            TurnOrder.Add(playerUnits[i]);
        }

        for (int i = 0; i < enemyUnits.Count; ++i)
        {
            enemyUnits[i].SetEnemyUnit();
            TurnOrder.Add(enemyUnits[i]);
        }
    }

    public void SetMission(Mission mission)
    {
        current_mission = mission;
    }

    public BattleMap GetBattleMap()
    {
        return battleMap;
    }
}
