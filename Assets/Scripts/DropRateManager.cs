using System;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
public class DropRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }

    public List<Drops> drops;

    private void OnDestroy()
    {
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        
        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if (randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }

        if (possibleDrops.Count > 0)
        {
            //todo en yüksek drop rate olan itemi dropla
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            LeanPool.Spawn(drops.itemPrefab, transform.position, Quaternion.identity);

        }
    }
}