using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    int MAX_UNITS = 6;

    public List<Unit> TurnOrder;
    BattleMap battleMap;
    GameObject gameManagerGO;
    GameManager gameManager;

    

    void Start()
    {
        battleMap = new BattleMap();
        battleMap.SetSize(6, 5);
        battleMap.SetMap();
    }

    // Update is called once per frame
    void Update()
    {
        SetTurnOrder();
    }

    void SetTurnOrder()
    {
        for (int i = 0; i < TurnOrder.Count; ++i)
        {
            if (i < TurnOrder.Count-1 && TurnOrder[i].GetSpeed() < TurnOrder[i + 1].GetSpeed())
            {
                Unit temp = TurnOrder[i];
                TurnOrder[i] = TurnOrder[i + 1];

                TurnOrder[i + 1] = temp;
                i = 0;
            }
        }
    }

    public void AddUnits(List<Unit> playerUnits, List <Unit> enemyUnits)
    {
        for (int i = 0; i < playerUnits.Count; ++i)
        {
            TurnOrder.Add(playerUnits[i]);
        }

        for (int i = 0; i < enemyUnits.Count; ++i)
        {
            TurnOrder.Add(enemyUnits[i]);
        }
    }
}
