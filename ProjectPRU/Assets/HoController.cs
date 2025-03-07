using UnityEngine;
using System.Collections;

public class HoController : MonoBehaviour
{
    public float speed = 3f; // Tốc độ di chuyển
    public float targetY = -2f; // Điểm đích trên trục Y
    private Vector3 startPosition;
    public Animator animator;
    public AudioClip honoi;
    private AudioSource audioSource;
    public GameObject targetObject; // GameObject cần giảm order layer và đổi animation

    void Start()
    {
        startPosition = transform.position; // Lưu vị trí ban đầu
        animator = GetComponent<Animator>(); // Lấy Animator
        audioSource = gameObject.AddComponent<AudioSource>(); // Thêm AudioSource nếu chưa có

        if (animator == null)
        {
            Debug.LogError("Không tìm thấy Animator trên GameObject!");
        }
        if (targetObject == null)
        {
            Debug.LogError("targetObject chưa được gán trong Inspector!");
        }

        // Gọi Coroutine để thực hiện hành động
        StartCoroutine(DelayedAction());
    }

    IEnumerator DelayedAction()
    {
        yield return new WaitForSeconds(10f);

        // Kiểm tra targetObject có Animator không
        Animator targetAnimator = targetObject?.GetComponent<Animator>();
        if (targetAnimator == null)
        {
            Debug.LogError("targetObject không có Animator!");
            yield break; // Dừng coroutine nếu không có Animator
        }

        targetAnimator.Play("Bui");
        yield return new WaitForSeconds(11f); // Chờ 11 giây

        // Giảm y xuống 1 đơn vị (chỉ thực hiện 1 lần)
        transform.position += new Vector3(0, -1, 0);

        // Giảm sortingOrder của targetObject nếu có SpriteRenderer
        SpriteRenderer sr = targetObject?.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingOrder = 10; // Đặt order layer thành 10
        }
        else
        {
            Debug.LogError("targetObject không có SpriteRenderer!");
        }

        // Chuyển sang animation "BuiIdle"
        targetAnimator.Play("BuiIdle");

        yield return new WaitForSeconds(10f);

        // Phát âm thanh nếu AudioClip không null
        if (honoi != null && audioSource != null)
        {
            animator.Play("HoTalk");
            audioSource.PlayOneShot(honoi);

        }
        else
        {
            Debug.LogError("Không tìm thấy AudioClip hoặc AudioSource!");
        }
    }
}
