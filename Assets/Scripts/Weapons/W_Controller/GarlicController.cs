using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;

public class GarlicController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Lean.Pool.LeanPool.Spawn(weaponData.Prefab);
        spawnedGarlic.transform.position = transform.position;
        spawnedGarlic.transform.parent = transform;
    }

    
}
