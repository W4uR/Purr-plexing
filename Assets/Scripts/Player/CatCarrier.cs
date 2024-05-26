using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCarrier : MonoBehaviour
{
    [SerializeField]
    private AudioGroup catPickUpSoundSFX;
    [SerializeField]
    private AudioClip levelFinishedSFX;

    [SerializeField]
    private AudioSource audioSource;


    [SerializeField]
    private string catTag;
    [SerializeField]
    private string destinationTag;


    private HashSet<Cat> heldCats = new HashSet<Cat>();
    private int deliveredCats = 0;

    private void OnEnable()
    {
        LevelManager.OnLevelLoaded += OnLevelLoaded;       
    }

    private void OnDisable()
    {
        LevelManager.OnLevelLoaded -= OnLevelLoaded;
    }

    private void OnLevelLoaded(int index)
    {
        heldCats = new HashSet<Cat>();
        deliveredCats = 0;
    }

    void PickUpCat(Cat cat)
    {
        if (heldCats.Contains(cat)) return;

        EffectDisplay.DisplayEffect(Color.green, 0.3f);
        Debug.Log($"CatCarrier::PickUpCat | Called | heldCats.Count = {heldCats.Count}");
        heldCats.Add(cat);
        Debug.Log($"CatCarrier::PickUpCat | Executing | heldCats.Count = {heldCats.Count}");
        cat.transform.position = transform.position;
        cat.transform.SetParent(transform, true);
        audioSource.PlayOneShot(catPickUpSoundSFX.GetRandomClip());
        cat.GetComponent<Scareable>().enabled = false;
    }

    void DeliverHeldCats()
    {
        Debug.Log($"CatCarrier::DeliverHeldCats | Called | heldCats.Count = {heldCats.Count}");
        if (heldCats.Count == 0) return;

        deliveredCats += heldCats.Count;
        Debug.Log($"CatCarrier::DeliverHeldCats | Executing | deliveredCats.Count = {deliveredCats}");

        foreach (var cat in heldCats)
        {
            Destroy(cat.gameObject);
        }
        heldCats.Clear();

        Debug.Log($"CatCarrier::DeliverHeldCats | Executing | LevelManager.GetNumberOfCatsOnCurrentLevel() = {LevelManager.GetNumberOfCatsOnCurrentLevel()}");
        if (deliveredCats == LevelManager.GetNumberOfCatsOnCurrentLevel())
        {
            Debug.Log("Every cat has been saved on this level.");
            audioSource.PlayOneShot(levelFinishedSFX);
            GameManager.Instance.LevelFinished();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(destinationTag))
        {
            DeliverHeldCats();
        }
        else if (other.tag.Equals(catTag))
        {
            PickUpCat(other.GetComponent<Cat>());
        }
    }
}
