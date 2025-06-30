using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float hp;
    [SerializeField] private Slider fillSlider;
    [SerializeField] private Slider yellowSlider;
    public float lerpSpeed = 5f;
    public static HealthController instance;
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        if (instance == null)
            instance = this;
        fillSlider.value = 100f;
    }
    private void Update()
    {
        if (yellowSlider.value != fillSlider.value)
            UpdateYellow();
    }
    public void TakeDame(float hpCurrent, float damage)
    {
        fillSlider.value = hpCurrent * 100 / hp;
        animator.SetTrigger(AnimEnemy.Hurt);
        Debug.Log("hpCurrent: " + hpCurrent);
        Debug.Log("hp: " + hp);
    }
    private void UpdateYellow()
    {
        yellowSlider.value = Mathf.Lerp(yellowSlider.value, fillSlider.value, lerpSpeed * Time.deltaTime);
    }
}
