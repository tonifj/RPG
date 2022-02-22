using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    Vector3 tile_position;
    bool occupied; //tells if is there something in the tile
    int size;

    Vector2Int battle_map_pos;


    private GameObject TileSelector;

    // Start is called before the first frame update
    void Start()
    {
        size = 3;
        transform.localScale = new Vector3(size, 1, size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetRightNeighborTile()
    {

    }

    void GetLeftNeighborTile()
    {

    }

    void GetFrontNeighborTile()
    {

    }

    void GetBackNeighborTile()
    {

    }

    public Vector2Int GetBattleMapPos()
    {
        return battle_map_pos;
    }

    public void SetBattleMapPos(Vector2Int v)
    {
        battle_map_pos = v;
    }

    public void SetTilePos(Vector2 pos)
    {
        transform.position = new Vector3(size*pos.x, 0, size*pos.y);
    }

}
