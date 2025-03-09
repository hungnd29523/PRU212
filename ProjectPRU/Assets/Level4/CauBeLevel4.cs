using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CauBeLevel4 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  
    public Animator animator;
    private AudioSource audioSource;
    public AudioClip caube;
   
    private bool canMove = false;
    void Start()
    {
        animator = GetComponent<Animator>();

        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(DelayedAction());
    }
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(19.5f);
        audioSource.PlayOneShot(caube);
        animator.SetBool("CBTalk", true);
        yield return new WaitForSeconds(13f);
        SceneManager.LoadScene("Level5");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
