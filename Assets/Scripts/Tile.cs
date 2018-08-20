using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;
    public List<Tile> adjacencyList = new List<Tile>();

    public bool visited = false;
    public Tile parent = null;
    public int distance = 0;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (current) // Tile en la que esta parado el jugador
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else if (target) // Tile objetivo
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else if (selectable) // Tile que se puede seleccionar
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
	}

    public void Reset()
    {
        adjacencyList.Clear();
        walkable = true;
        current = false;
        target = false;
        selectable = false;
        visited = false;
        parent = null;
        distance = 0;
    }
    public void FindNeighbours(float jumpHeight) // Busca las tiles adyacentes
    {
        Reset();
        CheckTile(Vector3.forward, jumpHeight);
        CheckTile(-Vector3.forward, jumpHeight);
        CheckTile(Vector3.right, jumpHeight);
        CheckTile(-Vector3.right, jumpHeight);
    }

    public void CheckTile(Vector3 direction, float jumpHeight) // Revisa en que estado esta la tile
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 * jumpHeight) / 2.0f, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            Tile tile = item.GetComponent<Tile>();
            if (tile != null && tile.walkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, -Vector3.up, out hit, 1)) // Agrega a la lista de tiles seleccionables si no tiene nada encima
                {
                    adjacencyList.Add(tile);
                }
            }
        }
    }
}
