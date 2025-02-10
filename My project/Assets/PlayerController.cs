using System.IO;
using UnityEngine;
using TMPro;
using static UnityEditor.Experimental.GraphView.GraphView;
using Unity.VisualScripting;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int speed = 8;
    private Rigidbody2D rb;
    public float force = 10;
    private bool isGrounded = true;
    public int coin = 0;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gameOverText; 

    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip bombSound;
    private AudioSource audioSource;
    public TextMeshProUGUI playerNameText;
    public AudioClip cupSound;
    public TextMeshProUGUI resultText; 


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coinText.text = "Coin: 0";
        audioSource = GetComponent<AudioSource>(); 
        gameOverText.gameObject.SetActive(false); 
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        playerNameText.text = playerName;
        resultText.gameObject.SetActive(false);

    }

    void Update()
    {
        float X = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(X, Y, 0) * speed * Time.deltaTime;
        transform.Translate(move);

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, force);
            PlaySound(jumpSound); 
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coin++;
            coinText.text = "Coin: " + coin;
            PlaySound(coinSound); 
        }

        if (other.gameObject.CompareTag("Bom"))
        {
            PlayBombSound(); 
            Destroy(other.gameObject);
            ResetCoins(); 
            DestroyAllCoins(); 
            GameOver(); 
        }
        if (other.gameObject.CompareTag("Cup") && coin >= 5 )
        {
            PlayCupSound();
            ShowVictoryScreen();
            Destroy(other.gameObject);
            DestroyAllCoins();
        }

    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip); 
        }
    }

    private void PlayBombSound()
    {
        if (bombSound != null)
        {
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
            tempAudioSource.clip = bombSound;
            tempAudioSource.Play();
            Destroy(tempAudio, bombSound.length);
        }
    }

    private void PlayCupSound()
    {
        if (cupSound != null)
        {
            GameObject tempAudio = new GameObject("TempAudio");
            AudioSource tempAudioSource = tempAudio.AddComponent<AudioSource>();
            tempAudioSource.clip = cupSound;
            tempAudioSource.Play();
            Destroy(tempAudio, cupSound.length);
        }
    }
    private void ResetCoins()
    {
        coin = 0;
        coinText.text = "Coin: " + coin;
    }

    private void DestroyAllCoins()
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin");
        foreach (GameObject coin in coins)
        {
            Destroy(coin);
        }
        GameObject[] boms = GameObject.FindGameObjectsWithTag("Bom");
        foreach (GameObject bom in boms)
        {
            Destroy(bom);
        }
        GameObject[] cups = GameObject.FindGameObjectsWithTag("Cup");
        foreach (GameObject cup in cups)
        {
            Destroy(cup);
        }
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        Destroy(this.gameObject); 
    }
    private void ShowVictoryScreen()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "Player");
        string victoryMessage = $"Victory! \nPlayer: {playerName}\nCoin: {coin}";

        resultText.text = victoryMessage;
        resultText.gameObject.SetActive(true);

        SaveVictoryToFile(playerName, coin);  
        Destroy(this.gameObject);
    }

    private void SaveVictoryToFile(string playerName, int coin)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "VictoryLog.txt");
        string logEntry = $"{System.DateTime.Now}: \nPlayer: {playerName}, \nCoin: {coin}\n";

        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true)) 
            {
                writer.WriteLine(logEntry);
            }
            Debug.Log($"Victory appended to {filePath}");
        }
        catch (IOException ex)
        {
            Debug.LogError($"Failed to write to file: {ex.Message}");
        }
    }


}
