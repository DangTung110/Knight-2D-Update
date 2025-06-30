using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hp;
    public float speed = 5f;
    public float force = 11.5f;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded;
    private Animator animator;
    private float h;
    public bool isFacingRight = true;
    [SerializeField] private Transform attackPoint;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        GetComponent<HealthController>().hp = hp;
    }
    private void Update()
    {
        if (hp <= 0)
            return;
        Jump();
    }
    void FixedUpdate()
    {
        if (hp <= 0)
            return;
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
        hp -= dame;
        GetComponent<HealthController>().TakeDame(hp, dame);
        Debug.Log("Hp player: " + hp);
    }
}
