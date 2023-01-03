using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMask : MonoBehaviour
{
    public GameObject ground_0;
    public GameObject ground_1;
    public GameObject ground_2;
    public GameObject ground_5;
    public GameObject ground_6;
    public GameObject ground_7;
    public GameObject ground_12;
    public GameObject ground_13;
    public GameObject ground_14;
    public GameObject ground_15;
    public GameObject ground_17;
    public GameObject ground_19;
    public GameObject ground_20;

    private void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        Vector3Int startCoord = tilemap.origin;
        Vector3Int size = tilemap.size;

        for (int x = startCoord.x; x < startCoord.x + size.x; x++)
        {
            for (int y = startCoord.y; y < startCoord.y + size.y; y++)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, startCoord.z));
                
                if (tile != null)
                {
                    GameObject inst = null;

                    switch (tile.name) {
                        case "ground_0":
                            inst = ground_0;
                            break;
                        
                        case "ground_1":
                            inst = ground_1;
                            break;

                        case "ground_2":
                            inst = ground_2;
                            break;

                        case "ground_5":
                            inst = ground_5;
                            break;

                        case "ground_6":
                            inst = ground_6;
                            break;

                        case "ground_7":
                            inst = ground_7;
                            break;

                        case "ground_12":
                            inst = ground_12;
                            break;

                        case "ground_13":
                            inst = ground_13;
                            break;

                        case "ground_14":
                            inst = ground_14;
                            break;

                        case "ground_15":
                            inst = ground_15;
                            break;

                        case "ground_17":
                            inst = ground_17;
                            break;

                        case "ground_19":
                            inst = ground_19;
                            break;

                        case "ground_20":
                            inst = ground_20;
                            break;
                    }

                    Vector3 coord = tilemap.CellToWorld(new Vector3Int(x, y, startCoord.z)) + new Vector3(0.5f, 0.5f, 0);
                    Instantiate(inst, coord, Quaternion.identity, transform);
                }
            }
        }
    }
}