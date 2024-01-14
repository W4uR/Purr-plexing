using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // A falakat sorrendje fontos!!! (Észak, Kelet, Dél, Nyugat)
    [Header("Walls (N,E,S,W)")]
    [SerializeField]
    private GameObject[] walls;

    private FloorMaterial floorMaterial;

    /// <summary>
    /// Beállít egy padló típust a mezõnek.
    /// </summary>
    /// <param name="floorMaterial">A beállítandó padló típus</param>
    public void SetFloorMaterial(FloorMaterial floorMaterial)
    {
        this.floorMaterial = floorMaterial;
    }

    /// <summary>
    /// Elrejti a paraméterben adott irányban lévõ falat.
    /// </summary>
    /// <param name="direction">Az elrejtendõ fal globális iránya</param>
    public void HideWall(AbsoluteDirection direction)
    {
        walls[(int)direction].SetActive(false);
    }

    /// <summary>
    /// Visszaad egy véletlenszerû lépéshangeffektet a padló típusának megfelelõen
    /// </summary>
    /// <returns></returns>
    public AudioClip GetRandomStepSound()
    {
        return floorMaterial.GetRandomSound();
    }
}
