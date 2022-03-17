using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    //Important
    int battle_id;
    int pesetas_reward;
    //Equipment reward

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
    public int current_turn;

    public List<GameObject> TurnOrder;
    public GameObject[] EnemyUnits;
    public GameObject[] PlayerUnits;


    BattleMap battleMap = new BattleMap();

    GameObject battleCameraGO;
    BattleCamera battleCamera;

    Vector2Int action_tile; //used to select a tile where to cast an action

    public static bool isPlayerTurn;


    void Start()
    {
        currentSubmenu = CurrentSubmenu.FIRST;

        battleCameraGO = GameObject.FindGameObjectWithTag("MainCamera");
        battleCamera = battleCameraGO.GetComponent<BattleCamera>();

        EnemyUnits = new GameObject[6];
        PlayerUnits = new GameObject[6];

        optionIndex = 0;
        battleState = BattleState.SET;
        battleMap.SetSize(12, 12);
        battleMap.SetMap();

        SetBattle();

        //SetTurnOrder();

        //InstantiateUnits();

    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.instance.GetUnitWithTurn() != null)
            battleCamera.SetTarget(TurnManager.instance.GetUnitWithTurn().transform);

        switch (battleState)
        {

            case (BattleState.SET):
                {
                    //allow player to set its units

                    //start battle
                    battleState = BattleState.START;
                }
                break;

            case (BattleState.START):
                {
                    //ui animations for battle start

                    battleState = BattleState.BATTLE;
                }
                break;


            case (BattleState.BATTLE):
                {
                    isPlayerTurn = TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().is_player_unit;

                    if (TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().IsPlayerUnit())
                    {
                        PlayerTurn();
                    }

                    else
                    {
                        EnemyCommandMove();
                    }
                }
                break;
        }
    }

    void SetTurnOrder()
    {
        for (int i = 0; i < EnemyUnits.Length; ++i)
            TurnOrder.Add(EnemyUnits[i]);

        for (int i = 0; i < PlayerUnits.Length; ++i)
        {
            TurnOrder.Add(PlayerUnits[i]);
        }

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

    void InstantiateUnits() //this has to change. Enemy units will already be on the map when loading the battle
                            //Player units will instantiate when he choses where to place its units
    {
        for (int i = 0; i < TurnOrder.Count; ++i)
        {

            Vector3 new_position = new Vector3(TurnOrder[i].GetComponent<Unit>().GetPosition().x * Globals.TILE_SIZE, 1.5f, TurnOrder[i].GetComponent<Unit>().GetPosition().y * Globals.TILE_SIZE);
            if (TurnOrder[i].GetComponent<Unit>().IsPlayerUnit())
            {
                TurnOrder[i] = Instantiate(PlayerUnitPrefab, new_position, Quaternion.identity);
            }
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
                    CommandMove();
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


    void CommandMove()
    {
        HideBattleUI();

        TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ActionMovement();


        if (TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().IsPlayerUnit() && TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().finished_movement)
            EndTurn();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();
            ShowBattleUI();
            currentSubmenu = CurrentSubmenu.FIRST;

        }
    }

    void EnemyCommandMove()
    {

        TurnManager.instance.GetUnitWithTurn().GetComponent<NPCMove>().ActionMovement();

        //if (TurnManager.instance.GetUnitWithTurn() != null && TurnManager.instance.GetUnitWithTurn().GetComponent<NPCMove>().finished_movement)
           // EndTurn();
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
        if (TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().is_player_unit)
        {
            TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();
            ShowBattleUI();
            currentSubmenu = CurrentSubmenu.FIRST;
        }

    }

    IEnumerator EnemyTurn()
    {
        action_tile = TurnOrder[current_turn].GetComponent<Unit>().GetPosition(); //reset action tile to the turn unit's position
        enemy_turn_finished = false;
        yield return new WaitForSeconds(2);
        EndTurn();
        enemy_turn_finished = true;

    }

    void PlayerActionMove()
    {
        EndTurn();
    }

    void SetBattle() //depending on its id, rewards and enemy units will change
    {

        switch (battle_id)
        {
            case (0): //introduction mission
                pesetas_reward = 100;

                break;
        }
    }

    void ShowCurrentUnitInfo()
    {

    }

    void PlacePlayerUnits()
    {

    }
}
