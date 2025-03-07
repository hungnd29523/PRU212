using UnityEngine;
using System.Collections;

public class CauBeController : MonoBehaviour
{
    public float speed = 3f; // Tốc độ di chuyển
    public float targetX = 7f; // Điểm đích trên trục X
    private Vector3 startPosition;
    public Animator animator;
    private bool canMove = false; // Biến kiểm soát khi nào được di chuyển

    public AudioClip danchuyengt;  // Âm thanh phát sau 1 giây
    public AudioClip cbnoi; // Âm thanh phát sau 10 giây
    private AudioSource audioSource;
    public AudioClip danchuyengtho;
    public AudioClip cbdap;
    void Start()
    {
        startPosition = transform.position; // Lưu vị trí ban đầu
        animator = GetComponent<Animator>(); // Lấy Animator
        audioSource = gameObject.AddComponent<AudioSource>();

        if (animator == null)
        {
            Debug.LogError("Không tìm thấy Animator trên GameObject!");
        }

        // Chạy coroutine để phát âm thanh và delay di chuyển
        StartCoroutine(GameSequence());
    }

    IEnumerator GameSequence()
    {

        audioSource.PlayOneShot(danchuyengt);
        yield return new WaitForSeconds(8f);

        // Phát âm thanh đầu tiên sau 1 giây

        canMove = true;
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(cbnoi);
        yield return new WaitForSeconds(4.2f);
        audioSource.PlayOneShot(danchuyengtho);
        yield return new WaitForSeconds(34f);
        audioSource.PlayOneShot(cbdap);
    }

    void Update()
    {
        if (canMove) // Chỉ di chuyển nếu đã hết thời gian chờ
        {
            if (transform.position.x < startPosition.x + targetX)
            {
                transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }
}
