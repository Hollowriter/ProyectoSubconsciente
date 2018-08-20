using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript // Como no va a estar agregado en ningun objeto, no tiene porque ser monobehaviour
{
    [MenuItem("Tools/Assign_Tile_Material")]
    public static void AssignTileMaterial()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        Material material = Resources.Load<Material>("Tile");
        foreach (GameObject t in tiles)
        {
            t.GetComponent<Renderer>().material = material;
        }
    }
   [MenuItem("Tools/Assign_Tile_Script")]
    public static void AssignTileScript()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        foreach (GameObject t in tiles)
        {
            t.AddComponent<Tile>();
        }
    }
}
