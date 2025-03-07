using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVLevel1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject targetObject; 
    private Vector3 targetPosition;
    private bool isMoving = false;
    public AudioClip Danchuyen;
    public AudioClip Pv;
    public AudioClip beep;
    private AudioSource audioSource;
    public GameObject background; 
    public string nextSceneName = "Level2";
    public TextMeshProUGUI x;
    public TextMeshProUGUI mising;
    public SpriteRenderer spriteRenderer; 
    public Color newColor = Color.black;
    void Start()
    {


        audioSource = gameObject.AddComponent<AudioSource>();
        targetPosition = targetObject.transform.position + new Vector3(6f, 0, 0);

        StartCoroutine(StartMovingAfterDelay());
        StartCoroutine(GameSequence());
        background.SetActive(false);
        mising.gameObject.SetActive(false);
        x.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            targetObject.transform.position = Vector3.MoveTowards(
                targetObject.transform.position,
                targetPosition,
                5f * Time.deltaTime
            );

            if (Vector3.Distance(targetObject.transform.position, targetPosition) < 0.01f)
            {
                x.gameObject.SetActive(true);
                isMoving = false;
                spriteRenderer.color = newColor;
                mising.gameObject.SetActive(true);

            }
        }
    }
    IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(22f); 
        isMoving = true;
    }
    IEnumerator GameSequence()
    {
        yield return new WaitForSeconds(1f);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(Danchuyen);
        }

        

        yield return new WaitForSeconds(13f);
        if (audioSource != null)
        {
            audioSource.PlayOneShot(Pv);
        }

        yield return new WaitForSeconds(38f);
        if (background != null)
        {
            background.SetActive(true);
            audioSource.PlayOneShot(beep);
        }

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(nextSceneName);
    }
}
