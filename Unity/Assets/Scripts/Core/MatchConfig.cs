using System;

[Serializable]
public class MatchConfig
{
    public int MaxPlayers = 4;
    public float MatchDurationSeconds = 300f;
    public string MapName = "DefaultMap";
    public bool EnableAI = true;
}
