using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class PlayerSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    public NetworkPrefabRef PlayerPrefab;
    public static Vector3 PlayerSpawnPos;

    private void Awake()
    {
        PlayerSpawnPos = GameObject.FindGameObjectWithTag("Respawn").transform.position;
        Log.Debug(PlayerSpawnPos);
    }

    /// <summary>
    /// Respawn all registred players on the level.
    /// </summary>
    /// <param name="runner"></param>
    public void RespawnPlayers(NetworkRunner runner)
    {
        if (!runner.IsClient)
        {
            Log.Debug("Debug:Start to Spawn");
            PlayerSpawnPos = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            foreach (var player in runner.ActivePlayers)
            {
                Log.Debug("Player: " + GameManager.Instance.GetPlayerData(player, runner).Nick.ToString());
                SpawnPlayer(runner, player, GameManager.Instance.GetPlayerData(player, runner).Nick.ToString());
            }
        }
    }

    private void SpawnPlayer(NetworkRunner runner, PlayerRef player, string nick = "")
    {
        if (runner.IsServer)
        {
            Log.Debug("Spawn Player...");
            NetworkObject playerObj = runner.Spawn(PlayerPrefab, Vector3.zero, Quaternion.identity, player, InitializeObjBeforeSpawn);

            PlayerData data = GameManager.Instance.GetPlayerData(player, runner);
            data.Instance = playerObj;

            playerObj.GetComponent<Player>().Nickname = data.Nick;
        }
    }

    public void RemovePlayer(NetworkRunner runner, PlayerRef player)
    {
        PlayerData data = GameManager.Instance.GetPlayerData(player, runner);
        runner.Despawn(data.Instance);
    }

    private void InitializeObjBeforeSpawn(NetworkRunner runner, NetworkObject obj)
    {
        var behaviour = obj.GetComponent<Player>();
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
