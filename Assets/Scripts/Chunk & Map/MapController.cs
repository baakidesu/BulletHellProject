using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Lean.Pool;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    #region Publics

    public List<GameObject> terrainChunks;
    public GameObject player;
    public LayerMask terrainMask;
    public float checkerRadius;
    public GameObject currentChunk;
    
    public List<GameObject> spawnedChunks;
    public float maxOpdist;
    public float optimizerCooldownDuration;
    
    #endregion
    
    #region Privates
    
    private float opdist;
    private GameObject latestChunk;
    private float optimizerCooldown;
    
    private Vector3 playerLastPosition;
    
    #endregion

    private void Start()
    {
        playerLastPosition = player.transform.position;
    }

    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }
        
        Vector3 moveDir = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDir);
        CheckAndSpawnChunk(directionName);

        //çapraz durumlar
        if (directionName.Contains("Up"))
        {
            CheckAndSpawnChunk("Up");
        }
        if (directionName.Contains("Right"))
        {
            CheckAndSpawnChunk("Right");
        }
        if (directionName.Contains("Left"))
        {
            CheckAndSpawnChunk("Left");
        }
        if (directionName.Contains("Down"))
        {
            CheckAndSpawnChunk("Down");
        }
        
    }

    void CheckAndSpawnChunk(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;

        if (MathF.Abs(direction.x) > MathF.Abs(direction.y))
        {
            if (direction.y > 0.5f)
            {
                return direction.x > 0 ? "Right Up" : "Left Up";
            }else if (direction.y < -0.5f)
            {
                return direction.x > 0 ? "Right Down" : "Left Down";
            }
            else
            {
                return direction.x > 0 ? "Right" : "Left"; 
            }
        }
        else
        {
            if (direction.x > 0.5f)
            {
                return direction.y > 0 ? "Right Up" : "Left Up";
            }else if (direction.x < -0.5f)
            {
                return direction.y > 0 ? "Left Up" : "Left Down";
            }
            else
            {
                return direction.y > 0 ? "Up" : "Down";
            }  
        }
    }

    void SpawnChunk(Vector3 spawnPosition)
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = LeanPool.Spawn(terrainChunks[rand], spawnPosition, Quaternion.identity); //leanpool
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        
        foreach (GameObject chunk in spawnedChunks)
        {
            opdist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opdist > maxOpdist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
