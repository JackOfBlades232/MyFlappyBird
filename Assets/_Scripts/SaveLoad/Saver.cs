using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class Saver
{
    public static void SavePlayerData(string savePath, PlayerData playerData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(savePath, FileMode.Create);
        
        formatter.Serialize(fs, playerData);
        fs.Close();
    }

    public static PlayerData LoadPlayerData(string savePath)
    {
        PlayerData data;
        
        if (File.Exists(savePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fs = new FileStream(savePath, FileMode.Open);

            data = (PlayerData) formatter.Deserialize(fs);
            
            fs.Close();
        }
        else 
            data = new PlayerData();

        return data;
    }
}