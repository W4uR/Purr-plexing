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

    private AudioGroup stepSounds;

    /// <summary>
    /// Be�ll�t egy padl� t�pust a mez�nek.
    /// </summary>
    /// <param name="floorMaterial">A be�ll�tand� padl� t�pus</param>
    public void SetFloorMaterial(AudioGroup stepSounds)
    {
        this.stepSounds = stepSounds;
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
    /// Visszaadja a padl�nak megfelel� l�p�shang kollekci�t
    /// </summary>
    /// <returns></returns>
    public AudioGroup GetStepSounds()
    {
        return stepSounds;
    }
}
