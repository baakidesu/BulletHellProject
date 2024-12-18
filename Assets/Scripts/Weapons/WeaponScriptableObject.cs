using System;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] GameObject prefab;

    public GameObject Prefab
    {
        get => prefab;
        private set => prefab = value;
    }

    //base stats
    [SerializeField]
    float damage;
    public float Damage {get => damage; private set => damage = value;}

    [SerializeField] 
    float speed;
    public float Speed {get => speed; private set => speed = value;}
    
    [SerializeField] 
    float cooldownDuration;
    public float CooldownDuration {get => cooldownDuration; private set => cooldownDuration = value;}
    
    [SerializeField] 
    int pierce;
    public int Pierce {get => pierce; private set => pierce = value;}
    
    [SerializeField] 
    int level;
    public int Level {get => level; private set => level = value;}
    
    [SerializeField] 
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab {get => nextLevelPrefab; private set => nextLevelPrefab = value;}

    [SerializeField] 
    new String name;
    public String Name {get => name; private set => name = value;}
    
    [SerializeField] 
    String description;
    public String Description {get => description; private set => description = value;}

    [SerializeField] 
    private Sprite icon;
    public Sprite Icon {get => icon; private set => icon = value;}

    [SerializeField] 
    private int evolvedUpgradeToRemove;
    public int EvolvedUpgradeToRemove {get => evolvedUpgradeToRemove; private set => evolvedUpgradeToRemove  = value;}

}
