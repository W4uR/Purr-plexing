using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCarrier : MonoBehaviour
{
    [SerializeField]
    private AudioGroup _catPickUpSoundSFX;
    [SerializeField]
    private AudioSource _audioSource;

    private List<Cat> _heldCats;
    private int _deliveredCats = 0;

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
        _heldCats = new List<Cat>();
        _deliveredCats = 0;
    }

    void AttachCat(Cat cat)
    {
        _heldCats.Add(cat);
        cat.transform.position = transform.position;
        cat.transform.SetParent(transform, true);
        _audioSource.PlayOneShot(_catPickUpSoundSFX.GetRandomClip());
    }

    void DeliverHeldCats()
    {
        if (_heldCats.Count == 0) return;
        _deliveredCats += _heldCats.Count;
        foreach (var cat in _heldCats)
        {
            Destroy(cat.gameObject);
        }
        _heldCats.Clear();
        if (_deliveredCats == LevelManager.GetNumberOfCatsOnCurrentLevel())
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
