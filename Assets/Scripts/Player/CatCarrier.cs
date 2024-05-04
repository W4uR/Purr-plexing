using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCarrier : MonoBehaviour
{
    [SerializeField]
    private AudioGroup catPickUpSoundSFX;
    [SerializeField]
    private AudioSource audioSource;


    [SerializeField]
    private string catTag;
    [SerializeField]
    private string destinationTag;


    private Cat heldCat = null;
    private int deliveredCats = 0;

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += OnLevelLoaded;       
    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded()
    {
        heldCat = null;
        deliveredCats = 0;
    }

    void PickUpCat(Cat cat)
    {
        heldCat = cat;
        cat.transform.position = transform.position;
        cat.transform.SetParent(transform, true);
        audioSource.PlayOneShot(catPickUpSoundSFX.GetRandomClip());
    }

    void DeliverHeldCat()
    {
        if (heldCat == null) return;

        deliveredCats++;
        Destroy(heldCat);
        heldCat = null;

        if (deliveredCats == LevelManager.GetNumberOfCatsOnCurrentLevel())
        {
            Debug.Log("Every cat has been saved on this level.");
            StartCoroutine(GameManager.Instance.LevelFinished());
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(destinationTag))
        {
            DeliverHeldCat();
        }
        else if (other.tag.Equals(catTag))
        {
            PickUpCat(other.GetComponent<Cat>());
        }
    }
}
