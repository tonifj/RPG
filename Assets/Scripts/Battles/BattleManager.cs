using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{


    //GO
    public GameObject ActionSelectorGO;
    public GameObject BattleUI;
    public GameObject FirstMenu;
    public GameObject MoveSelectorGO;
    public GameObject AttackSelectorGO;
    public GameObject SkillSelectorGO;
    public GameObject ItemSelectorGO;
    public GameObject WaitSelectorGO;

    //Prefabs
    public GameObject PlayerUnitPrefab;
    public GameObject EnemyUnitPrefab;

    //Materials
    public Material SelectableTilesMaterial;
    public Material SelectedTileMaterial;

    //booleans
    bool menu_is_reset = false;
    bool enemy_turn_finished = true;
    bool is_turn_unit_ally = false;
    bool action_tile_reseted = false;

    //enums
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

    BattleState battleState;
    CurrentSubmenu currentSubmenu;

    //other variables
    int optionIndex;
    int current_turn;

    public List<GameObject> TurnOrder;

    BattleMap battleMap = new BattleMap();

    GameObject battleCameraGO;
    BattleCamera battleCamera;

    Vector2Int action_tile; //used to select a tile where to cast an action

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

        battleCamera.SetTarget(TurnOrder[current_turn].transform);

    }

    // Update is called once per frame
    void Update()
    {
        battleCamera.SetTarget(TurnOrder[current_turn].transform);

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
                    if (TurnOrder[current_turn].GetComponent<Unit>().IsPlayerUnit())
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


    }

    void SetTurnOrder()
    {
        for (int i = 0; i < TurnOrder.Count; ++i)
        {
            if (i < TurnOrder.Count - 1 && TurnOrder[i].GetComponent<Unit>().GetSpeed() < TurnOrder[i + 1].GetComponent<Unit>().GetSpeed())
            {
                GameObject temp = TurnOrder[i];
                TurnOrder[i] = TurnOrder[i + 1];

                TurnOrder[i + 1] = temp;
                i = 0;
            }
        }
    }

    public void AddUnits(List<GameObject> units)
    {
        for (int i = 0; i < units.Count; ++i)
        {
            if (units[i].GetComponent<Unit>().IsPlayerUnit())
                units[i].GetComponent<Unit>().SetSpeed(190);
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

            Vector3 new_position = new Vector3(TurnOrder[i].GetComponent<Unit>().GetPosition().x * Globals.TILE_SIZE, 0, TurnOrder[i].GetComponent<Unit>().GetPosition().y * Globals.TILE_SIZE);
            if (TurnOrder[i].GetComponent<Unit>().IsPlayerUnit())
            {
                GameObject new_player_unit = Instantiate(PlayerUnitPrefab, new_position, Quaternion.identity);
                new_player_unit.transform.SetParent(TurnOrder[i].transform);
            }

            else
            {
                GameObject new_enemy_unit = Instantiate(EnemyUnitPrefab, new_position, Quaternion.identity);
                new_enemy_unit.transform.SetParent(TurnOrder[i].transform);
            }

            battleMap.GetTile(TurnOrder[i].GetComponent<Unit>().GetPosition()).SetOccupiedByPlayerUnit(TurnOrder[i].GetComponent<Unit>().IsPlayerUnit());
            battleMap.OccupyTile(battleMap.GetTile(TurnOrder[i].GetComponent<Unit>().GetPosition()));

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

        //paint all tiles where the movement is possible
        battleMap.ActionTileSelection(battleMap.GetTile(TurnOrder[current_turn].GetComponent<Unit>().GetPosition()), TurnOrder[current_turn].GetComponent<Unit>().GetMovementRange(), SelectableTilesMaterial);

        //unpaint all tiles where the movement is not possible
        //HERE
        //paint the current selected tile with a different color
        SelectTileForAction();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!battleMap.GetTile(action_tile).IsOccupied())
            {
                //Reset tile materials
                battleMap.ResetMaterials(battleMap.GetTile(TurnOrder[current_turn].GetComponent<Unit>().GetPosition()), TurnOrder[current_turn].GetComponent<Unit>().GetMovementRange());

                //free the previous tile
                FreeTile(battleMap.GetTile(TurnOrder[current_turn].GetComponent<Unit>().GetPosition()));

                //move unit to the tile
                MoveUnitToTile(action_tile);
            }
            
        }

        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Reset tile material
            battleMap.ResetMaterials(battleMap.GetTile(TurnOrder[current_turn].GetComponent<Unit>().GetPosition()), TurnOrder[current_turn].GetComponent<Unit>().GetMovementRange());

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
        if (!action_tile_reseted)
        {
            action_tile = TurnOrder[current_turn].GetComponent<Unit>().GetPosition();
            action_tile_reseted = true;
        }

        HandleSubmenus();
    }

    void EndTurn()
    {
        if (current_turn >= TurnOrder.Count - 1)
            current_turn = 0;
        else
            ++current_turn;

        if (TurnOrder[current_turn].GetComponent<Unit>().IsPlayerUnit())
        {
            ResetActionMenu();
            menu_is_reset = false;
        }

        action_tile_reseted = false;

    }

    IEnumerator EnemyTurn()
    {
        action_tile = TurnOrder[current_turn].GetComponent<Unit>().GetPosition(); //reset action tile to the turn unit's position
        enemy_turn_finished = false;
        yield return new WaitForSeconds(2);
        EndTurn();
        enemy_turn_finished = true;

    }

    void SelectTileForAction()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) //so it doesn't iterate all the time inside this
        {
            Vector2Int tile_to_select = action_tile;
            ++tile_to_select.y;

            if (battleMap.IsValidTileMovement(tile_to_select, TurnOrder[current_turn].GetComponent<Unit>().IsPlayerUnit()) && battleMap.GetTile(tile_to_select).GetMaterial().color == SelectableTilesMaterial.color)
                ++action_tile.y;
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow)) //so it doesn't iterate all the time inside this
        {
            Vector2Int tile_to_select = action_tile;
            --tile_to_select.y;

            if (battleMap.IsValidTileMovement(tile_to_select, TurnOrder[current_turn].GetComponent<Unit>().IsPlayerUnit()) && battleMap.GetTile(tile_to_select).GetMaterial().color == SelectableTilesMaterial.color)
                --action_tile.y;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow)) //so it doesn't iterate all the time inside this
        {
            Vector2Int tile_to_select = action_tile;
            --tile_to_select.x;

            if (battleMap.IsValidTileMovement(tile_to_select, TurnOrder[current_turn].GetComponent<Unit>().IsPlayerUnit()) && battleMap.GetTile(tile_to_select).GetMaterial().color == SelectableTilesMaterial.color)
                --action_tile.x;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) //so it doesn't iterate all the time inside this
        {
            Vector2Int tile_to_select = action_tile;
            ++tile_to_select.x;

            if (battleMap.IsValidTileMovement(tile_to_select, TurnOrder[current_turn].GetComponent<Unit>().IsPlayerUnit()) && battleMap.GetTile(tile_to_select).GetMaterial().color == SelectableTilesMaterial.color)
                ++action_tile.x;
        }

        battleMap.HighlightTile(battleMap.GetTile(action_tile), SelectedTileMaterial);
        battleCamera.SetTarget(battleMap.GetTile(action_tile).gameObject.transform);

    }

    void MoveUnitToTile(Vector2Int new_pos)
    {
        //occupy the tile
        battleMap.GetTile(action_tile).SetOccupied(true);

        //if the tile is occupied by a player unit, indicate it
        battleMap.GetTile(action_tile).SetOccupiedByPlayerUnit(TurnOrder[current_turn].GetComponent<Unit>().IsPlayerUnit());

        //Set the tilepos
        Vector3 new_position = new Vector3(new_pos.x * Globals.TILE_SIZE, 0, new_pos.y * Globals.TILE_SIZE);
        TurnOrder[current_turn].GetComponent<Unit>().SetPosition(new_pos);

        //set the new world pos
        TurnOrder[current_turn].transform.position = new_position;
        EndTurn();
    }

    void FreeTile(Tile t)
    {
        t.SetOccupied(false);
        t.SetOccupiedByPlayerUnit(false);

    }

}
