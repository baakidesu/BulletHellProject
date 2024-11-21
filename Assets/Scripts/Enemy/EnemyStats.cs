using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Lean.Pool;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
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
    
    [Header("Damage Feedback")]
    public Color damageColor = new Color(1,0,0,1);
    public float damageFlashDuration = 0.2f;
    public float deathFadeTime = 0.6f;
    Color originalColor;
    SpriteRenderer sr;
    EnemyMovement movement;

    public void Awake()
    {
        currentDamage = enemyData.Damage;
        currentHealth = enemyData.MaxHealth;
        currentMoveSpeed = enemyData.MoveSpeed;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform; //todo zenject
        
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;

        movement = GetComponent<EnemyMovement>();
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

    public void TakeDamage(float damage, Vector2 sourcePosition, float knockbackForce = 5f, float knockbackDuration = 0.2f)
    {
        currentHealth -= damage;
        StartCoroutine(DamageFlash());

        if (damage > 0)
        {
            GameManager.GenerateFloatingText(Mathf.FloorToInt(damage).ToString(), transform);
        }

        if (knockbackForce > 0)
        {
            Vector2 dir  = (Vector2)transform.position - sourcePosition;
            movement.Knockback(dir.normalized * knockbackForce, knockbackDuration);
        }
        
        if (currentHealth <=0)
        {
            Kill();
        }
    }

    IEnumerator DamageFlash()
    {
        sr.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        sr.color = originalColor;
    }

    IEnumerator KillFade()
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, origAlpha = sr.color.a;
        while (t < deathFadeTime)
        {
            yield return w;
            t+= Time.deltaTime;
            
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1-t / deathFadeTime) * origAlpha);
        }
        
        Destroy(gameObject);
    }

    public void Kill()
    {
        Destroy(gameObject);
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
