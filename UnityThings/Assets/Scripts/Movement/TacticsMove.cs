using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    List<Tile> selectableTiles = new List<Tile>();
    Stack<Tile> path = new Stack<Tile>();
    GameObject[] tiles;
    Tile currentTile;

    public bool turn = false;
    public bool moving = false;
    public int move = 2;
    public float jumpHeight = 1;
    public float moveSpeed = 4;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    float halfHeight = 0;

    protected void Init() // "Constructor"
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
        halfHeight = GetComponent<Collider>().bounds.extents.y;
        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile() // Obtiene la tile donde esta el personaje
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }

    public Tile GetTargetTile(GameObject target) // Tile objectivo seleccionada
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1))
        {
            tile = hit.collider.GetComponent<Tile>();
        }
        return tile;
    }

    public void ComputeAdjacencyList() // Busca las tiles adyacenetes al jugador
    {
        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbours(jumpHeight);
        }
    }

    public void FindSelectableTiles() // Busca las tiles que el jugador puede seleccionar para moverse
    {
        ComputeAdjacencyList();
        GetCurrentTile();
        Queue<Tile> process = new Queue<Tile>();
        process.Enqueue(currentTile);
        currentTile.visited = true; // Busca la tile actual
        while (process.Count > 0)
        {
            Tile t = process.Dequeue();
            selectableTiles.Add(t);
            t.selectable = true; // Agrega la primer tile procesada
            if (t.distance <= move)
            {
                foreach (Tile tiling in t.adjacencyList) // Agrega las tiles adyacentes
                {
                    if (!tiling.visited) // Solo agrega tiles que no hayamos procesado
                    {
                        tiling.parent = t;
                        tiling.visited = true;
                        tiling.distance = 1 + t.distance;
                        process.Enqueue(tiling);
                    }
                }
            }
        }
    }

    public void MoveToTile(Tile thyTile) // Esto crea un camino para el personaje que seguira y hara que se traslade a otro tile
    {
        path.Clear();
        thyTile.target = true;
        moving = true;
        Tile next = thyTile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    public void Move() // Con esta funcion se termina moviendo al personaje
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;
            if (Vector3.Distance(transform.position, target) >= 0.01f)
            {
                CalculateHeading(target);
                SetHorizontalVelocity();
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                transform.position = target;
                path.Pop();
            }
        }
        else
        {
            RemoveSelectableTiles();
            moving = false;
            TurnManager.EndTurn();
        }
    }

    protected void RemoveSelectableTiles() // Limpia la lista de tiles seleccionables
    {
        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tiler in selectableTiles)
        {
            tiler.Reset();
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

    public void BeginTurn()
    {
        turn = true;
        Debug.Log("BeginTurn");
    }

    public void EndTurn()
    {
        turn = false;
        Debug.Log("EndTurn");
    }
}
