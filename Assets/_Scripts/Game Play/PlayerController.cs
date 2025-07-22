using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isGrounded;
    private Animator animator;
    private float h;
    private Rigidbody2D rb;
    private float hpCurrent;
    public bool isDie = false;
    public bool isFacingRight = true;
    public float hp;
    public float speed = 5f;
    public float force = 11.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject loseObject;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        hpCurrent = hp;
        ResetPlayer();
    }
    private void Update()
    {
        if (isDie)
        {
            loseObject.SetActive(true);
            GameManager.instance.OnEndMenu();
            return;
        }
        Jump();
        Isfalling();
    }
    void FixedUpdate()
    {
        if (hpCurrent <= 0)
        {
            isDie = true;
            return;
        }
        Movement();
        UpdateAnimator();
    }
    private void Movement()
    {
        h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2 (h * speed, rb.velocity.y);
        if (h > 0 && !isFacingRight)
            Flip();
        else if (h < 0 && isFacingRight)
            Flip();
    }
    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.3f, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(0, force);
        }
    }
    private void UpdateAnimator()
    {
        if (isGrounded) 
            animator.SetFloat(AnimPlayer.Run, Mathf.Abs(h));
        animator.SetBool(AnimPlayer.Jump, !isGrounded);
    }
    private void Flip()
    {
        Vector3 localScele = transform.localScale;
        localScele.x *= -1;
        transform.localScale = localScele;
        isFacingRight = !isFacingRight;
    }
    public void TakeDame(float dame)
    {
        hpCurrent -= dame;
        GetComponent<HealthController>().TakeDame(hpCurrent, dame);
        animator.SetTrigger(AnimPlayer.Hurt);
    }
    private void Isfalling()
    {
        if (transform.position.y < -10)
        {
            isDie = true;
            hpCurrent = 0;
            GetComponent<HealthController>().hp = 0;
        }
    }
    public void ResetPlayer()
    {
        GetComponent<HealthController>().hp = hp;
        hpCurrent = hp;
        isDie = false;
    }
}