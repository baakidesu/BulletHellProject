using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Zenject;

public class ChunkTrigger : MonoBehaviour
{
    #region Private

    [Inject] private MapController mapController;
    

    #endregion

    #region Public

    public GameObject targetMap;

    #endregion
    
    /*
    [Inject]
    public void Construct(MapController dummyMapController)
    {
        mapController = dummyMapController;
        Debug.Log("ChunkTrigger constructed");
        Debug.Log(mapController);
    }*/

    void Start()
    {
        mapController = FindObjectOfType<MapController>();
    }
   

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mapController.currentChunk = targetMap;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (mapController.currentChunk == targetMap)
            {
                mapController.currentChunk = null;
            }
        }
    }
}
