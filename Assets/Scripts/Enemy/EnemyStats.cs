using System;
using UnityEngine;
using Lean.Pool;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    
    //current stats
    [HideInInspector]
    public float currentHealth; 
    [HideInInspector]
    public float currentDamage;
    [HideInInspector]
    public float currentMoveSpeed;

    public float despawnDistance = 20f;
    private Transform player;

    public void Awake()
    {
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform; //todo zenject
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    private void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relavtiveSpawnPoints[Random.Range(0,es.relavtiveSpawnPoints.Count)].position;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <=0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Lean.Pool.LeanPool.Despawn(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats player = other.gameObject.GetComponentInParent<PlayerStats>();
            player.TakeDamage(currentDamage);//oyuncuya hasar ver
        }
    }

    private void OnDestroy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>(); //zenject todo
        if (es == null) 
        {
            return;
        }
        else
        { 
            es.OnEnemyKilled(); 
        }
    }
}
