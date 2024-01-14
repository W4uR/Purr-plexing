using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // A falakat sorrendje fontos!!! (�szak, Kelet, D�l, Nyugat)
    [Header("Walls (N,E,S,W)")]
    [SerializeField]
    private GameObject[] walls;

    private FloorMaterial floorMaterial;

    /// <summary>
    /// Be�ll�t egy padl� t�pust a mez�nek.
    /// </summary>
    /// <param name="floorMaterial">A be�ll�tand� padl� t�pus</param>
    public void SetFloorMaterial(FloorMaterial floorMaterial)
    {
        this.floorMaterial = floorMaterial;
    }

    /// <summary>
    /// Elrejti a param�terben adott ir�nyban l�v� falat.
    /// </summary>
    /// <param name="direction">Az elrejtend� fal glob�lis ir�nya</param>
    public void HideWall(AbsoluteDirection direction)
    {
        walls[(int)direction].SetActive(false);
    }

    /// <summary>
    /// Visszaad egy v�letlenszer� l�p�shangeffektet a padl� t�pus�nak megfelel�en
    /// </summary>
    /// <returns></returns>
    public AudioClip GetRandomStepSound()
    {
        return floorMaterial.GetRandomSound();
    }
}
