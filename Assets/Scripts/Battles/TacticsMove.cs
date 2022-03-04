using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public bool moving = false;
    public int move = 5;
    public int jumpHeight = 2;
    public int moveSpeed = 2;
    public float jumpVelocity = 4.5f;

    bool fallingDown = false;
    bool jumpingUp = false;
    bool movingToEdge = false;


    Vector3 velocity = new Vector3(); //speed the player mooves from tile to tile
    Vector3 heading = new Vector3(); //direction the character is facing
    Vector3 jumpTarget = new Vector3();

    float halfHeight = 0;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y; //used to jump tiles

    }

    public void SetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject); // tile where the unit stands       
    }

    public Tile GetCurrenntTile()
    {
        return GetTargetTile(gameObject);
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
        SetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.BeingVisited();

        while (process.Count > 0)
        {
            Tile t = process.Dequeue();
            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
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

    public void MoveToTile(Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;

        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    protected void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            //calculate the unit's position on top on the target tile
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump = transform.position.y != target.y;

                if (jump)
                {
                    Jump(target);
                }

                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                //Motion
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }

            else
            {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }
        }

        else
        {
            RemoveSelectableTiles();
            moving = false;
        }
    }

    protected void RemoveSelectableTiles()
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }

        foreach (Tile tile in selectableTiles)
        {
            tile.ResetTile();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }

    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    void ResetUnit()
    {
        moving = false;
        GetTargetTile(gameObject).target = false;
        path.Clear();
    }

    void Jump(Vector3 target)
    {
        //state machine
        if (fallingDown)
        {
            FallDownward(target);
        }

        else if (jumpingUp)
        {
            JumpUpward(target);
        }

        else if (movingToEdge)
        {
            MoveToEdge();
        }

        else
        {
            PrepareJump(target);
        }
    }

    void PrepareJump(Vector3 target)
    {
        float targetY = target.y;
        target.y = transform.position.y;

        CalculateHeading(target);

        if (transform.position.y > targetY)
        {
            fallingDown = false;
            jumpingUp = false;
            movingToEdge = true;

            jumpTarget = transform.position + (target - transform.position) / 2;
        }

        else
        {

            fallingDown = false;
            jumpingUp = true;
            movingToEdge =true;

            velocity = heading * moveSpeed / 3; //so it gets close to the edge slower

            float difference = targetY - transform.position.y;

            velocity.y = jumpVelocity * (0.5f + difference / 2);
        }

    }

    void FallDownward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y <= target.y)
        {
            fallingDown = false;
            jumpingUp = false;
            movingToEdge = false;

            Vector3 pos = transform.position;
            pos.y = target.y;
            transform.position = pos;

            velocity = new Vector3();
        }
    }

    void JumpUpward(Vector3 target)
    {
        velocity += Physics.gravity * Time.deltaTime;

        if (transform.position.y > target.y)
        {
            jumpingUp = false;
            fallingDown = true;
        }
    }

    void MoveToEdge()
    {
        if (Vector3.Distance(transform.position, jumpTarget) >= 0.05f)
        {
            SetHorizontalVelocity();
        }

        else
        {
            movingToEdge = false;
            fallingDown = true;

            velocity /= 3;
        }
    }
}
