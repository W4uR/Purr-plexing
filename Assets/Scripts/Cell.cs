using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    GameObject[] walls;

    FloorMaterial floorMaterial;

    public void SetFloorMaterial(FloorMaterial floorMaterial)
    {
        this.floorMaterial = floorMaterial;
    }

    public void hideWall(Direction direction)
    {
        walls[((int)direction)].SetActive(false);
    }
}
