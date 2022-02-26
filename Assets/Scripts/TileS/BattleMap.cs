using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    public Material origin_tile_material;
    public Material selection_material;
    int map_width;//x
    int map_depth;//z
    GameObject[] TilesGO;
    List<Tile> Tiles = new List<Tile>();

    Vector2Int tile_matrix; //defines the matrix of tiles
    Tile invalidTile;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CopyMatrix()
    {
        for (int i = 0; i < TilesGO.Length; ++i)
        {
            Tiles.Add(TilesGO[i].GetComponent<Tile>());
        }
    }
    public void SetTileMatrix()
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

    public void PlaceTiles()
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

    void HighlightTile(Tile tile, bool isOrigin)
    {
        if (IsValidTile(tile))
        {
            // if (isOrigin)
            // tile.gameObject.GetComponent<MeshRenderer>().material = origin_tile_material;
            // else
            //tile.gameObject.GetComponent<MeshRenderer>().material = selection_material;
        }
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
        return t.GetBattleMapPos().x != -1;
    }

    int TileDistance(Tile origin, Tile end)
    {
        int dist = 0;

        if (origin.GetBattleMapPos().x < end.GetBattleMapPos().x)
        {
            for (int i = origin.GetBattleMapPos().x; i < end.GetBattleMapPos().x; ++i)
                ++dist;
        }

        else
        {
            for (int i = end.GetBattleMapPos().x; i < origin.GetBattleMapPos().x; ++i)
                ++dist;
        }


        if (origin.GetBattleMapPos().y < end.GetBattleMapPos().y)
        {
            for (int i = origin.GetBattleMapPos().y; i < end.GetBattleMapPos().y; ++i)
                ++dist;
        }

        else
        {
            for (int i = end.GetBattleMapPos().y; i < origin.GetBattleMapPos().y; ++i)
                ++dist;
        }

        return dist;
    }

    void ActionTileSelection(Tile origin, int range)
    {
        if (range == 0)
            HighlightTile(origin, true);
        else
        {
            for (int i = 0; i < Tiles.Count; ++i)
            {
                if (TileDistance(origin, Tiles[i]) <= range)
                    HighlightTile(Tiles[i], false);
            }
            HighlightTile(origin, true);
        }
    }

    public void SetSize(int w, int d)
    {
        map_width = w;
        map_depth = d;
    }

    public int GetTilesSize()
    {
        return Tiles.Count;
    }

    public void SetMap()
    {
        tile_matrix = new Vector2Int(0, 0);
        invalidTile = new Tile();

        TilesGO = GameObject.FindGameObjectsWithTag("tile");
        CopyMatrix(); //Copy the Tile component of each TileGO to a different array
        SetTileMatrix();
        PlaceTiles();

    }
}

