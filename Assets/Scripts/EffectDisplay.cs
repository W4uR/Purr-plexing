using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplay : MonoBehaviour
{
    [SerializeField]
    private Image effectImage;

    private static EffectDisplay s_instance;
    // Start is called before the first frame update
    void Awake()
    {
        s_instance = this;
    }


    public static IEnumerator DisplayEffect(Color color, float duration)
    {
        s_instance.effectImage.color = color;

        s_instance.effectImage.enabled = true;
        yield return new WaitForSeconds(duration);
        s_instance.effectImage.enabled = false;
    }
}
