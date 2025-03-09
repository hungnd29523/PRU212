using System.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CanhSat : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 startPosition;
    public Animator animator;
    private AudioSource audioSource;
    public AudioClip danchuyen;
    public AudioClip canhsat;
    private bool canMove = false;
    void Start()
    {
        startPosition = transform.position; // Lưu vị trí ban đầu
        animator = GetComponent<Animator>();
       
        audioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(DelayedAction());
         audioSource.PlayOneShot(danchuyen);
    }
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(8f);
        canMove = true;
        yield return new WaitForSeconds(4.5f);
        audioSource.PlayOneShot(canhsat);
        animator.SetBool("isTalk", true);
        yield return new WaitForSeconds(7f);
        animator.SetBool("isTalk", false);
    }
    // Update is called once per frame
    void Update()
    {
        if (canMove) // Chỉ di chuyển nếu đã hết thời gian chờ
        {
            if (transform.position.x < startPosition.x + 8.31f)
            {
                transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                // Kiểm tra Animator trước khi gọi SetBool
                if (animator != null)
                {
                    animator.SetBool("isWalk", true);

                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("battay", true);
                }
            }
        }

    }
}
