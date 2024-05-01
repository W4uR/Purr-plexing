using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField]
    private AudioSource menuLabel;
    [SerializeField]
    private AudioSource firstSelectedInput;


    private void OnEnable()
    {
        StartCoroutine(ReadMenu());
    }

    private void OnDisable()
    {
        menuLabel.Stop();
        firstSelectedInput.Stop();
        StopAllCoroutines();
    }

    IEnumerator ReadMenu()
    {
        while(menuLabel.clip == null)
        {
            yield return null;
        }
        menuLabel.Play();
        yield return new WaitForSeconds(menuLabel.clip.length+.6f);
        EventSystem.current.SetSelectedGameObject(firstSelectedInput.gameObject);
    }

}
