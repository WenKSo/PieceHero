using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player3D : NetworkBehaviour
{
    public NetworkPrefabRef Piece;

    [Networked(OnChanged = nameof(OnNickChanged))]
    public NetworkString<_16> Nickname { get; set; }
    [Networked] public int playerNo { get; set; }
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
        //GetComponentInChildren<NicknameText>().SetupNick(Nickname.ToString());
    }

    /// <param name="runner"></param>
    public void SpawnPiece(NetworkRunner runner)
    {
        if (runner.IsServer)
        {
            GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
            GameObject startBlock = blocks[0];
            for(int i=0;i<blocks.Length;i++)
            {
                if(blocks[i].GetComponent<TestBlock>().type == BlockType.Start)
                    startBlock = blocks[i];
            }
            foreach(var player in runner.ActivePlayers)
            PieceSpawnFunction(startBlock);
        }
    }

    private void PieceSpawnFunction(GameObject startBlock)
    {
        Runner.Spawn(
            Piece,
            startBlock.transform.position, 
            Quaternion.identity, 
            PlayerID,
            InitializeObjBeforeSpawn,
            predictionKey: null
            );
    }

    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
        
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
