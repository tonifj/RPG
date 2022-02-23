using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public Material selection_material;
    public int map_width;//x
    public int map_depth;//z
    GameObject[] TilesGO;
    Tile[] Tiles;
    Vector2Int tile_matrix; //defines the matrix of tiles
    Tile invalidTile;

    void Start()
    {
        TilesGO = GameObject.FindGameObjectsWithTag("tile");
        Tiles = new Tile[TilesGO.Length];
        CopyMatrix(); //Copy the Tile component of each TileGO to a different array

        tile_matrix = new Vector2Int(0, 0);
        SetTileMatrix(); //Sets the tile's positions like a matrix
        SetMap(); //Places the tiles depending on its matrix index
        invalidTile = new Tile();

        TileDistance(GetTile(new Vector2Int(0, 0)), GetTile(new Vector2Int(2, 2)));
       // ActionTileSelection(GetTile(new Vector2Int(3, 3)), 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CopyMatrix()
    {
        for (int i = 0; i < TilesGO.Length; ++i)
        {
            Tiles[i] = TilesGO[i].GetComponent<Tile>();
        }
    }
    void SetTileMatrix()
    {
        foreach (Tile t in Tiles)
        {

            if (tile_matrix.x >= map_width)
            {
                tile_matrix.x = 0;
                ++tile_matrix.y;
            }

            t.SetBattleMapPos(tile_matrix);

            ++tile_matrix.x;
        }
    }

    void SetMap()
    {
        foreach (Tile t in Tiles)
        {
            t.SetTilePos(t.GetBattleMapPos());
        }
    }

    Tile GetTile(Vector2Int v)
    {
        return Tiles[v.x + (map_width * v.y)].GetComponent<Tile>();
    }

    void HighlightTile(Tile tile)
    {
        if (IsValidTile(tile))
            tile.gameObject.GetComponent<MeshRenderer>().material = selection_material;
    }

    Tile GetRightNeighborTile(Tile origin)
    {
        if (origin.GetBattleMapPos().x < map_width - 1)
            return Tiles[(origin.GetBattleMapPos().x) + 1 + (map_width * origin.GetBattleMapPos().y)];

        else
            return invalidTile;
    }

    Tile GetLeftNeighborTile(Tile origin)
    {
        if (origin.GetBattleMapPos().x - 1 >= 0)
            return Tiles[(origin.GetBattleMapPos().x) - 1 + (map_width * origin.GetBattleMapPos().y)];

        else
            return invalidTile;
    }

    Tile GetFrontNeighborTile(Tile origin)
    {
        if (origin.GetBattleMapPos().y < map_depth - 1)
            return Tiles[origin.GetBattleMapPos().x + origin.GetBattleMapPos().y * map_depth + map_depth];

        else
            return invalidTile;
    }

    Tile GetBackNeighborTile(Tile origin)
    {
        if (origin.GetBattleMapPos().y - 1 >= 0)
            return Tiles[origin.GetBattleMapPos().x + origin.GetBattleMapPos().y * map_depth - map_depth];

        else
            return invalidTile;
    }

    bool IsValidTile(Tile t) //there is no tile with negative positions
    {
        Debug.Log(t.GetBattleMapPos());
        return t.GetBattleMapPos().x != -1;
    }

    void TileDistance(Tile origin, Tile end)
    {
        int dist = 0;

        for (int i = origin.GetBattleMapPos().x; i < end.GetBattleMapPos().x; ++i)
            ++dist;

        for (int i = origin.GetBattleMapPos().y; i < end.GetBattleMapPos().y; ++i)
            ++dist;

        Debug.Log(dist);
        //return dist;
    }

    void ActionTileSelection(Tile origin, int range)
    {
        for (int i = 0; i < Tiles.Length; ++i)
        {
            //if (TileDistance(origin, Tiles[i]) <= range)
            //{
            //    HighlightTile(Tiles[i]);
            //}
        }
    }

}

