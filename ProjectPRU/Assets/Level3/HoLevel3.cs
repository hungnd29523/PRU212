using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class HoLevel3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private bool canMoveBackward = false;
    private bool canFalling = false;
    private Vector3 startPosition;
    private Vector3 endPosition;
    public Animator animator;
    public AudioClip hokeu;
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
        yield return new WaitForSeconds(6.8f);
        // animator.SetBool("isRoi", true);
        endPosition = transform.position;
        canFalling = true;
        audioSource.PlayOneShot(hokeu);
    }
    // Update is called once per frame
    void Update()
    {
        if (canMoveBackward) // Chỉ di chuyển nếu được phép
        {
            if (transform.position.x > startPosition.x - 20f) // Giảm vị trí X
            {
                transform.position -= new Vector3(3 * Time.deltaTime, 0, 0);

                if (animator != null)
                {
                    animator.SetBool("isWalk", true); // Chuyển sang trạng thái đi lùi
                                                      // animator.Play("CaubeQuaylai"); // Phát animation quay lại
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("isWalk", false);
                    animator.Play("HoIdle"); // Trở về Idle sau khi di chuyển xong

                }
                canMoveBackward = false; // Ngăn di chuyển tiếp tục
                animator.SetBool("isRoi", true);
            }
        }
        if (canFalling) // Chỉ di chuyển nếu được phép
        {
            if (transform.position.y > endPosition.y - 2.5f) // Giảm vị trí X
            {
                transform.position -= new Vector3(0, 10 * Time.deltaTime, 0);

                if (animator != null)
                {
                    animator.SetBool("isRoi", true);
                 

                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("isRoi", false);

                    // Trở về Idle sau khi di chuyển xong

                }
                canFalling = false; // Ngăn di chuyển tiếp tục
                animator.SetBool("DuoiHo", true);
            }
        }
    }
}
