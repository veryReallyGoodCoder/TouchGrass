using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyBehaviourScript : MonoBehaviour
{
    private Rigidbody2D enemyRb;

    [SerializeField] private Transform target;

    [SerializeField] private float enemyMoveSpeed = 5;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distToTarget = Vector2.Distance(target.position, transform.position);

        if (distToTarget > 0.001f)
        {
            EnemyMoveTo(target);
        }
        else
        {
            enemyRb.totalForce = Vector2.zero;
        }
    }

    private void EnemyMoveTo(Transform target)
    {
        if (target == null) return;

        Vector2 targetPoint = (target.position - transform.position);
        Vector2 steering = targetPoint - enemyRb.linearVelocity;
        enemyRb.AddForce(steering * enemyMoveSpeed);

    }

}
