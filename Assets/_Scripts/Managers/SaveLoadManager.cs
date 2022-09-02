using UnityEngine;

public class SaveLoadManager : IInitializable
{
    private const string SaveFileName = "/save.bin";

    private string DataPath => Application.persistentDataPath + SaveFileName;
    
    public PlayerData PlayerData { get; private set; }

    public void Initialize() => PlayerData = Saver.LoadPlayerData(DataPath);

    public void OnGameEnd(int score)
    {
        PlayerData.UpdateHighScore(score);

        Saver.SavePlayerData(DataPath, PlayerData);
    }
}