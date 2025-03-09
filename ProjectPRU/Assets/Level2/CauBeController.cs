using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class CauBeController : MonoBehaviour
{
    public float speed = 1f; // Tốc độ di chuyển
    public float targetX = 8f; // Điểm đích trên trục X
    
    private Vector3 startPosition;
    private Vector3 nowPosition;
    public Animator animator;
    private bool canMove = false; // Biến kiểm soát khi nào được di chuyển
    private bool canMoveBackward = false;
    public AudioClip danchuyennoikehoach;
    public AudioClip danchuyengt;  // Âm thanh phát sau 1 giây
    public AudioClip cbnoi; // Âm thanh phát sau 10 giây
    public AudioClip danchuyengtho;
    public AudioClip cbdap;
    private AudioSource audioSource;
    public TextMeshProUGUI batngo;
    public TextMeshProUGUI tomo;
    public AudioClip noicaiho;
    void Start()
    {
        batngo.gameObject.SetActive(false);
        tomo.gameObject.SetActive(false);
        startPosition = transform.position; // Lưu vị trí ban đầu
        animator = GetComponent<Animator>();

        audioSource = gameObject.AddComponent<AudioSource>(); // Đảm bảo AudioSource luôn tồn tại

        // Kiểm tra Animator
        if (animator == null)
        {
            Debug.LogError(" Không tìm thấy Animator trên GameObject!");
        }

        // Kiểm tra từng AudioClip
        if (danchuyengt == null) Debug.LogError(" AudioClip 'danchuyengt' chưa được gán trong Inspector!");
        if (cbnoi == null) Debug.LogError(" AudioClip 'cbnoi' chưa được gán trong Inspector!");
        if (danchuyengtho == null) Debug.LogError(" AudioClip 'danchuyengtho' chưa được gán trong Inspector!");
        if (cbdap == null) Debug.LogError(" AudioClip 'cbdap' chưa được gán trong Inspector!");

        // Chạy coroutine để phát âm thanh và delay di chuyển
        StartCoroutine(GameSequence());
        StartCoroutine(TextSequece());
    }
        IEnumerator TextSequece()
        { 
        yield return new WaitForSeconds(15f);
        canMove = false;
        nowPosition = transform.position;
        tomo.gameObject.SetActive(true);
        Debug.Log("canMoveBackward: " + nowPosition);

        yield return new WaitForSeconds(5f);
        tomo.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        batngo.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(2f);
        batngo.gameObject.SetActive(false);


    }
        IEnumerator GameSequence()
    {
        // Phát âm thanh nếu không null
        if (danchuyengt != null && audioSource != null)
        {
            audioSource.PlayOneShot(danchuyengt);
        }
        yield return new WaitForSeconds(7f);

        canMove = true;
        yield return new WaitForSeconds(0.1f);

        if (cbnoi != null && audioSource != null)
        {
            audioSource.PlayOneShot(cbnoi);
        }
        yield return new WaitForSeconds(0.9f);
        yield return new WaitForSeconds(4.2f);

        if (danchuyengtho != null && audioSource != null)
        {
            audioSource.PlayOneShot(danchuyengtho);
        }
        yield return new WaitForSeconds(32f);


        if (cbdap != null && audioSource != null)
        {
            if (animator != null)
            {
                animator.Play("Talk");
            }
            audioSource.PlayOneShot(cbdap);


        }
        yield return new WaitForSeconds(7f); 

        if (animator != null)
        {
            animator.Play("Idle"); // Chuyển về trạng thái Idle
        }
        yield return new WaitForSeconds(7f);

        audioSource.PlayOneShot(danchuyennoikehoach);
        yield return new WaitForSeconds(10f);

        audioSource.PlayOneShot(noicaiho);
        if (animator != null)
        {
            animator.Play("Caubechitay"); 
        }
        yield return new WaitForSeconds(7f);
        if (animator != null)
        {
            animator.Play("Idle"); // Chuyển về trạng thái Idle
        }
        yield return new WaitForSeconds(2f);
        canMoveBackward = true; // Bắt đầu di chuyển lùi
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level3");

    }
    

    void Update()
    {
        if (canMove) // Chỉ di chuyển nếu đã hết thời gian chờ
        {
            if (transform.position.x < startPosition.x + targetX)
            {
                transform.position += new Vector3(2 * Time.deltaTime, 0, 0);

                // Kiểm tra Animator trước khi gọi SetBool
                if (animator != null)
                {
                    animator.SetBool("isWalking", true);
                    
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
        
        if (canMoveBackward) // Chỉ di chuyển nếu được phép
        {
            if (transform.position.x > nowPosition.x - 10f) // Giảm vị trí X
            {
                transform.position -= new Vector3(3 * Time.deltaTime, 0, 0);

                if (animator != null)
                {
                    animator.SetBool("isBack", true); // Chuyển sang trạng thái đi lùi
                }
            }
            else
            {
                if (animator != null)
                {
                    animator.SetBool("isBack", false);
                    animator.Play("Idle"); // Trở về Idle sau khi di chuyển xong
                }
                canMoveBackward = false; // Ngăn di chuyển tiếp tục
            }
        }


    }
}
