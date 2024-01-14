using UnityEngine.EventSystems;
using UnityEngine;

public class UISelectionHandler : MonoBehaviour, ISelectHandler
{
    [System.Serializable]
    public class ElementSelectedEvent : UnityEngine.Events.UnityEvent<TextAudio.Phrase> { }

    public ElementSelectedEvent onElementSelected;

    public TextAudio.Phrase phrase;

    public void OnSelect(BaseEventData eventData)
    {
        // Trigger the UnityEvent with the selected element type
        onElementSelected.Invoke(phrase);
    }
}