using UnityEngine;

public class SaveManager
{
    public void Save<T>(string key, T data)
    {
        string jsonDataString = JsonUtility.ToJson(data, true);
        Debug.Log(jsonDataString);
        PlayerPrefs.SetString(key, jsonDataString);
    }
    public T Load<T>(string key) where T : new()
    {
        if(PlayerPrefs.HasKey(key))
        {
            string loadedString = PlayerPrefs.GetString(key);
            Debug.Log(loadedString);
            return JsonUtility.FromJson<T>(loadedString);
        }
        else
        {
            return new T();
        }
    }
}
