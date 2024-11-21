using System;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyAfterSecond;
    public WeaponScriptableObject weaponData;
    
    //current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    private void Awake()
    {
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Lean.Pool.LeanPool.Despawn(gameObject, destroyAfterSecond);
    }

    public float GetCurrentDamage()
    {
        return currentDamage * FindObjectOfType<PlayerStats>().CurrentMight; //todo zenject
    }

    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        
        float dirX = direction.x;
        float dirY = direction.y;
        
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirX < 0 && dirY == 0) //left
        {
            scale.x *= -1;
            scale.y *= -1;
        }else if (dirX == 0 && dirY < 0) //down
        {
            scale.y *= -1;
        }else if (dirX == 0 && dirY > 0) //up
        {
            scale.x *= -1;
        }else if (dirX > 0 && dirY > 0) //right up
        {
            rotation.z = 0f;
        }else if (dirX > 0 && dirY < 0) // right down
        {
            rotation.z = -90f;
        }else if (dirX < 0 && dirY > 0) // left up
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = -90f;
        }else if (dirX < 0 && dirY < 0) //left down
        {
            scale.y *= -1;
            scale.x *= -1;
            rotation.z = 0f;
        }     
        
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
        
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage(),transform.position);
            ReducePierce();
        }else if (other.CompareTag("Prop"))
        {
            if (other.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
        }
    }

    void ReducePierce()
    {
        currentPierce--;
        if (currentPierce <= 0)
        {
            Lean.Pool.LeanPool.Despawn(gameObject);
        }
    }
}
