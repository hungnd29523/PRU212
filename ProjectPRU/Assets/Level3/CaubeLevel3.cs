using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaubeLevel3 : MonoBehaviour
{
    private bool canMoveBackward = false;
    private Vector3 startPosition;
    public Animator animator;
    public AudioClip danchuyen;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        startPosition = transform.position; // Lưu vị trí ban đầu
        animator = GetComponent<Animator>();
        StartCoroutine(DelayedAction());
    }
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(1f);
        canMoveBackward = true;
        yield return new WaitForSeconds(8f);
        animator.SetBool("Cuoi", true);
        animator.SetBool("DuoiHo", true);
        audioSource.PlayOneShot(danchuyen);
        yield return new WaitForSeconds(11f);
        SceneManager.LoadScene("Level4");
    }
        // Update is called once per frame
        void Update()
    {
        if (canMoveBackward) // Chỉ di chuyển nếu được phép
        {
            if (transform.position.x > startPosition.x - 11f) // Giảm vị trí X
            {
                transform.position -= new Vector3(3 * Time.deltaTime, 0, 0);

                if (animator != null)
                {
                    animator.SetBool("isBack", true); // Chuyển sang trạng thái đi lùi
                                                      // animator.Play("CaubeQuaylai"); // Phát animation quay lại
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("isBack", false);
                    animator.Play("CBIdle"); // Trở về Idle sau khi di chuyển xong
                    
                }
                canMoveBackward = false; // Ngăn di chuyển tiếp tục
            }
        }
    }
}
