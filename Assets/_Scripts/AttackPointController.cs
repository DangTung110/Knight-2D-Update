using UnityEngine;

public class AttackPointController : MonoBehaviour
{
    public float speed = 7f;
    public float horizontal;
    public float dameAttackPoint;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    private void Movement()
    {
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);
    }
    public void SetStatus(int key)
    {
        if (key == 0)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(Tag.Enemy))
        {
            collider.gameObject.GetComponent<EnemyController>().TakeDame(dameAttackPoint);
            gameObject.SetActive(false);
            collider.gameObject.GetComponent<EnemyController>().isTakeDame = true;
            Debug.Log("Dame Point: " + dameAttackPoint);
        }
    }
    public void SetScaleAttackPoint(float h, bool isFacingRight)
    {
        gameObject.SetActive(true);
        if (!isFacingRight && gameObject.transform.localScale.x > 0)
        {
            Flip();
        }
        else if (isFacingRight && gameObject.transform.localScale.x < 0)
        {
            Flip();
        }
        horizontal = h;
    }
    private void Flip()
    {
        Vector3 scaleAttackPoint = gameObject.transform.localScale;
        scaleAttackPoint.x *= -1;
        gameObject.transform.localScale = scaleAttackPoint;
    }
}
