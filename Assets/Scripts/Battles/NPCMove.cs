using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove
{
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
        if (!moving)
        {
            finished_movement = false;

            FindSelectableTiles();
            SelectTileMouse();
        }

        else
        {
            Move();
        }
    }

    void SelectTileMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if (t.selectable)
                    {
                        //move
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
