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

        if(!moving)
        {
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
        if(Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();

                    if(t.selectable)
                    {
                        //move
                        MoveToTile(t);
                    }
                }
            }
        }
    }

    void SelectTileKeyboard()
    {
        
    }
}
