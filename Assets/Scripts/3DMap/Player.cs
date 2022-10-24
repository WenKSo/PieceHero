using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    public NetworkPrefabRef Piece;

    #region NetworkedVariables
    [Networked(OnChanged = nameof(OnNickChanged))]
    public NetworkString<_16> Nickname { get; set; }
    public int PlayerID { get; private set; }
    [Networked]
    private NetworkBool Finished { get; set; }
    #endregion

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
        GameObject.FindGameObjectWithTag("Nick").GetComponentInChildren<NicknameText>().SetupNick(Nickname.ToString());
        SpawnPieces(Runner);
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
        //GetComponentInChildren<NicknameText>().SetupNick(Nickname.ToString());
    }

    /// <param name="runner"></param>
    public void SpawnPieces(NetworkRunner runner)
    {
        if(runner.IsServer)
        {
            GameObject[] startSquares = GameObject.FindGameObjectsWithTag("StartYellow");
            foreach (GameObject i in startSquares)
            {
                NetworkObject piece = Runner.Spawn(Piece, i.transform.position, Quaternion.identity, Runner.LocalPlayer, InitializeObjBeforeSpawn, predictionKey: null);
                piece.GetComponent<Piece>().currentSquare = i.GetComponent<Square>();
            }
        }
    }

    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
    }
}
