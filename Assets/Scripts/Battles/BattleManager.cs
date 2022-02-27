using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<Unit> TurnOrder;
    BattleMap battleMap = new BattleMap();
    public GameObject ActionSelectorGO;

    public GameObject FirstMenu;
    public GameObject MoveSelectorGO;
    public GameObject AttackSelectorGO;
    public GameObject SkillSelectorGO;
    public GameObject ItemSelectorGO;
    public GameObject WaitSelectorGO;


    public GameObject PlayerUnitPrefab;
    public GameObject EnemyUnitPrefab;

    bool menu_is_reset = false;

    enum BattleState
    {
        SET,
        START,
        BATTLE,
        VICTORY,
        DEFEAT
    }

    int optionIndex;

    BattleState battleState;

    void Start()
    {
        optionIndex = 0;
        battleState = BattleState.SET;
        battleMap.SetSize(6, 5);
        battleMap.SetMap();
        SetTurnOrder();
        InstantiateUnits();

        battleState = BattleState.START;
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleState)
        {
            case (BattleState.START):
                {
                    //ui animations for battle start

                    battleState = BattleState.BATTLE;
                }
                break;


            case (BattleState.BATTLE):
                {
                    for (int i = 0; i < TurnOrder.Count; ++i)
                    {
                        if (TurnOrder[i].IsPlayerUnit())
                        {
                            if(!menu_is_reset)
                            ResetActionMenu();

                            FirstMenuNav();
                        }
                    }
                }
                break;
        }
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

    public void AddUnits(List<Unit> units)
    {
        for (int i = 0; i < units.Count; ++i)
        {
            if (units[i].IsPlayerUnit())
                units[i].SetSpeed(190);
            TurnOrder.Add(units[i]);
        }
    }

    public BattleMap GetBattleMap()
    {
        return battleMap;
    }

    void InstantiateUnits()
    {
        for (int i = 0; i < TurnOrder.Count; ++i)
        {

            Vector3 new_position = new Vector3(TurnOrder[i].GetPosition().x * Globals.TILE_SIZE, 0, TurnOrder[i].GetPosition().y * Globals.TILE_SIZE);
            if (TurnOrder[i].IsPlayerUnit())
                Instantiate(PlayerUnitPrefab, new_position, Quaternion.identity);

            else
                Instantiate(EnemyUnitPrefab, new_position, Quaternion.identity);
        }
    }

    void PlayerActionSelect()
    {

    }

    void EnemyTurn()
    {

    }

    void EnemyMovement()
    {
        //IA to decide where to move
    }

    void EnemyAction()
    {
        //Attack
        //Cast skill
        //Item
        //Wait
    }

    void ResetActionMenu()
    {
        ActionSelectorGO.transform.position = MoveSelectorGO.transform.position;
        menu_is_reset = true;
    }

    void UInavigation()
    {
        if (FirstMenu.activeInHierarchy)
            FirstMenuNav();
    }


    void FirstMenuNav()
    {
        if (optionIndex == 0)
            ActionSelectorGO.transform.position = MoveSelectorGO.transform.position;

        else if (optionIndex == 1)
            ActionSelectorGO.transform.position = AttackSelectorGO.transform.position;

        else if (optionIndex == 2)
            ActionSelectorGO.transform.position = SkillSelectorGO.transform.position;

        else if (optionIndex == 3)
            ActionSelectorGO.transform.position = ItemSelectorGO.transform.position;

        else if (optionIndex == 4)
            ActionSelectorGO.transform.position = WaitSelectorGO.transform.position;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (optionIndex != 0)
                --optionIndex;
            else
                optionIndex = 4;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (optionIndex != 4)
                ++optionIndex;
            else
                optionIndex = 0;
        }
    }

}
