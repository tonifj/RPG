using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Vector3 tile_position;
    bool occupied; //tells if is there something in the tile.
    bool occupied_by_player_unit;

    Vector2Int battle_map_pos;

    Material original_material;

    

    private GameObject TileSelector;

    public Tile()
    {
        occupied = false;
        battle_map_pos = new Vector2Int(-1, -1);
    }

    // Start is called before the first frame update
    void Start()
    {
        original_material = GetComponent<MeshRenderer>().material;

        transform.localScale = new Vector3(Globals.TILE_SIZE, 1, Globals.TILE_SIZE);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        GetComponent<MeshRenderer>().material = original_material;
    }

    public Material GetMaterial()
    {
        return GetComponent<MeshRenderer>().material;
    }

    public bool IsOccupied()
    {
        return occupied;
    }

    public void SetOccupied(bool b)
    {
        occupied = b;
    }

    public void SetOccupiedByPlayerUnit(bool b)
    {
        occupied_by_player_unit = b;
    }

    public bool IsOccupiedByPlayerUnit()
    {
        return occupied_by_player_unit;
    }

}
