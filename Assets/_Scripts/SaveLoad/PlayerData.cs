using System;

[Serializable]
public class PlayerData
{
    public int HighScore { get; private set; }

    public PlayerData() => HighScore = 0;

    public void UpdateHighScore(int score)
    {
        if (score > HighScore)
            HighScore = score;
    }
}