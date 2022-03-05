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

    int TileDistance(GameObject origin, GameObject end)
    {
        int dist = 0;

        if (origin.transform.position.x < end.transform.position.x)
        {
            for (int i = (int)origin.transform.position.x; i < end.transform.position.x; ++i)
                ++dist;
        }

        else
        {
            for (int i = (int)end.transform.position.x; i < origin.transform.position.x; ++i)
                ++dist;
        }


        if (origin.transform.position.y < end.transform.position.y)
        {
            for (int i = (int)origin.transform.position.y; i < end.transform.position.y; ++i)
                ++dist;
        }

        else
        {
            for (int i = (int)end.transform.position.y; i < origin.transform.position.y; ++i)
                ++dist;
        }

        return dist;
    }

    public void AttackSkillTileSelection(GameObject origin, int range, Material select_mat)
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

    public void ResetMaterials(GameObject origin, int range)
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

    public void SetMap()
    {
        tile_matrix = new Vector2Int(0, 0);
        invalidTile = new Tile();

        TilesGO = GameObject.FindGameObjectsWithTag("tile");
        CopyMatrix(); //Copy the Tile component of each TileGO to a different array
                      // SetTileMatrix();
                      //PlaceTiles();
       
    }



}

