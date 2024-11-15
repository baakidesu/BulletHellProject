using UnityEngine;
using Lean.Pool;

public class KnifeController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnfie = Lean.Pool.LeanPool.Spawn(weaponData.Prefab);
        spawnedKnfie.transform.position = transform.position;
        spawnedKnfie.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedVector);
    }
    
}
    
