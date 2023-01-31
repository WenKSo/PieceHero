using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class TestBehaviour : NetworkBehaviour
{
    public override void Spawned()
    {
        FindObjectOfType<PlayerSpawner3D>().RespawnPlayers(Runner);
        StartLevel();
    }

    public void StartLevel()
    {
        LoadingManager.Instance.FinishLoadingScreen();  
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
}
