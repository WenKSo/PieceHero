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
    [Networked] public NetworkButtons ButtonsPrevious { get; set; }

    [Networked] public Piece3D myPiece { get; private set; }

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
        SpawnPiece(Runner);
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
            Debug.Log(startBlock.transform.position);
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
            (Runner, NO) => PieceOnBeforeSpawn(Runner, NO, startBlock.GetComponent<TestBlock>()),
            predictionKey: null
            );
    }

    private void PieceOnBeforeSpawn(NetworkRunner runner, NetworkObject justSpawnedNO, TestBlock startBlock)
    {
        justSpawnedNO.GetComponent<Piece3D>().BlockId = startBlock.id;
        myPiece = justSpawnedNO.GetComponent<Piece3D>();
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

    public override void FixedUpdateNetwork() 
    {
        if (GetInput<PlayerInput>(out var input) == false) return;
        if (Runner.IsForward && Runner.IsFirstTick){
            var pressed = input.Buttons.GetPressed(ButtonsPrevious);
            var released = input.Buttons.GetReleased(ButtonsPrevious);

            // store latest input as 'previous' state we had
            ButtonsPrevious = input.Buttons;

            if (pressed.IsSet(PlayerButtons.Roll))
            {
                Log.Debug("Pressed.");
                myPiece.currentBlock = myPiece.currentBlock.next;
                myPiece.BlockId = myPiece.currentBlock.id;
                if (Object.HasInputAuthority)
                {
                    Log.Debug(PlayerID);
                    InputVariables.instance.onClicked = false;
                }
            }
        }
    }

}
