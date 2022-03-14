using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActionMovement()
    {
        if (!turn)
            return;

        if (!moving)
        {
            finished_movement = false;
            FindNearestTarget();
            CalculatePath();
            FindSelectableTiles();

            actualTargetTile.target = true;
        }

        else
        {
            Move();
        }
    }

    void CalculateTileToMoveTo()
    {
       

    }

    void CalculatePath()
    {
        Tile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    void FindNearestTarget() // we want to get close to the nearest unit, at least closest the attack range
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("player unit");

        GameObject nearest = null; //closest player unit

        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets) // get closest player unit
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position);

            if (dist < distance)
            {
                distance = dist;
                nearest = obj;
            }
        }

        target = nearest;
    }
}
