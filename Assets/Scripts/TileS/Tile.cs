using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = false;
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

    public Material GetMaterial()
    {
        return GetComponent<MeshRenderer>().material;
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

    public void SetNeighbors(List<Tile> new_adjacents)
    {
        ResetTile();
        adjacents = new_adjacents;
    }

}
