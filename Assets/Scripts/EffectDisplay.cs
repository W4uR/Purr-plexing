using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectDisplay : MonoBehaviour
{
    [SerializeField]
    private Image effectImage;

    private float alphaVal;

    private static EffectDisplay s_instance;
    // Start is called before the first frame update
    void Awake()
    {
        s_instance = this;
        alphaVal = effectImage.color.a;
    }

    public static void DisplayEffect(Color color, float duration)
    {
        s_instance.StartCoroutine(Display(color, duration));
    }

    private static IEnumerator Display(Color color, float duration)
    {
        s_instance.effectImage.color = new Color(color.r,color.g,color.b, s_instance.alphaVal);
        s_instance.effectImage.enabled = true;
        yield return new WaitForSeconds(duration);
        s_instance.effectImage.enabled = false;
    }
}
