using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 1f;
    public int damage = 1;
    public LayerMask enemyLayer;
    public float attackCooldown = 0.3f;

    private float nextAttackTime = 0f;
    //other.GetComponent<EnemyHealth>()?.TakeDamage(damage);

    void Update()
    {
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
            
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
