using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    int MAX_UNITS = 6;
    int pesetas_reward;
    GameObject[] EnemyUnitsGO;
    Unit[] EnemyUnits;
    int id;
    void Start()
    {
        EnemyUnitsGO = GameObject.FindGameObjectsWithTag("enemy unit");
        EnemyUnits = new Unit[EnemyUnitsGO.Length];

        CopyEnemyMatrix();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void CopyEnemyMatrix()
    {
        for (int i = 0; i < EnemyUnitsGO.Length; ++i)
        {
            EnemyUnits[i] = EnemyUnitsGO[i].GetComponent<Unit>();
            EnemyUnits[i].SetEnemyUnit();
        }
    }

    public Unit[] GetEnemyUnits()
    {
        return EnemyUnits;
    }

    public void SetMissionUnits(ClassType type1, int lvl1, ClassType type2, int lvl2, ClassType type3, int lvl3,
                                ClassType type4, int lvl4, ClassType type5, int lvl5, ClassType type6, int lvl6)
    {
       //TODO
    }

    Unit CreateUnit(ClassType type, int lvl)
    {
        Unit new_unit = gameObject.AddComponent<Unit>();
        new_unit.SetClass(type);
        new_unit.SetLvl(lvl); //Also sets the stats

        return new_unit;
    }
}
