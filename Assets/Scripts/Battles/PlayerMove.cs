using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{
    Tile selectedTileForKeyboardMovement;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "tile")
            {
                Tile t = hit.collider.GetComponent<Tile>();

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
                            //todo - play a sound that indicates that it isn't a selectable tile
                        }
                    }
                }                    
            }
        }
    }

    void PaintTiles() //this only paints all tiles within movement range to the color. only for aesthetics purposes
    {
        

       
    }

    void SelectTileKeyboard()
    {

    }
}
