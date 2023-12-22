using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Object Tile", menuName = "Tiles/Object Tile")]
public class ObjectTile : Tile
{
    public GameObject[] objects;
}
