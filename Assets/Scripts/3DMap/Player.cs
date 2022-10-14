using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    
    public Square currentSquare { get; set; }
    [Networked]
    public ColorType color { get; set; }
    [Networked(OnChanged = nameof(OnNickChanged))]
    public NetworkString<_16> Nickname { get; set; }
    public int PlayerID { get; private set; }

    public override void Spawned()
    {
        PlayerID = Object.InputAuthority;

        if (Object.HasInputAuthority)
        {
            if (Nickname == string.Empty)
            {
                RPC_SetNickname(PlayerPrefs.GetString("Nick"));
            }
        }
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SetNickname(string nick)
    {
        Nickname = nick;
    }

    public static void OnNickChanged(Changed<Player> changed)
    {
        changed.Behaviour.OnNickChanged();
    }

    private void OnNickChanged()
    {
        GetComponentInChildren<NicknameText>().SetupNick(Nickname.ToString());
    }

    public void move(int steps){
        //当已经在最后的胜利通道时
        Square temp = currentSquare;
        int rsteps = steps;
        for(int i=0;i<steps;i++){
            temp = temp.next;
            if(currentSquare.type == SquareType.Finish && i<(steps-1))
            {
                rsteps = i;
                break;
            }
        }

        for(int i=0;i<rsteps;i++)
        {
            if(currentSquare.type == SquareType.Special)
            {
                currentSquare = currentSquare.next2;
            }
            else
            {
                currentSquare = currentSquare.next;
            }
            updatePos();   
        }
    }

    public void updatePos()
    {
        Vector3 pos = currentSquare.transform.position;
        pos.y = transform.position.y;
        transform.position = pos;
    } 
}
