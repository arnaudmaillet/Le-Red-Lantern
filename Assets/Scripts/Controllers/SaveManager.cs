using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string SAVED_GAME = "savedGame";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVED_GAME, json);
    }

    public static SaveData LoadGame()
    {
        string json = PlayerPrefs.GetString(SAVED_GAME);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public static bool IsGameSaved()
    {
        return PlayerPrefs.HasKey(SAVED_GAME);
    }

    public static void ClearSaveGame()
    {
        PlayerPrefs.DeleteKey(SAVED_GAME);
    }
}
