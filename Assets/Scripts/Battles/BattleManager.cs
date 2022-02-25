using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    int MAX_UNITS = 6;

    Mission mission;
    public List<Unit> TurnOrder;
    GameObject gameManagerGO;
    GameManager gameManager;

    void Start()
    {
        gameManagerGO = GameObject.FindGameObjectWithTag("game manager");
        gameManager = gameManagerGO.GetComponent<GameManager>();
               
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetTurnOrder()
    {
        //for (int i = 0; i < TurnOrder.Length; ++i)
        //{
        //    if (i < TurnOrder.Length && TurnOrder[i].GetSpeed() < TurnOrder[i + 1].GetSpeed())
        //    {
        //        Unit temp = TurnOrder[i];
        //        TurnOrder[i] = TurnOrder[i + 1];

        //        TurnOrder[i + 1] = temp;
        //        i = 0;
        //    }
        //}
    }
}
