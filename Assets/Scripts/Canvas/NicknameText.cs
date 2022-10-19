using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class NicknameText : MonoBehaviour
{
    private TMP_Text _text;

    public void SetupNick(string nick)
    {
        Log.Debug("fuck " + nick);
        if (_text == null)
            _text = GetComponent<TMP_Text>();
        _text.text = nick;
    }
}
