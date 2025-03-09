using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QTCS : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator animator;
    private AudioSource audioSource;
    public AudioClip danchuyen;
    public AudioClip intro;
    public GameObject CS;
    public GameObject CB;
    public GameObject Ho;
    public TextMeshProUGUI textDisplay; // Tham chiếu đến UI Text
    private string fullText = "Quà tặng cuộc sống"; // Chuỗi cần hiển thị
    private float delay = 0.15f; // Thời gian delay giữa mỗi chữ

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(DelayedAction());
        audioSource.PlayOneShot(intro);
        StartCoroutine(Tagrget());
        CS.SetActive(false);

        CB.SetActive(false);
        Ho.SetActive(false);
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(12f); // Chờ 10 giây trước khi hiển thị chữ
        StartCoroutine(ShowTextLetterByLetter());

        yield return new WaitForSeconds(6f); 
        audioSource.PlayOneShot(danchuyen);
    }

    IEnumerator ShowTextLetterByLetter()
    {
        textDisplay.text = ""; // Xóa nội dung cũ trước khi hiển thị
        foreach (char letter in fullText)
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(delay);
        }
    }
    IEnumerator Tagrget()
    {
        yield return new WaitForSeconds(4f);
        CB.SetActive(true); 
        yield return new WaitForSeconds(3f);
        CS.SetActive(true);
        yield return new WaitForSeconds(3f);
       
        Ho.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
