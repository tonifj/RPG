using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleManager : MonoBehaviour
{
    //Important
    int battle_id;
    int pesetas_reward;
    //Equipment reward

    //GO
    GameObject battleCameraGO;
    BattleCamera battleCamera;

    public GameObject ActionSelectorItemGO;
    public GameObject FirstMenuUI;
    public GameObject ActionUI;

    public GameObject MoveSelectorGO;
    public GameObject ActionSelectorMenuGO;
    public GameObject WaitSelectorGO;
    public GameObject StatusSelectorGO;

    public GameObject AttackSelectorGO;
    public GameObject SkillSelectorGO;
    public GameObject ItemSelectorGO;

    GameObject AccuracyBarGO;
    GameObject UnitInfoGO;

    public GameObject ItemMenu;
    GameObject help_panel;

    //Prefabs
    public GameObject PlayerUnitPrefab;
    public GameObject EnemyUnitPrefab;

    //Materials
    public Material SelectableTilesMaterial;
    public Material SelectedTileMaterial;

    //booleans
    public static bool isPlayerTurn;
    public bool confirmation_button_clicked = false;
    public bool cancel_action_button_clicked = false;

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
        LOOK,
        FIRST,
        MOVE,
        ACTION,
        STATUS,
        ATTACK,
        SKILL,
        ITEM,
        WAIT,
        NONE
    }
    enum ActionType
    {
        LOOK,
        MOVE,
        ATTACK,
        ITEM,
        ACTION,
        WAIT,
        STATUS
    }

    BattleState battleState;
    CurrentSubmenu currentSubmenu;

    //other variables
    int optionIndex;
    public int current_turn;

    public GameObject[] EnemyUnits;
    public GameObject[] PlayerUnits;


    BattleMap battleMap = new BattleMap();


    Vector2Int action_tile; //used to select a tile where to cast an action

    Player player;
    public Consumible consumible_to_be_used;
    FillBar accuracy_bar_filler;

    Button confirmation_button;
    Button cancel_button;
    Unit unit_to_cast_action;

    GameObject wait_selectorGO;

    void Start()
    {
        Random.seed = Random.Range(0, 100);

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

        player = GameObject.FindGameObjectWithTag("PlayerGO").GetComponent<Player>();

        help_panel = GameObject.FindGameObjectWithTag("help panel");
        HideHelpPanel();

        accuracy_bar_filler = GameObject.FindGameObjectWithTag("accuracy bar filler").GetComponent<FillBar>();
        AccuracyBarGO = GameObject.FindGameObjectWithTag("accuracyBarGO");
        HideAccuracyBar();

        UnitInfoGO = GameObject.FindGameObjectWithTag("uinfoGO");

        confirmation_button = GameObject.FindGameObjectWithTag("confirmation button").GetComponent<Button>();
        cancel_button = GameObject.FindGameObjectWithTag("cancel button").GetComponent<Button>();
        HideButtons();

        wait_selectorGO = GameObject.FindGameObjectWithTag("waiterGO");
        wait_selectorGO.SetActive(false);
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
                        HideFirstMenu();
                        HideActionMenu();
                        EnemyCommandMove();
                    }
                }
                break;
        }
    }

    public BattleMap GetBattleMap()
    {
        return battleMap;
    }

    void EnemyAction()
    {
        //Attack
        //Cast skill
        //Item
        //Wait
    }

    void HandleSubmenus()
    {
        switch (currentSubmenu)
        {
            case CurrentSubmenu.LOOK:
                {
                    MouseTileSelection(ActionType.LOOK);
                }
                break;
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

            case CurrentSubmenu.ACTION:
                {
                    ActionMenuNav();
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
                    ItemAction();
                }
                break;

            case CurrentSubmenu.STATUS:
                {
                    CommandMove();
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
        ShowActionSelector();
        ShowFirstMenu();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (optionIndex != 0)
                --optionIndex;
            else
                optionIndex = 3;

            switch (optionIndex)
            {
                case 0:
                    {
                        if (!TurnManager.instance.movement_available)
                            optionIndex = 3;
                        break;
                    }

                case 1:
                    {
                        if (!TurnManager.instance.action_available)
                            optionIndex = 0;
                        break;
                    }
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (optionIndex != 4)
                ++optionIndex;
            else
                optionIndex = 0;

            switch (optionIndex)
            {
                case 4:
                    {
                        if (!TurnManager.instance.movement_available)
                            optionIndex = 1;
                        break;
                    }

                case 1:
                    {
                        if (!TurnManager.instance.action_available)
                            optionIndex = 2;
                        break;
                    }
            }
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(TurnManager.instance.movement_available && TurnManager.instance.action_available)
            {
                HideFirstMenu();
                currentSubmenu = CurrentSubmenu.LOOK;
            }

            else if(!TurnManager.instance.movement_available)
            {
                TurnManager.instance.movement_available = true;
                optionIndex = 0;
                TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().GoToPreviousPos();
            }
           
        }

        switch (optionIndex)
        {
            case 0:
                {
                    ActionSelectorItemGO.transform.position = MoveSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.MOVE;
                    }
                }
                break;

            case 1:
                {
                    ActionSelectorItemGO.transform.position = ActionSelectorMenuGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.ACTION;
                        optionIndex = 0;
                    }
                }
                break;

            case 2:
                {
                    ActionSelectorItemGO.transform.position = WaitSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.WAIT;
                    }
                }
                break;

            case 3:
                {
                    ActionSelectorItemGO.transform.position = StatusSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.STATUS;
                    }
                }
                break;

            default: //in case there is some error
                {
                    ActionSelectorItemGO.transform.position = MoveSelectorGO.transform.position;
                    optionIndex = 0;
                }
                break;
        }
    }
    void ActionMenuNav()
    {
        HideFirstMenu();
        ShowActionSelector();
        ShowActionMenu();
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) //so it doesn't iterate all the time inside this
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (optionIndex != 0)
                    --optionIndex;
                else
                    optionIndex = 3;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (optionIndex != 3)
                    ++optionIndex;
                else
                    optionIndex = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HideActionSelector();
            HideActionMenu();
            currentSubmenu = CurrentSubmenu.FIRST;
        }

        switch (optionIndex)
        {
            case 0:
                {
                    ActionSelectorItemGO.transform.position = AttackSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        HideActionSelector();
                        currentSubmenu = CurrentSubmenu.ATTACK;
                    }
                }
                break;

            case 1:
                {
                    ActionSelectorItemGO.transform.position = SkillSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        currentSubmenu = CurrentSubmenu.SKILL;
                    }
                }
                break;

            case 2:
                {
                    ActionSelectorItemGO.transform.position = ItemSelectorGO.transform.position;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        HideActionSelector();
                        currentSubmenu = CurrentSubmenu.ITEM;
                    }
                }
                break;

            default: //in case there is some error
                {
                    ActionSelectorItemGO.transform.position = AttackSelectorGO.transform.position;
                    optionIndex = 0;
                }
                break;
        }
    }

    void CommandMove()
    {
        HideActionSelector();
        HideFirstMenu();
        MouseTileSelection(ActionType.MOVE);

        if (TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().finished_movement)
        {


            if (TurnManager.instance.action_available)
            {
                ShowFirstMenu();
                ShowActionSelector();
                currentSubmenu = CurrentSubmenu.FIRST;
                optionIndex = 1;

            }

            else
                currentSubmenu = CurrentSubmenu.WAIT;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();
            ShowFirstMenu();
            ShowActionSelector();
            currentSubmenu = CurrentSubmenu.FIRST;

        }
    }

    void EnemyCommandMove()
    {
        TurnManager.instance.GetUnitWithTurn().GetComponent<NPCMove>().ActionMovement();

        if (TurnManager.instance.GetUnitWithTurn() != null && TurnManager.instance.GetUnitWithTurn().GetComponent<NPCMove>().finished_movement)
            EndTurn();
    }

    void AttackAction()
    {
        HideActionMenu();
        MouseTileSelection(ActionType.ATTACK);
    }

    void SkillAction()
    {
        FirstMenuUI.SetActive(false);

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
        HideActionMenu();
        ShowItemMenu();

        if (consumible_to_be_used != null)
        {
            HideActionMenu();
            SetTargetTilesForItemUsage();
            MouseTileSelection(ActionType.ITEM);
            HideUnitInfo();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowUnitInfo();
                HideAccuracyBar();
                HideHelpPanel();
                ShowActionMenu();
                ResetItemUsageTiles();
                consumible_to_be_used = null;
            }
        }

        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowActionMenu();
                HideItemMenu();
                currentSubmenu = CurrentSubmenu.ACTION;
            }
        }
    }

    void ShowItemMenu()
    {
        ItemMenu.SetActive(true);
    }

    void HideItemMenu()
    {
        ItemMenu.SetActive(false);
        HideHelpPanel();
    }

    void WaitAction()
    {
        HideFirstMenu();
        HideActionSelector();
        wait_selectorGO.GetComponent<WaitSelectorIndicator>().SetIndicators();
        wait_selectorGO.SetActive(true);

        if (wait_selectorGO.GetComponent<WaitSelectorIndicator>().wait_dir_completed)
        {
            EndTurn();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if ((TurnManager.instance.action_available && !TurnManager.instance.movement_available) ||
              (!TurnManager.instance.action_available && TurnManager.instance.movement_available))
            {
                wait_selectorGO.SetActive(false);
                ShowFirstMenu();
                ShowActionSelector();
                currentSubmenu = CurrentSubmenu.FIRST;
            }

            else
            {
                //todo: play sound

            }
        }
    }

    void MouseTileSelection(ActionType actionType)
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        switch (actionType)
        {
            case ActionType.LOOK:
                {
                    HideActionSelector();
                    TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();

                    Tile t;
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "tile")
                        {
                            t = hit.collider.GetComponent<Tile>();

                            t.selectable = true;

                            if (t.IsUnitOnTile())
                            {
                                GetComponent<TargetUnitInfoManager>().SetUnit(t.GetUnitOnTop());
                            }
                            else
                            {
                                GetComponent<TargetUnitInfoManager>().SetUnit(null);
                            }
                        }

                        else if (hit.collider.tag == "enemy unit" || hit.collider.tag == "player unit")
                        {
                            GetComponent<TargetUnitInfoManager>().SetUnit(hit.collider.GetComponent<Unit>());
                        }

                        else
                            GetComponent<TargetUnitInfoManager>().SetUnit(null);
                    }

                    if (Input.GetKeyDown(KeyCode.Escape))
                    {

                        TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();
                        currentSubmenu = CurrentSubmenu.FIRST;
                        GetComponent<TargetUnitInfoManager>().SetUnit(null);
                    }

                    break;
                }

            case ActionType.MOVE:
                {
                    HideActionSelector();
                    TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ActionMovement();
                    break;
                }

            case ActionType.ITEM:
                {
                    HideActionSelector();
                    SelectTileItem();
                    break;
                }

            case ActionType.ATTACK:
                {
                    SelectTileAttack();
                    break;
                }
        }
    }

    void ShowActionMenu()
    {
        ActionUI.SetActive(true);
    }

    void HideActionMenu()
    {
        ActionUI.SetActive(false);
    }

    void HideFirstMenu()
    {
        FirstMenuUI.SetActive(false);
    }

    void ShowFirstMenu()
    {
        FirstMenuUI.SetActive(true);
    }

    void PlayerTurn()
    {
        HandleSubmenus();
    }

    void EndTurn()
    {
        if (TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().is_player_unit)
        {
            optionIndex = 0;

            GetComponent<TargetUnitInfoManager>().SetUnit(null);
            unit_to_cast_action = null;

            wait_selectorGO.SetActive(false);
            wait_selectorGO.GetComponent<WaitSelectorIndicator>().wait_dir_completed = false;

            TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();
            currentSubmenu = CurrentSubmenu.FIRST;

            HideAccuracyBar();

            TurnManager.instance.EndTurn();

        }

        else
            TurnManager.instance.EndTurn();


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

    void HideHelpPanel()
    {
        help_panel.GetComponent<Image>().enabled = false;
        help_panel.GetComponentInChildren<Text>().enabled = false;
    }

    #region ITEM_STUFF
    public void SetItemToBeUsed(Consumible consum) // an inventory button will execute this
    {
        consumible_to_be_used = consum;
    }

    void SetTargetTilesForItemUsage()
    {
        if (consumible_to_be_used != null) //if we selected the consumible by pressing the inventory button.
        {
            Tile current_tile = TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().GetCurrenntTile();
            TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ComputeAdjacencyLists(TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().jumpHeight, null, TacticsMove.TypeOfAdjacents.OTHER);
            current_tile.selectable = true;

            foreach (Tile t in current_tile.adjacents)
            {
                t.selectable = true;
            }
        }
    }

    void ResetItemUsageTiles()
    {
        Tile current_tile = TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().GetCurrenntTile();
        current_tile.selectable = false;

        foreach (Tile t in current_tile.adjacents)
        {
            t.selectable = false;
        }
    }

    void SelectTileItem()
    {
        if (unit_to_cast_action == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Tile t;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "tile")
                {
                    t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        t.target = true;
                        if (Input.GetMouseButtonUp(0))
                        {
                            if (t.IsUnitOnTile())
                            {
                                ShowButtons();
                                unit_to_cast_action = t.GetUnitOnTop();
                                GetComponent<TargetUnitInfoManager>().SetUnit(unit_to_cast_action);
                            }

                            else
                            {
                                // TODO: play a sound indicating this tile is not selectable
                            }
                        }
                    }

                    else //if the tile is not selectable
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            // TODO: play a sound indicating this tile is not selectable

                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "tile")
                    {
                        t = hit.collider.GetComponent<Tile>();
                        t.target = false;
                    }
                }
            }
        }

        else
        {
            ShowUnitInfoAndAccBar(100); //because items have 100% accuracy

            if (confirmation_button_clicked)
            {
                ApplyItemEffects(unit_to_cast_action);
                player.RemoveConsumible(consumible_to_be_used);
                HideItemMenu();
                consumible_to_be_used = null;
                HideButtons();
                confirmation_button_clicked = false;
                TurnManager.instance.action_available = false;

                if (TurnManager.instance.movement_available)
                {
                    TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();
                    ShowFirstMenu();
                    optionIndex = 0;
                }

                else
                    currentSubmenu = CurrentSubmenu.WAIT;
            }

            else if (cancel_action_button_clicked)
            {
                HideAccuracyBar();
                HideItemMenu();
                HideButtons();
                ShowActionMenu();
                TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();
                consumible_to_be_used = null;
                unit_to_cast_action = null;
                GetComponent<TargetUnitInfoManager>().SetUnit(null);
                cancel_action_button_clicked = false;
                currentSubmenu = CurrentSubmenu.ACTION;
            }
        }
    }

    void HideActionSelector()
    {
        ActionSelectorItemGO.SetActive(false);
    }

    void ShowActionSelector()
    {
        ActionSelectorItemGO.SetActive(true);
    }

    void ApplyItemEffects(Unit target)
    {
        if (consumible_to_be_used != null)
        {
            switch (consumible_to_be_used.GetConsumibleType())
            {
                case Consumible.ConsumibleType.HP_HEAL:
                    target.Heal(consumible_to_be_used.GetPower());
                    break;
            }
        }
    }
    #endregion

    #region ACCURACY BAR AND "DO IT - CANCEL" BUTTONS

    void ShowAccuracyBar()
    {
        AccuracyBarGO.SetActive(true);
    }

    void HideAccuracyBar()
    {
        AccuracyBarGO.SetActive(false);
    }

    void ShowUnitInfo()
    {
        UnitInfoGO.SetActive(true);
    }

    void HideUnitInfo()
    {
        UnitInfoGO.SetActive(false);
    }

    void ShowUnitInfoAndAccBar(int precision)
    {
        ShowUnitInfo();
        ShowAccuracyBar();
        accuracy_bar_filler.SetPercentage(precision);
    }

    public void ConfirmationButton()
    {
        confirmation_button_clicked = true;
    }

    void ShowButtons()
    {
        confirmation_button.gameObject.SetActive(true);
        cancel_button.gameObject.SetActive(true);
    }

    void HideButtons()
    {
        confirmation_button.gameObject.SetActive(false);
        cancel_button.gameObject.SetActive(false);
    }

    public void CancelButton()
    {
        cancel_action_button_clicked = true;
    }

    #endregion

    void SelectTileAttack()
    {
        int range = TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().GetAttackRange();
        battleMap.AttackSkillTileSelection(TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().GetTargetTile(TurnManager.instance.GetUnitWithTurn()).gameObject, range);

        if (unit_to_cast_action == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Tile t;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "tile")
                {
                    t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        t.target = true;
                        if (t.IsUnitOnTile())
                        {
                            GetComponent<TargetUnitInfoManager>().SetUnit(t.GetUnitOnTop());

                            if (Input.GetMouseButtonUp(0))
                            {
                                unit_to_cast_action = t.GetUnitOnTop();
                            }
                        }
                    }
                    else
                    {
                        GetComponent<TargetUnitInfoManager>().SetUnit(null);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "tile")
                    {
                        t = hit.collider.GetComponent<Tile>();
                        t.target = false;
                    }
                }

                battleMap.ResetTilesByRange(TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>().GetTargetTile(TurnManager.instance.GetUnitWithTurn()).gameObject, range);
                GetComponent<TargetUnitInfoManager>().SetUnit(null);
                HideAccuracyBar();
                HideHelpPanel();
                ShowActionMenu();
                currentSubmenu = CurrentSubmenu.ACTION;

            }
        }

        else
        {
            ShowButtons();
            ShowUnitInfoAndAccBar(ComputeAttackAccuracy()); //because items have 100% accuracy

            if (confirmation_button_clicked)
            {
                Random.seed = Random.Range(0, 100);
                int random_num = Random.Range(0, 100);
                Debug.Log("Acc: " + ComputeAttackAccuracy().ToString());
                Debug.Log("RGN: " + random_num.ToString());
                if (random_num <= ComputeAttackAccuracy())
                {
                    unit_to_cast_action.ReceivePhyDamage(TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().GetPhyAttack());
                }

                else
                {
                    Debug.Log("MISS");
                }

                TurnManager.instance.action_available = false;
                confirmation_button_clicked = false;
                unit_to_cast_action = null;
                GetComponent<TargetUnitInfoManager>().SetUnit(null);
                HideButtons();
                HideAccuracyBar();
                TurnManager.instance.GetUnitWithTurn().GetComponent<PlayerMove>().ResetTilesColor();

                if (TurnManager.instance.movement_available)
                {
                    ShowFirstMenu();
                    currentSubmenu = CurrentSubmenu.FIRST;
                    optionIndex = 0;
                }
                else
                {
                    currentSubmenu = CurrentSubmenu.WAIT;
                }
            }

            else if (cancel_action_button_clicked || Input.GetKeyDown(KeyCode.Escape))
            {
                unit_to_cast_action = null;
                cancel_action_button_clicked = false;
                GetComponent<TargetUnitInfoManager>().SetUnit(null);

                HideAccuracyBar();
                HideButtons();
            }
        }
    }

    int ComputeAttackAccuracy()
    {
        float accuracy = 100;

        //check if target is alive
        if (unit_to_cast_action.GetStatus() != Unit.unit_status.DEAD)
        {
            switch (Globals.GetRelativePosition(TurnManager.instance.GetUnitWithTurn().GetComponent<TacticsMove>(), unit_to_cast_action.gameObject.GetComponent<TacticsMove>()))
            {
                case Globals.RelativePosition.FRONT:
                    {
                        accuracy -= unit_to_cast_action.GetEvasion();
                        break;
                    }

                case Globals.RelativePosition.SIDE:
                    {
                        accuracy -= unit_to_cast_action.GetEvasion() / 2;
                        break;
                    }

                case Globals.RelativePosition.REAR:
                    {
                        accuracy -= unit_to_cast_action.GetEvasion() / 4;
                        break;
                    }
            }

        }

        return Mathf.RoundToInt(accuracy);
    }
}
