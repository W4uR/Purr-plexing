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

    public void hideWall(AbsoluteDirection direction)
    {
        walls[((int)direction)].SetActive(false);
    }

    public AudioClip getRandomStepSound()
    {
        int randomIndex = Random.Range(0, floorMaterial.stepSounds.Count);
        return floorMaterial.stepSounds[randomIndex];
    }
}
