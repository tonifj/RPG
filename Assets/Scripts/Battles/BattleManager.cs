using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    int MAX_UNITS = 6;

    Mission mission;
    public Unit[] TurnOrder;
    GameObject gameManagerGO;
    GameManager gameManager;

    void Start()
    {
        gameManagerGO = GameObject.FindGameObjectWithTag("game manager");
        gameManager = gameManagerGO.GetComponent<GameManager>();
        TurnOrder = mission.GetEnemyUnits();
        for(int i = 0; i < gameManager.PlayerUnits.Length; ++i)
        {
            TurnOrder[i + MAX_UNITS] = gameManager.PlayerUnits[i];
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
