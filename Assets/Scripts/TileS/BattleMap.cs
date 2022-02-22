using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMap : MonoBehaviour
{
    GameObject[] TilesGO;
    Tile[] Tiles;
    Vector2 tile_matrix; //defines the matrix of tiles

    void Start()
    {
        TilesGO = GameObject.FindGameObjectsWithTag("tile");

        Tiles = new Tile[TilesGO.Length];
        CopyMatrix();

        tile_matrix = new Vector2(0, 0);
        SetTileMatrix();
        SetMap();
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

            if (tile_matrix.x >= 10)
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
  
}
