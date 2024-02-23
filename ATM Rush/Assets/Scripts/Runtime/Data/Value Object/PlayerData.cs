using System;

[Serializable]
public struct PlayerData
{
    public PlayerMovementData MovementData;
}

public struct PlayerMovementData
{
    public float ForwardSpeed;
    public float HorizontalSpeed;
}
