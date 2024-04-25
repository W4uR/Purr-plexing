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

    void AttachCat(Cat cat)
    {
        heldCat = cat;
        cat.transform.position = transform.position;
        cat.transform.SetParent(transform, true);
        audioSource.PlayOneShot(catPickUpSoundSFX.GetRandomClip());
    }

    void DeliverHeldCats()
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
        switch (other.tag)
        {
            case "Nest":
                DeliverHeldCats();
                break;
            case "Cat":
                AttachCat(other.GetComponent<Cat>());
                break;
            default:
                break;
        }
    }
}
