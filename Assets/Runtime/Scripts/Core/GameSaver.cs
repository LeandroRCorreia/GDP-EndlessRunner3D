using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class ScoreData
{
    public int bestScore = 0;
    public int lastScore = 0;
    public int totalCherriesCollected = 0;
    public int totalPeanutCollected = 0;

}

public class AudioPreferencesData
{
    public float mainVolumeDb = 0;
    public float musicVolumeDb = 0;
    public float sfxVolumeDb = 0;

}

public class GameSaver : MonoBehaviour
{
    
    public ScoreData CurrentScoreData {get; private set;}
    public AudioPreferencesData CurrentAudioPreferencesData {get; private set;}

    private bool IsGameLoaded => CurrentScoreData != null || CurrentAudioPreferencesData != null;

    private string ScoreDataPath => Path.Combine(Application.persistentDataPath, "ScoreData.Json");
    private string AudioPreferencesDataPath => Path.Combine(Application.persistentDataPath, "AudioPreferences.json");

    public void LoadGame()
    {
        if(!IsGameLoaded)
        {
            CurrentScoreData = LoadScoreData() ?? new ScoreData();
            CurrentAudioPreferencesData = LoadAudioPreferences() ?? new AudioPreferencesData();
        }

    }

    private AudioPreferencesData LoadAudioPreferences()
    {
        using (FileStream fileStream = new(AudioPreferencesDataPath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader streamReader = new(fileStream))
        using (JsonTextReader jsonTextReader = new(streamReader))
        {
            var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<AudioPreferencesData>(jsonTextReader);

        }
    }

    private ScoreData LoadScoreData()
    {
        using (FileStream stream = new(ScoreDataPath, FileMode.OpenOrCreate, FileAccess.Read))
        using (StreamReader streamReader = new(stream))
        using (JsonTextReader jsonTextReader = new(streamReader))
        {
            var jsonSerializer = new JsonSerializer();
            return jsonSerializer.Deserialize<ScoreData>(jsonTextReader);
        }

    }

    public void SaveScoreData(ScoreData newScoreData)
    {
        using(FileStream fileStream = new(ScoreDataPath, FileMode.OpenOrCreate, FileAccess.Write))
        using(StreamWriter streamWriter = new(fileStream))
        using(JsonTextWriter jsonTextWriter = new(streamWriter))
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(jsonTextWriter, newScoreData);

        }
        CurrentScoreData = newScoreData;

    }

    public void SaveAudioPreferences(AudioPreferencesData newAudioPreferences)
    {
        using(FileStream fileStream = new(AudioPreferencesDataPath, FileMode.OpenOrCreate, FileAccess.Write))
        using(StreamWriter streamWriter = new(fileStream))
        using(JsonTextWriter jsonTextWriter = new(streamWriter))
        {
            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(jsonTextWriter, newAudioPreferences);
        }
        CurrentAudioPreferencesData = newAudioPreferences;
    }

    public void DeleteAllData()
    {
        File.Delete(ScoreDataPath);
        File.Delete(AudioPreferencesDataPath);

        CurrentScoreData = null;
        CurrentAudioPreferencesData = null;
        LoadGame();

    }

}
