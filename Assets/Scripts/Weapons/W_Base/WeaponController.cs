using UnityEngine;

public class WeaponController : MonoBehaviour
{
    #region Publics

    [Header("Weapon Stats")] 
    public WeaponScriptableObject weaponData;
    
    #endregion
    
    #region Privates
    
    private float currentCooldown;

    protected PlayerMovement pm;
    
    #endregion
    
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration;
    }

    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }
    
    protected virtual void Attack()
    {
        currentCooldown = weaponData.CooldownDuration;
    }
}
