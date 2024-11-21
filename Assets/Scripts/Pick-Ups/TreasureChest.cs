using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreasureChest : MonoBehaviour
{
    InventoryManager inventory;

    private void Start()
    {
        inventory = FindObjectOfType<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OpenTresureChest();
            Destroy(gameObject);
        }
    }

    public void OpenTresureChest()
    {
        if (inventory.GetPossibleEvolutions().Count() <= 0)
        {
            Debug.LogWarning("No Available Evolutions");
            return;
        }
        
        WeaponEvolutionBlueprint toEvolve = inventory.GetPossibleEvolutions()[Random.Range(0, inventory.GetPossibleEvolutions().Count)];
        inventory.EvolveWeapon(toEvolve);
    }
}
