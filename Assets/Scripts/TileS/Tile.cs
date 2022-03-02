using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false; //selectable to move to

    public Vector2Int battle_map_pos;

    public List<Tile> adjacents = new List<Tile>();

    //BFS
    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

    public Tile()
    {
        battle_map_pos = new Vector2Int(-1, -1);
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(Globals.TILE_SIZE, 1, Globals.TILE_SIZE);
    }

    // Update is called once per frame
    void Update()
    {
        if (current)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }

        else if (target)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        else if (selectable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        else
        {
            GetComponent<Renderer>().material.color = Color.white;
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

    public void SetTilePos(Vector2 pos)
    {
        transform.position = new Vector3(Globals.TILE_SIZE * pos.x, 0, Globals.TILE_SIZE * pos.y);
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
    }

    public void FindNeighbors(float jumpHeight)
    {
        ResetTile();
        CheckTile(Vector3.forward, jumpHeight);
        CheckTile(-Vector3.forward, jumpHeight);
        CheckTile(Vector3.right, jumpHeight);
        CheckTile(-Vector3.right, jumpHeight);


    }

    public void CheckTile(Vector3 direction, float jumpHeight)
    {
        Vector3 halfExtents = new Vector3(0.25f, (Globals.TILE_SIZE+jumpHeight)/2, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach(Collider collider in colliders)
        {
            Tile tile = collider.GetComponent<Tile>();
            if(tile != null && tile.walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1)); //if there isn't something on top of the tile
                {
                    adjacents.Add(tile);
                }
            }
        }
    }
  

    public bool IsWalkable()
    {
        return walkable;
    }

    public void BeingVisited()
    {
        visited = true;
    }
}
