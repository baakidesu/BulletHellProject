using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/PassiveItemScriptableObject")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    private float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value;  }
    
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
}
