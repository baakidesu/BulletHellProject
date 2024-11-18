using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Privates

    private Transform player;

    private EnemyStats enemy;
    
    #endregion
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, enemy.currentMoveSpeed * Time.deltaTime);
    }
}
