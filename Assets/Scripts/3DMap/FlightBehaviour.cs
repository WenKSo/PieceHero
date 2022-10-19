using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class FlightBehaviour : NetworkBehaviour
{
    public override void Spawned()
    {
        FindObjectOfType<PlayerSpawner>().RespawnPlayers(Runner);
        StartLevel();
    }

    public void StartLevel()
    {
        LoadingManager.Instance.FinishLoadingScreen();  
        GameManager.Instance.SetGameState(GameManager.GameState.Playing);
    }
}
