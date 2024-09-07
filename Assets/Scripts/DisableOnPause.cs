using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnPause : MonoBehaviour
{
    void Start()
    {
        GamePauser.Instance?.componentsToDisableOnPause.Add(this);
    }

    private void OnDestroy()
    {
        GamePauser.Instance?.componentsToDisableOnPause.Remove(this);
    }
}
