using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{

    int map_width;//x
    int map_depth;//z
    GameObject[] TilesGO;
    List<Tile> Tiles = new List<Tile>();

    Material selection_material;


    Vector2Int tile_matrix; //defines the matrix of tiles
    Tile invalidTile;

    void Start()
    {
        invalidTile.SetTilePos(new Vector2Int(-1, -1));
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

    public Tile GetTile(Vector2Int v)
    {
        return Tiles[v.x + (map_width * v.y)].GetComponent<Tile>();
    }

    public void HighlightTile(Tile tile, Material highlight_material)
    {
        if (IsValidTile(tile.GetBattleMapPos()))
        {
            tile.gameObject.GetComponent<MeshRenderer>().material = highlight_material;
        }
    }

    void ResetTileMaterial(Tile tile)
    {
        if (IsValidTile(tile.GetBattleMapPos()))
        {
            tile.ResetTileMaterial();
        }
    }

    List<Tile> GetNeighbors(Tile origin)
    {
        List<Tile> neighbors = new List<Tile>();

        if (IsValidTile(GetBackNeighborTile(origin).GetBattleMapPos()))
            neighbors.Add(GetBackNeighborTile(origin));

        if (IsValidTile(GetFrontNeighborTile(origin).GetBattleMapPos()))
            neighbors.Add(GetFrontNeighborTile(origin));

       if (IsValidTile(GetRightNeighborTile(origin).GetBattleMapPos()))
            neighbors.Add(GetRightNeighborTile(origin));

       if (IsValidTile(GetLeftNeighborTile(origin).GetBattleMapPos()))
            neighbors.Add(GetLeftNeighborTile(origin));

        return neighbors;
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
    public bool IsValidTile(Vector2Int tile_pos) //there is no tile with negative positions
    {
        return tile_pos.x > -1 &&
            tile_pos.x < map_width &&

           tile_pos.y > -1 &&
           tile_pos.y < map_depth;
    }

    //public bool IsValidTileMovement(Vector2Int tile_pos, bool is_player_unit)
   // {




    //}

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

    public void AttackSkillTileSelection(Tile origin, int range, Material select_mat)
    {
        selection_material = select_mat;

        if (range == 0)
            HighlightTile(origin, select_mat);
        else
        {
            for (int i = 0; i < Tiles.Count; ++i)
            {
                if (TileDistance(origin, Tiles[i]) <= range)
                    HighlightTile(Tiles[i], select_mat);
            }
            HighlightTile(origin, select_mat);
        }
    }

    public void MovementTileSelection(Tile origin, int range, Material select_mat, bool is_player_unit)
    {
        
    }

    public void ResetMaterials(Tile origin, int range)
    {
        if (range == 0)
            ResetTileMaterial(origin);
        else
        {
            for (int i = 0; i < Tiles.Count; ++i)
            {
                if (TileDistance(origin, Tiles[i]) <= range)
                    ResetTileMaterial(Tiles[i]);
            }
            ResetTileMaterial(origin);
        }
    }

    public void SetSize(int w, int d)
    {
        map_width = w;
        map_depth = d;
    }

    void SetTileNeighbors()
    {
        for(int i = 0; i < Tiles.Count; ++i)
        {
            Tiles[i].SetNeighbors(GetNeighbors(Tiles[i]));
        }
    }


    public void SetMap()
    {
        tile_matrix = new Vector2Int(0, 0);
        invalidTile = new Tile();

        TilesGO = GameObject.FindGameObjectsWithTag("tile");
        CopyMatrix(); //Copy the Tile component of each TileGO to a different array
        SetTileMatrix();
        PlaceTiles();
        SetTileNeighbors();
    }

    public void OccupyTile(Tile t)
    {

    }

    public void FreeTile(Tile t)
    {
    }

    void CalculateAvailableTilesForMovement(Tile origin, int movement_range, bool is_player_unit)
    {

    }

 


}

