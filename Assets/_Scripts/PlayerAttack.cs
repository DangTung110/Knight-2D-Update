using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animatorPlayer;
    [SerializeField] private GameObject attackPointDistant;
    [SerializeField] private Transform attackPointClose;
    [SerializeField] private Animator animmatorAttackPoint;
    [SerializeField] private AttackPointController attackPointController;
    public LayerMask enemyLayer;
    public float attackRange;
    public float dameClose;
    private void Awake()
    {
        animatorPlayer = GetComponent<Animator>();
        attackPointDistant.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) Attack(1);
        else if (Input.GetKeyDown(KeyCode.W)) Attack(2);
        else if (Input.GetKeyDown(KeyCode.E)) Attack(3);
        else if (Input.GetKeyDown(KeyCode.R)) Attack(4);
    }
    public void Attack(int key)
    {
        switch (key)
        {
            case 1:
                animatorPlayer.SetTrigger(AnimPlayer.Attack_1);
                break;
            case 2:
                animatorPlayer.SetTrigger(AnimPlayer.Attack_2);
                attackPointController.dameAttackPoint = 4;
                break;
            case 3:
                animatorPlayer.SetTrigger(AnimPlayer.Flame);
                attackPointController.dameAttackPoint = 4;
                break;
             case 4:
                animatorPlayer.SetTrigger(AnimPlayer.FireBall);
                attackPointController.dameAttackPoint = 7;
                break;
            default:
                break;
        }
    }
    public void AttackPoint(int key)
    {
        float h = transform.localScale.x / math.abs(transform.localScale.x);
        attackPointDistant.GetComponent<AttackPointController>().SetScaleAttackPoint(h, gameObject.GetComponent<PlayerController>().isFacingRight);
        if (key == 2)
        {
            attackPointDistant.transform.localPosition = transform.localPosition + new Vector3(2.43f * h, 0f, 0f);
            attackPointDistant.SetActive(true);
            animmatorAttackPoint.SetBool(AnimAttackPoint.Attack_2, true);
        }
        else if (key == 4) 
        {
            attackPointDistant.transform.localPosition = transform.localPosition + new Vector3(2.43f * h, 0.44f, 0f);
            attackPointDistant.SetActive(true);
            animmatorAttackPoint.SetBool(AnimAttackPoint.FireBall, true);
        }
        else if (key == 3)
        {
            attackPointDistant.transform.localPosition = transform.localPosition + new Vector3(2.43f * h, 0.44f, 0f);
            attackPointDistant.SetActive(true);
            animmatorAttackPoint.SetBool(AnimAttackPoint.Flame, true);
        }
    }
    public void AttackClose()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPointClose.position, attackRange, enemyLayer);
        foreach (Collider2D hit in hits)
        {
            EnemyController enemy = hit.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDame(dameClose);
                enemy.isTakeDame = true;
                Debug.Log("Dame Close: " + dameClose);
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(attackPointClose.position, attackRange);
    }
}