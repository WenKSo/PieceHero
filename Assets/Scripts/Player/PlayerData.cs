using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using FusionUtilsEvents;

public class PlayerData: NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnNickUpdate))]
    public NetworkString<_16> Nick { get; set; }
    [Networked(OnChanged = nameof(OnMapUpdate))]
    public NetworkString<_512> Map { get; set; }
    [Networked]
    public NetworkObject Instance { get; set; }

    public FusionEvent OnPlayerDataSpawnedEvent;

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SetNick(string nick)
    {
        Nick = nick;
    }

    [Rpc(sources: RpcSources.InputAuthority, targets: RpcTargets.StateAuthority)]
    public void RPC_SetMap(string map)
    {
        Map = map;
    }

    public override void Spawned()
    {
        if (Object.HasInputAuthority){
            RPC_SetNick(PlayerPrefs.GetString("Nick"));
        }
        if (Object.HasStateAuthority)RPC_SetMap(PlayerPrefs.GetString("Map"));
        DontDestroyOnLoad(this);
        Runner.SetPlayerObject(Object.InputAuthority, Object);
        OnPlayerDataSpawnedEvent?.Raise(Object.InputAuthority, Runner);
    }

    public static void OnNickUpdate(Changed<PlayerData> changed)
    {
        changed.Behaviour.OnPlayerDataSpawnedEvent?.Raise(changed.Behaviour.Object.InputAuthority, changed.Behaviour.Runner);
    }

    public static void OnMapUpdate(Changed<PlayerData> changed)
    {
        changed.Behaviour.OnPlayerDataSpawnedEvent?.Raise(changed.Behaviour.Object.InputAuthority, changed.Behaviour.Runner);
    }
}
