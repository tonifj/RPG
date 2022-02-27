using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<Unit> TurnOrder;
    int current_turn;

    BattleMap battleMap = new BattleMap();

    GameObject battleCameraGO;
    BattleCamera battleCamera;


    public GameObject ActionSelectorGO;

    public GameObject BattleUI;
    public GameObject FirstMenu;
    public GameObject MoveSelectorGO;
    public GameObject AttackSelectorGO;
    public GameObject SkillSelectorGO;
    public GameObject ItemSelectorGO;
    public GameObject WaitSelectorGO;


    public GameObject PlayerUnitPrefab;
    public GameObject EnemyUnitPrefab;

    public Material SelectableTilesMaterial;

    bool menu_is_reset = false;
    bool enemy_turn_finished = true;
    bool is_turn_unit_ally = false;


    enum BattleState
    {
        SET,
        START,
        BATTLE,
        VICTORY,
        DEFEAT
    }

    enum CurrentSubmenu
    {
        FIRST,
        MOVE,
        ATTACK,
        SKILL,
        ITEM,
        WAIT,
        NONE
    }

    int optionIndex;

    BattleState battleState;
    CurrentSubmenu currentSubmenu;
    void Start()
    {
        currentSubmenu = CurrentSubmenu.FIRST;

        battleCameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        battleCamera = battleCameraGO.GetComponent<BattleCamera>();

        optionIndex = 0;
        battleState = BattleState.SET;
        battleMap.SetSize(6, 5);
        battleMap.SetMap();
        SetTurnOrder();
        InstantiateUnits();

        battleState = BattleState.START;

        battleCamera.SetTarget(battleMap.GetTile(TurnOrder[current_turn].GetPosition()).transform);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentSubmenu);
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
                    if (TurnOrder[current_turn].IsPlayerUnit())
                    {
                        PlayerTurn();
                    }

                    else
                    {
                        if (enemy_turn_finished)
                            StartCoroutine(EnemyTurn());

                    }
                }
                break;
        }

        battleCamera.SetTarget(battleMap.GetTile(TurnOrder[current_turn].GetPosition()).transform);

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

        ShowBattleUI();
        ShowFirstMenu();
        currentSubmenu = CurrentSubmenu.FIRST;
        optionIndex = 0;

        //set all other submenus to false
        ActionSelectorGO.transform.position = MoveSelectorGO.transform.position;


    }

    void HandleSubmenus()
    {
        switch (currentSubmenu)
        {
            case CurrentSubmenu.FIRST:
                {
                    FirstMenuNav();
                }
                break;

            case CurrentSubmenu.MOVE:
                {
                    MoveAction();
                }
                break;

            case CurrentSubmenu.ATTACK:
                {
                    AttackAction();
                }
                break;
            case CurrentSubmenu.SKILL:
                {
                    AttackAction();
                }
                break;
            case CurrentSubmenu.ITEM:
                {
                    AttackAction();
                }
                break;
            case CurrentSubmenu.WAIT:
                {
                    WaitAction();

                }
                break;

        }
    }

    void FirstMenuNav()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) //so it doesn't iterate all the time inside this
        {
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

        switch (optionIndex)
        {
            case 0:
                {
                    ActionSelectorGO.transform.position = MoveSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.MOVE;
                    }
                }
                break;

            case 1:
                {
                    ActionSelectorGO.transform.position = AttackSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.ATTACK;
                    }
                }
                break;

            case 2:
                {
                    ActionSelectorGO.transform.position = SkillSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.SKILL;
                    }
                }
                break;

            case 3:
                {
                    ActionSelectorGO.transform.position = ItemSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.ITEM;
                    }
                }
                break;

            case 4:
                {
                    ActionSelectorGO.transform.position = WaitSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.WAIT;
                    }
                }
                break;

            default: //in case there is some error
                {
                    ActionSelectorGO.transform.position = MoveSelectorGO.transform.position;
                    optionIndex = 0;
                }
                break;
        }
    }


    void MoveAction()
    {
        HideBattleUI();
        battleMap.ActionTileSelection(battleMap.GetTile(TurnOrder[current_turn].GetPosition()), TurnOrder[current_turn].GetMovementRange(), SelectableTilesMaterial);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            battleMap.ResetMaterials(battleMap.GetTile(TurnOrder[current_turn].GetPosition()), TurnOrder[current_turn].GetMovementRange());
            ShowBattleUI();
            currentSubmenu = CurrentSubmenu.FIRST;

        }
    }

    void AttackAction()
    {
        HideBattleUI();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) //so it doesn't iterate all the time inside this
        {

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowBattleUI();
            currentSubmenu = CurrentSubmenu.FIRST;

        }
    }

    void SkillAction()
    {
        BattleUI.SetActive(false);

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) //so it doesn't iterate all the time inside this
        {

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowFirstMenu();
            currentSubmenu = CurrentSubmenu.FIRST;

        }
    }

    void ItemAction()
    {
        HideFirstMenu();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) //so it doesn't iterate all the time inside this
        {

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowBattleUI();
            currentSubmenu = CurrentSubmenu.FIRST;

        }
    }

    void WaitAction()
    {
        HideBattleUI();

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) //so it doesn't iterate all the time inside this
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EndTurn();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowBattleUI();
            currentSubmenu = CurrentSubmenu.FIRST;

        }


    }

    void HideBattleUI()
    {
        BattleUI.SetActive(false);
    }

    void ShowBattleUI()
    {
        BattleUI.SetActive(true);
    }

    void HideFirstMenu()
    {
        FirstMenu.SetActive(false);
    }

    void ShowFirstMenu()
    {
        FirstMenu.SetActive(true);
    }

    void PlayerTurn()
    {
        HandleSubmenus();
    }

    void EndTurn()
    {
        if (current_turn >= TurnOrder.Count - 1)
            current_turn = 0;
        else
            ++current_turn;

        if (TurnOrder[current_turn].IsPlayerUnit())
        {
            ResetActionMenu();
            menu_is_reset = false;
        }

    }

    IEnumerator EnemyTurn()
    {
        enemy_turn_finished = false;
        yield return new WaitForSeconds(2);
        EndTurn();
        enemy_turn_finished = true;

    }

}
