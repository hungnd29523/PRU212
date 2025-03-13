using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using TMPro;

public class HoController : MonoBehaviour
{
    public float speed = 3f; // Tốc độ di chuyển
    public float targetY = -2f; // Điểm đích trên trục Y
    private Vector3 startPosition;
    public Animator animator;
    public AudioClip honoi;
    public AudioClip hohoi;
    private AudioSource audioSource;
    public GameObject targetObject; // GameObject cần giảm order layer và đổi animation
    public AudioClip hobilua;
    private bool canMoveBackward = false;
    public TextMeshProUGUI nup;
    public TextMeshProUGUI nhay;
    void Start()
    {
        startPosition = transform.position; // Lưu vị trí ban đầu
        animator = GetComponent<Animator>(); // Lấy Animator
        audioSource = gameObject.AddComponent<AudioSource>(); // Thêm AudioSource nếu chưa có

        // Kiểm tra lỗi trước khi tiếp tục
        if (animator == null)
        {
            Debug.LogError(" Không tìm thấy Animator trên GameObject!");
        }
        if (targetObject == null)
        {
            Debug.LogError(" targetObject chưa được gán trong Inspector!");
        }
        if (honoi == null)
        {
            Debug.LogError(" AudioClip 'honoi' chưa được gán trong Inspector!");
        }
        nhay.gameObject.SetActive(false);
        nup.gameObject.SetActive(false);
        // Gọi Coroutine để thực hiện hành động
        StartCoroutine(DelayedAction());
    }
    void Update()
    {
       
        if (canMoveBackward) // Chỉ di chuyển nếu được phép
        {
            if (transform.position.x > startPosition.x - 15f) // Giảm vị trí X
            {
                transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

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
                     animator.Play("Idle"); // Trở về Idle sau khi di chuyển xong
                }
                canMoveBackward = false; // Ngăn di chuyển tiếp tục
            }
        }


    }
    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(13f);
        nup.gameObject.SetActive(true);
        // Kiểm tra targetObject có Animator không
        if (targetObject != null)
        {
            Animator targetAnimator = targetObject.GetComponent<Animator>();
            if (targetAnimator != null)
            {
                targetAnimator.Play("Bui");
            }
            else
            {
                Debug.LogError(" targetObject không có Animator!");
            }
        }

        yield return new WaitForSeconds(10f);

        // Giảm y xuống 1 đơn vị
        transform.position += new Vector3(0, -1, 0);
        nhay.gameObject.SetActive(true);
        nup.gameObject.SetActive(false);
        // Giảm sortingOrder của targetObject nếu có SpriteRenderer
        if (targetObject != null)
        {
            SpriteRenderer sr = targetObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sortingOrder = 10; // Đặt order layer thành 10
            }
            else
            {
                Debug.LogError("error");
            }

            // Chuyển sang animation "BuiIdle" nếu có Animator
            Animator targetAnimator = targetObject.GetComponent<Animator>();
            if (targetAnimator != null)
            {
                targetAnimator.Play("BuiIdle");
            }
        }

        yield return new WaitForSeconds(8f);
        nhay.gameObject.SetActive(false);
        // Phát âm thanh nếu AudioClip và audioSource hợp lệ
        if (honoi != null && audioSource != null)
        {
            if (animator != null)
            {
                animator.Play("HoTalk");
            }
            audioSource.PlayOneShot(honoi);
        }
        else
        {
            Debug.LogError(" Không thể phát âm thanh! Kiểm tra AudioClip 'honoi' và AudioSource.");
        }
        yield return new WaitForSeconds(14f);
        animator.Play("HoIdle");
        yield return new WaitForSeconds(6f);
        animator.Play("HoTalk");
        audioSource.PlayOneShot(hohoi);
        yield return new WaitForSeconds(6f);
        animator.Play("HoIdle");
        yield return new WaitForSeconds(18f);
        animator.Play("HoTalk");
        audioSource.PlayOneShot(hobilua);
         yield return new WaitForSeconds(2f);
        animator.Play("HoIdle");
        canMoveBackward = true; // Bắt đầu di chuyển lùi
    }
}
