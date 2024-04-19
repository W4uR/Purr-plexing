using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    [Range(0.6f,1.6f)]
    [SerializeField]
    private float _iconRadius = 1f;

    [SerializeField]
    private Image _iconPrefab;

    private Transform _listener;
    private static Compass _instance;
    private static Dictionary<DisplayableAudio, Image> _audiosToDisplay;
    private float _calculatedIconRadius;

    private void Awake()
    {
        _instance = this;
        _audiosToDisplay = new Dictionary<DisplayableAudio, Image>();
    }

    private void Start()
    {
        _listener = Camera.main.transform;
        _calculatedIconRadius = _iconRadius * ((RectTransform)transform).sizeDelta.x * 0.5f;
    }

    void Update()
    {
        _calculatedIconRadius = _iconRadius * ((RectTransform)transform).sizeDelta.x * 0.5f;
        RenderAudios();
    }

    public static void AttachAudio(DisplayableAudio dp)
    {
        if (_audiosToDisplay.ContainsKey(dp)) return;

        var iconObject = Instantiate(_instance._iconPrefab, _instance.transform);
        iconObject.sprite = dp.GetIcon();
        _audiosToDisplay.Add(dp, iconObject);
    }

    public static void DetachAudio(DisplayableAudio dp)
    {
        Destroy(_audiosToDisplay[dp]);
        _audiosToDisplay.Remove(dp);
    }

    private void RenderAudios()
    {
        foreach (var pair in _audiosToDisplay)
        {
            RectTransform rt = (RectTransform)pair.Value.transform;

            Vector3 dirVector = (pair.Key.transform.position - _listener.position);

            if (dirVector.magnitude < 0.5f) // Egy mezõn vagyunk
            {
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one*1.3f;
                pair.Value.color = Color.green;
                return;
            }

            float radians = Vector3.SignedAngle(_listener.right, dirVector, Vector3.down) * Mathf.Deg2Rad;
            float x = Mathf.Cos(radians)* _calculatedIconRadius;
            float y = Mathf.Sin(radians)* _calculatedIconRadius;

            rt.anchoredPosition = new Vector2(x, y);
            rt.localScale = Vector3.one * pair.Key.GetClarity();

            if(pair.Key.GetClarity() < 1f)
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
