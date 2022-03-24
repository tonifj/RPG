using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    GameObject[] enemyUnits;
    GameObject[] playerUnits;

    public List<Unit> gameUnits;
    Queue<TacticsMove> turnQueue = new Queue<TacticsMove>();

    [SerializeField]
    string current_unit;
    public enum UnitAction
    {
        MOVE,
        ACTION
    }
    UnitAction[] actions_done;

    public static TurnManager instance { get; private set; } //free to read, but settable only from here

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyUnits = GameObject.FindGameObjectsWithTag("enemy unit");
        playerUnits = GameObject.FindGameObjectsWithTag("player unit"); //for the moment. the objective is to make the player place the units in a small area

        actions_done = new UnitAction[2];

    }

    // Update is called once per frame
    void Update()
    {
        if (gameUnits.Count <= 0)
            InitTurnQueue();
    }

    public void InitTurnQueue()
    {
        //clear list everytime
        gameUnits.Clear();

        //add units
        AddUnitsToUnitsList(playerUnits);
        AddUnitsToUnitsList(enemyUnits);

        //order units by speed
        OrganizeUnitsBySpeed();

        //Begin fastest unit's turn
        StartTurn();
    }

    public void StartTurn()
    {
        if (turnQueue.Count > 0)
        {
            turnQueue.Peek().BeginTurn();
            current_unit = turnQueue.Peek().gameObject.GetComponent<Unit>().GetName();
        }
    }

    public void EndTurn()
    {
        TacticsMove unit = turnQueue.Dequeue();
        unit.EndTurn();

        if (turnQueue.Count > 0)
        {
            StartTurn();
        }

        else
        {
            InitTurnQueue();
        }
    }

    void AddUnitsToUnitsList(GameObject[] units)
    {
        for (int i = 0; i < units.Length; ++i)
            gameUnits.Add(units[i].GetComponent<Unit>());
    }

    void OrganizeUnitsBySpeed()
    {
        List<Unit> tempList = gameUnits;

        for (int i = 0; i < tempList.Count-1; ++i)
        {
            Unit actualUnit = tempList[i];
            for (int j = i+1; j < tempList.Count; ++j)
            {
                if(actualUnit.GetSpeed() < tempList[j].GetSpeed())
                {
                    tempList[i] = tempList[j];
                    tempList[j] = actualUnit;
                }
            }
        }

        gameUnits = tempList;

        for(int i = 0; i < gameUnits.Count; ++i)
        {
            turnQueue.Enqueue(gameUnits[i].gameObject.GetComponent<TacticsMove>());
        }
    }

    public GameObject GetUnitWithTurn()
    {
        for(int i = 0; i < gameUnits.Count; ++i)
        {
            if (gameUnits[i].GetComponent<TacticsMove>().turn)
                return gameUnits[i].gameObject;
        }

        return null;
    }

    public void AddAction(UnitAction actionType)
    {
        actions_done[0] = actionType;
    }

    public UnitAction[] GetActionsDone()
    {
        return actions_done;
    }
}



