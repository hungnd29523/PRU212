using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class CharacterNameManager : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TextMeshProUGUI welcomeText;
    public Button enterButton;
    public Button playButton;

    public Button musicPlayButton;
    public Button musicPauseButton;
    public Button musicStopButton;

    public Button viewHistoryButton;              
    public TextMeshProUGUI historyText;           

    public AudioSource audioSource;

    private void Start()
    {
        welcomeText.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false); 

        enterButton.onClick.AddListener(DisplayWelcomeMessage);
        playButton.onClick.AddListener(LoadGameScene);

        musicPlayButton.onClick.AddListener(PlayMusic);
        musicPauseButton.onClick.AddListener(PauseMusic);
        musicStopButton.onClick.AddListener(StopMusic);

        viewHistoryButton.onClick.AddListener(DisplayVictoryHistory);  
    }

    private void DisplayWelcomeMessage()
    {
        string playerName = nameInputField.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            welcomeText.text = "Welcome " + playerName + "!";
            welcomeText.gameObject.SetActive(true);
            playButton.gameObject.SetActive(true);

            PlayerPrefs.SetString("PlayerName", playerName);
        }
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void PlayMusic()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    private void PauseMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.UnPause();
        }
    }

    private void StopMusic()
    {
        audioSource.Stop();
    }

    private void DisplayVictoryHistory()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "VictoryLog.txt");

        if (File.Exists(filePath))
        {
            string history = File.ReadAllText(filePath);
            historyText.text = "Victory History:\n" + history;
        }
        else
        {
            historyText.text = "No victory history found.";
        }

        historyText.gameObject.SetActive(true);  
    }
}
