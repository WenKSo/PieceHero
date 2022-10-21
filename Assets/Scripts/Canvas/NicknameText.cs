using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class NicknameText : NetworkBehaviour
{   
    private TMP_Text _text;
    [Networked] private int row{get;set;}

    public void SetupNick(string nick)
    {
        if (_text == null)
            _text = GetComponent<TMP_Text>();
        _text.text = nick;
    }

    public void SetNick(int num)
    {
        row = num;
    }

     public override void FixedUpdateNetwork()
    {
        _text.text = row.ToString();
    }
}
