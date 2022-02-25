using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    int MAX_UNITS = 6;
    int pesetas_reward;

    //TODO set equipment reward

    GameObject[] EnemyUnitsGO;
    List <Unit> EnemyUnits = new List<Unit>();
    int id;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<Unit> GetEnemyUnits()
    {
        return EnemyUnits;
    }

    public void SetMission(int new_id, int reward)
    {
        id = new_id;
        pesetas_reward = reward;
    }

    public void CreateEnemyUnit(ClassType type, int lvl)
    {
        Unit new_unit = new Unit();
        new_unit.SetClass(type);
        new_unit.SetLvl(lvl); //Also sets the stats
        new_unit.SetEnemyUnit();
        EnemyUnits.Add(new_unit);
    }

    public void PlaceUnit(Unit unit, Vector2Int tile)
    {
        unit.SetPosition(tile);
    }

}
