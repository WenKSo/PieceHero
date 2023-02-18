using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class TestInputController : NetworkBehaviour, INetworkRunnerCallbacks
{
    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Runner.AddCallbacks(this);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
        var playerInput = new PlayerInput();
        playerInput.Buttons.Set(PlayerButtons.Roll, InputVariables.instance.onClicked);
        input.Set(playerInput);
    }

    public void OnDisable()
    {
        if(Runner != null)
        {
            Runner.RemoveCallbacks( this );
        }
    }

    #region UnusedCallbacks
    public void OnConnectedToServer(NetworkRunner runner) {}
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) {}
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) {}
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) {}
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) {}
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {}
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {}
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) {}
    public void OnSceneLoadDone(NetworkRunner runner) {}
    public void OnSceneLoadStart(NetworkRunner runner) {}
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {}
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {}
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message){}

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }
    #endregion
}
