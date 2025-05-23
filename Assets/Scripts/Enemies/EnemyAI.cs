using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 5f;
    public float attackRange = 1f;
    public int damage = 1;
    public float attackCooldown = 1f;

    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float nextAttackTime = 0f;

    private Vector2 patrolDirection;
    private float patrolTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        TryFindPlayer();

        patrolDirection = Random.insideUnitCircle.normalized;
        patrolTimer = Random.Range(2f, 4f);
    }

    void Update()
    {
        // Попытка найти игрока, если он ещё не найден
        if (player == null)
        {
            TryFindPlayer();
            return; // Ждём до следующего кадра
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            // Преследует игрока
            movement = (player.position - transform.position).normalized;

            if (distanceToPlayer < attackRange && Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else
        {
            // Патрулирует
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0)
            {
                patrolDirection = Random.insideUnitCircle.normalized;
                patrolTimer = Random.Range(2f, 4f);
            }

            movement = patrolDirection;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        PlayerHealth ph = player.GetComponent<PlayerHealth>();
        if (ph != null)
        {
            ph.TakeDamage(damage);
        }
    }

    void TryFindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }
}
