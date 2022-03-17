using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false; //selectable to move to

    public bool is_player_unit_on_top = false;

    public Vector2Int battle_map_pos;

    public List<Tile> adjacents = new List<Tile>();

    //BFS
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    //A*
    public float f = 0; // g+h
    public float g = 0; // cost parent to current tile
    public float h = 0; // cost processed tile to destination

    public Tile()
    {
        battle_map_pos = new Vector2Int(-1, -1);
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetTile();
        transform.localScale = new Vector3(Globals.TILE_SIZE, 1, Globals.TILE_SIZE);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = Color.white;

        if (TurnManager.instance.GetUnitWithTurn() != null && TurnManager.instance.GetUnitWithTurn().GetComponent<Unit>().is_player_unit)
        {
            if (selectable)
            {
                GetComponent<Renderer>().material.color = Color.blue;
            }   
        }


        if (target)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }

       
        






    }

    public Vector2Int GetBattleMapPos() //Returns position in tilepos (vector2int)
    {
        return battle_map_pos;
    }

    public void SetBattleMapPos(Vector2Int v)
    {
        battle_map_pos = v;
    }

    public void SetTilePos(Vector3 pos)
    {
        transform.position = new Vector3(Globals.TILE_SIZE * pos.x, pos.y, Globals.TILE_SIZE * pos.z);
    }

    public Vector3 GetWorldPos() //returns position in vec3
    {
        return transform.position;
    }

    public void ResetTileMaterial()
    {
        GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public void ResetTile()
    {
        adjacents.Clear();
        current = false;
        target = false;
        selectable = false; //selectable to move to

        visited = false;
        parent = null;
        distance = 0;

        g = 0;
        h = 0;
        f = 0;
    }

    public void FindNeighbors(float jumpHeight, Tile target)
    {
        ResetTile();
        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    public bool IsSomethingOnTile()
    {
        RaycastHit hit;
        return Physics.Raycast(gameObject.transform.position, Vector3.up, out hit, 1);
    }

    public void CheckTile(Vector3 direction, float jumpHeight, Tile target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (Globals.TILE_SIZE + jumpHeight) / 2, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider collider in colliders)
        {
            Tile tile = collider.GetComponent<Tile>();


            if (tile != null && tile.walkable)
            {
                RaycastHit hit;

                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || tile == target) //if there is something on top of the tile or the tile we're looking at is the same we chose to move to
                {
                    adjacents.Add(tile);
                }

                else if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || tile == target)
                {
                    if (hit.collider.tag == "player unit" && BattleManager.isPlayerTurn)
                        adjacents.Add(tile);

                    //else if (hit.collider.tag == "enemy unit" && !BattleManager.isPlayerTurn)
                       // adjacents.Add(tile);
                   
                }

            }

        }

    }



    public bool IsWalkable()
    {
        return walkable;
    }

    public void SetColor(Color c)
    {
        GetComponent<Renderer>().material.color = c;

    }

    public void BeingVisited()
    {
        visited = true;
    }
}
