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
    [Networked] public NetworkButtons ButtonsPrevious { get; set; }
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
                NetworkObject piece = Runner.Spawn(Piece, i.transform.position, Quaternion.identity, PlayerID, InitializeObjBeforeSpawn, predictionKey: null);
                piece.GetComponent<Piece>().squareId = i.GetComponent<Square>().id;
            }
        }
    }

    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
    }

    public override void FixedUpdateNetwork() 
    {
        if (GetInput<PlayerInput>(out var input) == false) return;
       
        if (Runner.IsForward && Runner.IsFirstTick)
        {
            // compute pressed/released state
            var pressed = input.Buttons.GetPressed(ButtonsPrevious);
            var released = input.Buttons.GetReleased(ButtonsPrevious);

            // store latest input as 'previous' state we had
            ButtonsPrevious = input.Buttons;

            if (pressed.IsSet(PlayerButtons.Roll))
            {
                roll();
                Log.Debug("Pressed.");
            }
        }    
    }

    void roll(){
        MapManager mapManager = FindObjectOfType<MapManager>();
        mapManager.roll();
    }
}
