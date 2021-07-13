using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject canvas;
    Text playerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerName = canvas.transform.Find("PlayerNameInputField/text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [System.Serializable]
    class SaveData
    {
        public Text PlayerName;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.PlayerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.PlayerName;
        }
    }

    //ok
    public void ReStart()
    {
        SceneManager.LoadScene("main");
    }

    public void OnApplicationQuit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
