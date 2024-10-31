using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Lean.Pool;
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

    private PlayerMovement pM;
    public Vector3 noTerrainPosition;
    
    private float opdist;
    private GameObject latestChunk;
    private float optimizerCooldown;
    
    #endregion
    
    [Inject]
    public void Construct(PlayerMovement playerM)
    {
        pM = playerM;  
        Debug.Log("MapController::Construct");
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
        
        if (pM.moveDir.x > 0 && pM.moveDir.y == 0) //right
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunks();
            }
        }else if (pM.moveDir.x < 0 && pM.moveDir.y == 0) //left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunks();
            }
        }else if (pM.moveDir.x == 0 && pM.moveDir.y > 0) //up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunks();
            }
        }else if (pM.moveDir.x == 0 && pM.moveDir.y < 0) //down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunks();
            }
        }else if (pM.moveDir.x < 0 && pM.moveDir.y < 0) // left down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                SpawnChunks();
            }
        }else if (pM.moveDir.x > 0 && pM.moveDir.y < 0) // right down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                SpawnChunks();
            }
        }else if (pM.moveDir.x > 0 && pM.moveDir.y > 0) // right up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                SpawnChunks();
            }
        }else if (pM.moveDir.x < 0 && pM.moveDir.y > 0) // left up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                SpawnChunks();
            }
        }
    }

    void SpawnChunks()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Lean.Pool.LeanPool.Spawn(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
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
