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

    void Start()
    {
        TilesGO = GameObject.FindGameObjectsWithTag("tile");

        Tiles = new Tile[TilesGO.Length];
        CopyMatrix();

        tile_matrix = new Vector2Int(0, 0);
        SetTileMatrix(); //Sets the tile's positions like a matrix
        SetMap(); //Places the tiles depending on its matrix index

        HighlightTile(new Vector2Int(1, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CopyMatrix()
    {
        for(int i = 0; i < TilesGO.Length; ++i)
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
        return Tiles[map_width * v.x + v.y].GetComponent<Tile>();
    }

    void HighlightTile(Vector2Int v)
    {

        Tiles[map_width * v.x + v.y].GetComponent<MeshRenderer>().material = selection_material;
    }
  
}
