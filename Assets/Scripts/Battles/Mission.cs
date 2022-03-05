using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    int MAX_UNITS = 6;
    int pesetas_reward;

    //TODO set equipment reward

    GameObject[] EnemyUnitsGO;
    List <GameObject> EnemyUnits = new List<GameObject>();
    int id;
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> GetEnemyUnits()
    {
        return EnemyUnits;
    }

    public void SetMission(int new_id, int reward)
    {
        id = new_id;
        pesetas_reward = reward;
    }

    public void CreateEnemyUnit(ClassType type, Genre genre, int lvl)
    {
        GameObject new_unit = new GameObject();
        new_unit.AddComponent<Unit>();
        new_unit.AddComponent<NPCMove>();
        new_unit.GetComponent<Unit>().SetClass(type);
        new_unit.GetComponent<Unit>().SetLvl(lvl); //Also sets the stats
        new_unit.GetComponent<Unit>().SetEnemyUnit();
        new_unit.GetComponent<Unit>().SetName(Globals.GenerateRandomName(genre));
        EnemyUnits.Add(new_unit);
    }

    public void PlaceUnit(GameObject unit, Vector2Int tile)
    {
        unit.GetComponent<Unit>().SetPosition(tile);
        unit.transform.position = new Vector3(unit.GetComponent<Unit>().GetPosition().x * Globals.TILE_SIZE, 0, unit.GetComponent<Unit>().GetPosition().y * Globals.TILE_SIZE);
    }
}
