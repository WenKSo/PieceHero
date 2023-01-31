using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class PlayerSpawner3D : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef PlayerPrefab;
    public static Vector3 PlayerSpawnPos;
    private int counter;

    private void Awake()
    {
        PlayerSpawnPos = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        counter = 0;
    }

    /// <summary>
    /// Respawn all registred players on the level.
    /// </summary>
    /// <param name="runner"></param>
    public void RespawnPlayers(NetworkRunner runner)
    {
        if (!runner.IsClient)
        {
            PlayerSpawnPos = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            foreach (var player in runner.ActivePlayers)
            {
                SpawnPlayer(runner, player, counter, GameManager.Instance.GetPlayerData(player, runner).Nick.ToString());
                counter++;
            }
        }
    }

    private void SpawnPlayer(NetworkRunner runner, PlayerRef player, int PlayerNo, string nick = "")
    {
        if (runner.IsServer)
        {
            NetworkObject playerObj = runner.Spawn(PlayerPrefab, Vector3.zero, Quaternion.identity, player, InitializeObjBeforeSpawn);
            playerObj.GetComponent<Player3D>().playerNo = PlayerNo;
            PlayerData data = GameManager.Instance.GetPlayerData(player, runner);
            data.Instance = playerObj;
            playerObj.GetComponent<Player3D>().Nickname = data.Nick; 
        }
    }

    public void RemovePlayer(NetworkRunner runner, PlayerRef player)
    {
        PlayerData data = GameManager.Instance.GetPlayerData(player, runner);
        runner.Despawn(data.Instance);
    }

    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
        var behaviour = obj.GetComponent<Player3D>();
        behaviour.playerNo = counter;
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        RemovePlayer(runner, player);
    }

    #region UnusedCallbacks
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {
    }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }

    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }



    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {
    }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }
    #endregion
}
