using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PVLevel1 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject targetObject;
    public GameObject Pvnam;
    public GameObject PVnu;
    private Vector3 targetPosition;
    private bool isMoving = false;
    public AudioClip Danchuyen;
    public AudioClip Pv;
    public AudioClip beep;
    private AudioSource audioSource;
    public GameObject background; 
    public TextMeshProUGUI x;
    public TextMeshProUGUI mising;
    public SpriteRenderer spriteRenderer; 
    public Color newColor = Color.black;
    public Animator animator;
    public TextMeshProUGUI pv;
    public TextMeshProUGUI danchuyen;
    private string pvnoi = "PV đang nói";
    private string danchuyennoi = "Người dẫn chuyện đang nói";
    void Start()
    {
        animator = GetComponent<Animator>();
        Pvnam.gameObject.SetActive(true);
        PVnu.gameObject.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        targetPosition = targetObject.transform.position + new Vector3(6f, 0, 0);
       // danchuyen.gameObject.SetActive(true);
        pv.gameObject.SetActive(false);
        StartCoroutine(StartMovingAfterDelay());
        StartCoroutine(ShowSCLetterByLetter());
       // StartCoroutine(ShowPVLetterByLetter());
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
        yield return new WaitForSeconds(23f); 
        isMoving = true;
    }
    IEnumerator ShowPVLetterByLetter()
    {
        pv.text = ""; // Xóa nội dung cũ trước khi hiển thị
        foreach (char letter in pvnoi)
        {
            pv.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator ShowSCLetterByLetter()
    {
        danchuyen.text = ""; // Xóa nội dung cũ trước khi hiển thị
        foreach (char letter in danchuyennoi)
        {
            danchuyen.text += letter;
            yield return new WaitForSeconds(0.2f);
        }
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
      //  pv.gameObject.SetActive(true);
        StartCoroutine(ShowPVLetterByLetter());
       // danchuyen.gameObject.SetActive(false);
        Pvnam.gameObject.SetActive(false);
        PVnu.gameObject.SetActive(true);
        yield return new WaitForSeconds(39f);
      //  pv.gameObject.SetActive(false);
        if (background != null)
        {
            background.SetActive(true);
            audioSource.PlayOneShot(beep);
        }

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level2");
    }
}
