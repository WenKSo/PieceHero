using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player3D : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnNickChanged))]
    public NetworkString<_16> Nickname { get; set; }
    [Networked] public int playerNo { get; set; }

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            if (Nickname == string.Empty)
            {
                RPC_SetNickname(PlayerPrefs.GetString("Nick"));
            }
        }
        GameObject.FindGameObjectWithTag("Nick").GetComponentInChildren<NicknameText>().SetupNick(Nickname.ToString());
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SetNickname(string nick)
    {
        Nickname = nick;
    }

    public static void OnNickChanged(Changed<Player3D> changed)
    {
        changed.Behaviour.OnNickChanged();
    }

    private void OnNickChanged()
    {
        //GetComponentInChildren<NicknameText>().SetupNick(Nickname.ToString());
    }

}
