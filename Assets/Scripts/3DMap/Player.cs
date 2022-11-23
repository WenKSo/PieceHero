using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Player : NetworkBehaviour
{
    public NetworkPrefabRef Piece;

    #region NetworkedVariables
    [Networked] public int playerNo { get; set; }
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
    public void SpawnPieces(NetworkRunner runner){
        if (runner.IsServer)
        {
            GameObject[] startYellowSquares = GameObject.FindGameObjectsWithTag("StartYellow");
            GameObject[] startRedSquares = GameObject.FindGameObjectsWithTag("StartRed");
            GameObject[] startBlueSquares = GameObject.FindGameObjectsWithTag("StartBlue");
            GameObject[] startGreenSquares = GameObject.FindGameObjectsWithTag("StartGreen");
            if (playerNo == 0)
            {
                foreach (GameObject i in startYellowSquares)
                {
                    PieceSpawnFunction(i);
                }
            }
            else if (playerNo == 1)
            {
                foreach (GameObject i in startRedSquares)
                {
                    PieceSpawnFunction(i);
                }
            }
            else if (playerNo == 2)
            {
                foreach (GameObject i in startBlueSquares)
                {
                    PieceSpawnFunction(i);
                }
            }
            else if (playerNo == 3)
            {
                foreach (GameObject i in startGreenSquares)
                {
                    PieceSpawnFunction(i);
                }
            }
        }
    }

    private void PieceSpawnFunction(GameObject startSquare)
    {
        Runner.Spawn(
            Piece,
            startSquare.transform.position, 
            Quaternion.identity, 
            PlayerID,
            (Runner, NO) => PieceOnBeforeSpawn(Runner, NO, startSquare.GetComponent<Square>()),
            predictionKey: null
            );
    }

    private void PieceOnBeforeSpawn(NetworkRunner runner, NetworkObject justSpawnedNO, Square startSquare)
    {
        justSpawnedNO.GetComponent<Piece>().squareId = startSquare.id;
    }

    public override void FixedUpdateNetwork() 
    {
        if (GetInput<PlayerInput>(out var input) == false) return;
        
        var pressed = input.Buttons.GetPressed(ButtonsPrevious);
        var released = input.Buttons.GetReleased(ButtonsPrevious);

        // store latest input as 'previous' state we had
        ButtonsPrevious = input.Buttons;

        if (pressed.IsSet(PlayerButtons.Roll))
        {
            Log.Debug("Pressed.");
            MapManager mapManager = FindObjectOfType<MapManager>();
            mapManager.piece.SetSquareId(10);
        } 
    }



    public void roll(){
        MapManager mapManager = FindObjectOfType<MapManager>();
        //mapManager.roll();
        mapManager.piece.squareId = 0;
    }
}
