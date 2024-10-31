using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
public class PropRandomizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;
    
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int random = Random.Range(0, propPrefabs.Count);
            GameObject prop = Lean.Pool.LeanPool.Spawn(propPrefabs[random], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
