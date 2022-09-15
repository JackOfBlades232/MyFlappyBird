using System;

[Serializable]
public class PlayerData
{
    public int LastScore { get; private set; }
    public int HighScore { get; private set; }

    public bool MusicIsMuted { get; private set; }
    public bool SoundIsMuted { get; private set; }

    public PlayerData()
    {
        HighScore = 0;
        MusicIsMuted = false;
        SoundIsMuted = false;
    }

    public void UpdateScore(int score)
    {
        LastScore = score;
        
        if (score > HighScore)
            HighScore = score;
    }

    public void UpdateMute(bool isMusic, bool isMuted)
    {
        if (isMusic)
            MusicIsMuted = isMuted;
        else
            SoundIsMuted = isMuted;
    }
}