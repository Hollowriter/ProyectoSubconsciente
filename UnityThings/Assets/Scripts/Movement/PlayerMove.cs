using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove
{

	// Use this for initialization
	void Start ()
    {
        Init();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse(); // Este control despues lo reemplazaremos con lo que terminaremos moviendo al jugador (Sino sera cambiarlo en el GDD)
        }
        else
        {
            Move();
        }
	}

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tile")
                {
                    Tile t = hit.collider.GetComponent<Tile>();
                    if (t.selectable)
                    {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
