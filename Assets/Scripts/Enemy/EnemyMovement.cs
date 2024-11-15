using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region Publics

    public EnemyScriptableObject enemyData;
    
    #endregion 
    
    #region Privates

    private Transform player;
    
    #endregion
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, enemyData.MoveSpeed * Time.deltaTime);
    }
}
