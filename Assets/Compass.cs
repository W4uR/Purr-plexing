using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    [SerializeField]
    private Image iconPrefab;

    private Transform listener;
    private float calculatedIconRadius;

    private static Compass s_instance;
    private static Dictionary<DisplayableAudio, Image> s_audiosToDisplay;

    private void Awake()
    {
        s_instance = this;
        s_audiosToDisplay = new Dictionary<DisplayableAudio, Image>();
    }

    private void Start()
    {
        listener = Camera.main.transform;
        calculatedIconRadius = ((RectTransform)transform).sizeDelta.x * 0.6f;
    }

    void LateUpdate()
    {
        RenderAudios();
    }

    public static void AttachAudio(DisplayableAudio dp)
    {
        if (s_audiosToDisplay.ContainsKey(dp)) return;

        var iconObject = Instantiate(s_instance.iconPrefab, s_instance.transform);
        iconObject.enabled = false;
        iconObject.sprite = dp.Icon;
        s_audiosToDisplay.Add(dp, iconObject);
    }

    public static void DetachAudio(DisplayableAudio dp)
    {
        s_audiosToDisplay[dp].enabled = false;
        Destroy(s_audiosToDisplay[dp]);
        s_audiosToDisplay.Remove(dp);
    }
    
    private void RenderAudios()
    {
        foreach (var pair in s_audiosToDisplay)
        {

            pair.Value.enabled = pair.Key.Active;
            
            if (!pair.Key.Active)
                continue;
            
            RectTransform rt = (RectTransform)pair.Value.transform;

            Vector3 dirVector = Vector3.ProjectOnPlane(pair.Key.transform.position - listener.position, Vector3.up);
            

            if (dirVector.magnitude < 0.5f) // Egy mezõn vagyunk
            {
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one * 1.3f;
                pair.Value.color = Color.green;
                continue;
            }

            float radians = Vector3.SignedAngle(listener.right, dirVector,Vector3.down) * Mathf.Deg2Rad;
            float x = Mathf.Cos(radians)* calculatedIconRadius;
            float y = Mathf.Sin(radians)* calculatedIconRadius;

            rt.anchoredPosition = new Vector2(x, y);
            rt.localScale = Vector3.one * pair.Key.Clarity;

            if(pair.Key.Clarity < 1f)
            {
                pair.Value.color = Color.white;
            }
            else
            {
                pair.Value.color = Color.yellow;
            }
        }
    }

}
