using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private AudioSource menuLabel;
    [SerializeField]
    private Transform inputsParent;


    private void OnEnable()
    {
        StartCoroutine(ReadMenu());
    }

    private void OnDisable()
    {
        menuLabel.Stop();
        StopAllCoroutines();
    }

    IEnumerator ReadMenu()
    {
        while(menuLabel.clip == null)
        {
            yield return null;
        }
        menuLabel.Play();
        yield return new WaitForSeconds(menuLabel.clip.length+.1f);
        if(!inputsParent.GetComponentsInChildren<Transform>().Select(x=>x.gameObject).ToList().Contains(EventSystem.current.currentSelectedGameObject))
            EventSystem.current.SetSelectedGameObject(inputsParent.GetComponentsInChildren<AudioSource>(false).First().gameObject);
    }

}
