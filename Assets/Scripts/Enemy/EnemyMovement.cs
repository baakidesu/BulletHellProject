using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Privates

    private Transform player;

    private EnemyStats enemy;
    
    private Vector2 knockbackVelocity;
    private float knockbackDuration;
    
    #endregion
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, enemy.currentMoveSpeed * Time.deltaTime); // vector 2 olabilir
        }
    }

    public void Knockback(Vector2 velocity, float duration)
    {
        if (knockbackDuration > 0)
        {
            return;
        }
        
        knockbackVelocity = velocity;
        knockbackDuration = duration;
    }
}
