using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{

    int map_width;//x
    int map_depth;//z
    GameObject[] TilesGO;
    List<GameObject> Tiles = new List<GameObject>();

    Material selection_material;


    Vector2Int tile_matrix; //defines the matrix of tiles
    Tile invalidTile;

    void Start()
    {
        invalidTile.SetTilePos(new Vector3(-1, 0, -1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CopyMatrix()
    {
        for (int i = 0; i < TilesGO.Length; ++i)
        {
            Tiles.Add(TilesGO[i]);
        }
    }

    public Tile GetTile(Vector2Int v)
    {
        return Tiles[v.x + (map_width * v.y)].GetComponent<Tile>();
    }

    public void HighlightTile(GameObject tile, Material highlight_material)
    {
        if (IsValidTile(tile.transform.position))
        {
            tile.gameObject.GetComponent<MeshRenderer>().material = highlight_material;
        }
    }

    void ResetTileMaterial(GameObject tile)
    {
        if (IsValidTile(tile.transform.position))
        {
            tile.GetComponent<Tile>().ResetTileMaterial();
        }
    }

    public bool IsValidTile(Vector3 tile_pos) //there is no tile with negative positions
    {
        return tile_pos.x > -1 &&
            tile_pos.x < map_width &&

           tile_pos.z > -1 &&
           tile_pos.z < map_depth;
    }

    public static int TileDistance(GameObject origin, GameObject end)
    {
        int dist = 0;

        if (origin.GetComponent<Tile>().GetBattleMapPos().x < end.GetComponent<Tile>().GetBattleMapPos().x)
        {
            for (int i = origin.GetComponent<Tile>().GetBattleMapPos().x; i < end.GetComponent<Tile>().GetBattleMapPos().x; ++i)
                ++dist;
        }

        else
        {
            for (int i = end.GetComponent<Tile>().GetBattleMapPos().x; i < origin.GetComponent<Tile>().GetBattleMapPos().x; ++i)
                ++dist;
        }


        if (origin.GetComponent<Tile>().GetBattleMapPos().y < end.GetComponent<Tile>().GetBattleMapPos().y)
        {
            for (int i = origin.GetComponent<Tile>().GetBattleMapPos().y; i < end.GetComponent<Tile>().GetBattleMapPos().y; ++i)
                ++dist;
        }

        else
        {
            for (int i = end.GetComponent<Tile>().GetBattleMapPos().y; i < origin.GetComponent<Tile>().GetBattleMapPos().y; ++i)
                ++dist;
        }

        return dist;
    }

    public void AttackSkillTileSelection(GameObject origin, int range)
    {

        if (range == 0) //only for self-targeted skills
            origin.GetComponent<Tile>().selectable = true;

        else
        {
            if (range == 1)
            {
                for (int i = 0; i < Tiles.Count; ++i)
                {
                    if (origin.transform.position.y == Tiles[i].transform.position.y ||
                        origin.transform.position.y + 1 == Tiles[i].transform.position.y ||
                        origin.transform.position.y - 1 == Tiles[i].transform.position.y) //if there is a diff of 1 tile of height
                    {
                        if (TileDistance(origin, Tiles[i]) <= range)
                        {
                            Tiles[i].GetComponent<Tile>().selectable = true;
                            Tiles[i].GetComponent<Tile>().target = false;
                        }
                    }
                }
            }

            else
            {
                for (int i = 0; i < Tiles.Count; ++i)
                {
                    if (TileDistance(origin, Tiles[i]) <= range)
                    {
                        Tiles[i].GetComponent<Tile>().selectable = true;
                        Tiles[i].GetComponent<Tile>().target = false;
                    }
                }
            }

            origin.GetComponent<Tile>().selectable = false;
        }

        
    }

    public void ResetTilesByRange(GameObject origin, int range)
    {
        if (range == 0)
            origin.GetComponent<Tile>().selectable = false;
        else
        {
            for (int i = 0; i < Tiles.Count; ++i)
            {
                if (TileDistance(origin, Tiles[i]) <= range)
                    Tiles[i].GetComponent<Tile>().selectable = false;
            }
        }
    }

    public void SetSize(int w, int d)
    {
        map_width = w;
        map_depth = d;
    }

    void SetTileMatrix()
    {
        foreach (GameObject t in Tiles)
        {

            if (tile_matrix.x >= map_width)
            {
                tile_matrix.x = 0;
                ++tile_matrix.y;
            }

            t.GetComponent<Tile>().SetBattleMapPos(tile_matrix);

            ++tile_matrix.x;
        }
    }

    public void SetMap()
    {
        tile_matrix = new Vector2Int(0, 0);
        invalidTile = new Tile();

        TilesGO = GameObject.FindGameObjectsWithTag("tile");
        CopyMatrix(); //Copy the Tile component of each TileGO to a different array
        SetTileMatrix();
        //PlaceTiles();    
    }

    public List<GameObject> GetTiles()
    {
        return Tiles;
    }



}

