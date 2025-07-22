using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    public float speed = 5f;
    private bool isLadder, isClimbing;
    private Rigidbody2D rb;
    public static LadderMovement instance;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (instance == null)
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxisRaw("Vertical");
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0f, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Ladder))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tag.Ladder))
        {
            isLadder = false;
            isClimbing = false;
            Debug.Log(isClimbing);
        }
    }
}
