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

    private AudioGroup stepSounds;

    /// <summary>
    /// Beállít egy padló típust a mezõnek.
    /// </summary>
    /// <param name="floorMaterial">A beállítandó padló típus</param>
    public void SetFloorMaterial(AudioGroup stepSounds)
    {
        this.stepSounds = stepSounds;
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
    /// Visszaadja a padlónak megfelelõ lépéshang kollekciót
    /// </summary>
    /// <returns></returns>
    public AudioGroup GetStepSounds()
    {
        return stepSounds;
    }
}
