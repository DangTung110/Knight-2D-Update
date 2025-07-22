using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 target;
    private Animator animator;
    private float delay = 2f;
    private float attackNumber = 2f;
    private float hp, speed, dame;
    private bool isAttack = false, isDie = false;
    private float timeDieDelay;
    public EnemyInfo enemyInfo;
    public float attackRange = 0.5f;
    public bool isTakeDame = false;
    public LayerMask playerLayer;
    [SerializeField] private PlayerController playerController;
    [SerializeField] Transform PointA, PointB;
    [SerializeField] private Transform attackPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        dame = enemyInfo.dame;
        speed = enemyInfo.speed;
        GetComponent<HealthController>().hp = hp = enemyInfo.hp;
        target = PointB.position;
        transform.position = PointA.position;
        animator.SetBool(AnimEnemy.Run, true);
    }
    private void Update()
    {
        if (hp <= 0 && !isDie)
        {
            timeDieDelay += Time.deltaTime;
            speed = 0;
            if (timeDieDelay > 0.3f)
            {
                animator.SetTrigger(AnimEnemy.Die);
                isDie = true;
                timeDieDelay = 0;
            }
        }
        else if (isDie)
        {
            timeDieDelay += Time.deltaTime;
            if (timeDieDelay > 0.5f)
                gameObject.SetActive(false);
        }
        else
            CheckAttack();
    }
    private void FixedUpdate()
    {
        if (isTakeDame || isAttack || isDie)
            return;
        Movement();
    }
    private void Movement()
    {
        if (Vector2.Distance(transform.position, target) >= 0.5f)
        {
            rb.velocity = new Vector2(speed, 0f);
            animator.SetBool(AnimEnemy.Run, true);
            animator.SetBool(AnimEnemy.Idle, false);
        }
        else if (Vector2.Distance(transform.position, target) < 0.5f)
        {
            delay -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            animator.SetBool(AnimEnemy.Run, false);
            animator.SetBool(AnimEnemy.Idle, true);
            if (delay < 0f)
            {
                ChangeTarget();
                Flip();
                delay = 2f;
            }
        }
    }
    private void ChangeTarget()
    {
        if (target == PointA.position)
            target = PointB.position;
        else
            target = PointA.position;
    }
    private void Flip()
    {
        Vector3 localScele = transform.localScale;
        localScele.x *= -1;
        transform.localScale = localScele;
        speed *= -1;
    }
    private void CheckAttack()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        if (cols.Length > 0)
        {
            delay -= Time.deltaTime;
            rb.velocity = Vector2.zero;
            animator.SetBool(AnimEnemy.Run, false);
            animator.SetBool(AnimEnemy.Idle, true);
            isAttack = true;
            if (delay < 0f)
            {
                attackNumber = Random.Range(1, 3);
                animator.SetTrigger(AnimEnemy.Attack + attackNumber.ToString());
                animator.SetBool(AnimEnemy.Idle, false);
                delay = 1f;
            }
        }
        else
        {
            isAttack = false;
        }
    }
    private void Attack()
    {
        playerController.TakeDame(dame);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
    public void TakeDame(float dame)
    {
        hp -= dame;
        GetComponent<HealthController>().TakeDame(hp, dame);
    }
    public void EnemyDie()
    {
        gameObject.SetActive(false);
    }
    private void ResetStatus()
    {
        isTakeDame = false;
    }
}
