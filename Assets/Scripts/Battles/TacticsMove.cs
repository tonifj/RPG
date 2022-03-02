using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public int move = 5;
    public int jumpHeight = 2;
    public int moveSpeed = 2;

    Vector3 velocity = new Vector3(); //speed the player mooves from tile to tile
    Vector3 heading = new Vector3(); //direction the character is facing

    float halfHeight = 0;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y; //used to jump tiles
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject); //return the tile where the unit stands
        
    }

    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;

        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
            tile.current = true;
        }
        return tile;
    }

    public void ComputeAdjacencyLists()
    {
         tiles = GameObject.FindGameObjectsWithTag("tile"); //if the map changes size, do this here

        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(jumpHeight);
        }
    }

    public void FindSelectableTiles() //BFS
    {
        ComputeAdjacencyLists();
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.BeingVisited();

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();
            selectableTiles.Add(t);
            t.selectable = true; 

            if(t.distance < move)
            {
                foreach (Tile tile in t.adjacents)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;

                        process.Enqueue(tile);
                    }
                }
            }
            
        }
    }
}
