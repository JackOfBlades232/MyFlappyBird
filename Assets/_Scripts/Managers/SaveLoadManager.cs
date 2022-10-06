using UnityEngine;

public class SaveLoadManager : IInitializable
{
    private const string SaveFileName = "/save.bin";

    private static SaveLoadManager s_instance;

    public static SaveLoadManager Instance =>
        s_instance ??= new SaveLoadManager();

    private string DataPath => Application.persistentDataPath + SaveFileName;

    public PlayerData PlayerData { get; private set; }
    
    private SaveLoadManager() { }

    public void Initialize() => PlayerData = Saver.LoadPlayerData(DataPath);
    
    private void Save() => Saver.SavePlayerData(DataPath, PlayerData);

    public void OnGameEnd(int score)
    {
        PlayerData.UpdateScore(score);
        Save();
    }

    public void OnMuteButtonPressed(bool isMusic, bool isMuted)
    {
        PlayerData.UpdateMute(isMusic, isMuted);
        Save();
    }
}