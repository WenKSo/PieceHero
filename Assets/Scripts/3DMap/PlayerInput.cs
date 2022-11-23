using Fusion;

enum PlayerButtons {
    Roll = 0
}

public struct PlayerInput : INetworkInput {
    public NetworkButtons Buttons;
    public int rollNum;
}