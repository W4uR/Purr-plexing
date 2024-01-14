using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSaver : MonoBehaviour
{

    List<Cat> heldCats;
    int catsToSave;
    int savedCats = 0;


    private void OnEnable()
    {
        LevelManager.levelLoaded += OnLevelLoaded;       
    }

    private void OnDisable()
    {
        LevelManager.levelLoaded -= OnLevelLoaded;
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
        cat.isCarried = true;
        cat.transform.SetParent(transform, false);
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
            return;
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
            case "Spawn":
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
