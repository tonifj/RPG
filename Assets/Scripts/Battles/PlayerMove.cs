using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{
    Tile start_tile;

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

        else if (!moving)
        {
            finished_movement = false;
            FindSelectableTiles();
            SelectTileMovement();
        }

        else
        {
           Move();
        }
    }

    void SelectTileMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Tile t;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "tile")
            {
              t  = hit.collider.GetComponent<Tile>();

                if (t.selectable)
                {
                    t.target = true;
                    if (Input.GetMouseButtonUp(0))
                    {
                        t.target = true;

                        if (!t.IsSomethingOnTile())
                            MoveToTile(t);

                        else
                        {
                            //TODO - play a sound that indicates that it isn't a selectable tile
                        }
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

    public void SetStartTile(Tile tile) //TODO
    {
        start_tile = tile;
    }
}
