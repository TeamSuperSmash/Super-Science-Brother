using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonReset : MonoBehaviour
{
    private void Awake()
    {
        _button = GetComponent<Button>();
        if (_button != null) return;
        Debug.Log("ButtonReset:Awake - Couldn't find the _button.");
    }

    public void ResetState()
    {
        _button.enabled = false;
        _button.enabled = true;
    }

    private Button _button;
}
