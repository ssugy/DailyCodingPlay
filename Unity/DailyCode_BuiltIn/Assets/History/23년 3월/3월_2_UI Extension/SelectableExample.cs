using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectableExample : MonoBehaviour
{
    GameObject sel;

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null
            && EventSystem.current.currentSelectedGameObject.CompareTag("Player")
            && EventSystem.current.currentSelectedGameObject != sel
            )
            sel = EventSystem.current.currentSelectedGameObject;
        else if (sel != null
                && sel.CompareTag("Player")
                && EventSystem.current.currentSelectedGameObject == null
                //&& EventSystem.current.currentSelectedGameObject.CompareTag("Player")
                )
            EventSystem.current.SetSelectedGameObject(sel);
    }
}
