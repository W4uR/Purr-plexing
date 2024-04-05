using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSaver : MonoBehaviour
{
    private List<Cat> heldCats;
    private int catsToSave;
    private int savedCats = 0;

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
        heldCats = new List<Cat>();
        savedCats = 0;
        catsToSave = LevelManager.GetNumberOfCatsOnCurrentLevel();
        print("Cats to save on this level: " + catsToSave);
    }

    void AttachCat(Cat cat)
    {
        heldCats.Add(cat);
        cat.SetCarried(true);
        cat.transform.position = transform.position;
        cat.transform.SetParent(transform, true);
        GlobalSoundEffects.Instance.PlayCatPickUp();
    }

    void SaveCats()
    {
        if (heldCats.Count == 0) return;
        savedCats += heldCats.Count;
        if(savedCats == catsToSave)
        {
            Debug.Log("Every cat has been saved on this level.");
            GlobalSoundEffects.Instance.PlayAllCatsSaved();
            GameManager.Instance.LevelFinished();
        }
        foreach (var cat in heldCats)
        {
            Destroy(cat.gameObject);
        }
        heldCats.Clear();
        GlobalSoundEffects.Instance.PlayCatSaved();
    }


    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Nest":
                SaveCats();
                break;
            case "Cat":
                AttachCat(other.GetComponent<Cat>());
                break;
            default:
                break;
        }
    }
}
