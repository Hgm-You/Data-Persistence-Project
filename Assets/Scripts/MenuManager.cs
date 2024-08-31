using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField NameInputField; // Champ pour entrer le nom du joueur
    public Text BestScoreText; // Affiche le meilleur score

    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/playerdata.json";
        LoadBestScore();
    }

    public void StartGame()
    {
        string playerName = NameInputField.text;
        PlayerPrefs.SetString("PlayerName", playerName);
        SceneManager.LoadScene("Main"); // Assure-toi que "GameScene" est le nom de ta scène de jeu
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    private void LoadBestScore()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            BestScoreText.text = $"Best Score: {playerData.highScore} by {playerData.playerName}";
        }
        else
        {
            BestScoreText.text = "Best Score: None";
        }
    }
}
