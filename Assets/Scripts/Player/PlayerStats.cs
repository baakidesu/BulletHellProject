using System;
using System.Collections.Generic;
using Lean.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
     CharacterScriptableObject characterData;
     
     float currentHealth;
     float currentRecovery;
     float currentMoveSpeed;
     float currentMight;
     float currentProjectileSpeed;
     float currentMagnet;
     
    #region Current Stats Properties
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
            {
                currentHealth = value;
                if (GameManager.instance !=null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }
            }
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value) //değer değişmiş mi diye bak
            {
                currentRecovery = value; //gerçek zamanlı olarak değeri güncelle
                if (GameManager.instance !=null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
            }
        }
    }
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed; }
        set
        {
            if (currentMoveSpeed != value) //değer değişmiş mi diye bak
            {
                currentMoveSpeed = value; //gerçek zamanlı olarak değeri güncelle
                if (GameManager.instance !=null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }
            }
        }
    }
    public float CurrentMight
    {
        get { return currentMight; }
        set
        {
            if (currentMight != value) //değer değişmiş mi diye bak
            {
                currentMight = value; //gerçek zamanlı olarak değeri güncelle
                if (GameManager.instance !=null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
            }
        }
    }
    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed; }
        set
        {
            if (currentProjectileSpeed != value) //değer değişmiş mi diye bak
            {
                currentProjectileSpeed = value; //gerçek zamanlı olarak değeri güncelle
                if (GameManager.instance !=null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
            }
        }
    }
    public float CurrentMagnet
    {
        get { return currentMagnet; }
        set
        {
            if (currentMagnet != value) //değer değişmiş mi diye bak
            {
                currentMagnet = value; //gerçek zamanlı olarak değeri güncelle
                if (GameManager.instance !=null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
            }
        }
    }
    #endregion
    
    [Header(("Experiance/Level"))] 
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }
    
    //I-Frames
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    public List<LevelRange> levelRanges;

    private InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text levelText;
    
    public GameObject secondWeaponTest, firstPI, secondPI;
    
    private void Awake()
    {
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        inventory = GetComponent<InventoryManager>();
        
        CurrentHealth = characterData.MaxHealth;
        CurrentMight = characterData.Might;
        CurrentRecovery = characterData.Recovery;
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;
        
        SpawnWeapon(characterData.StartingWeapon);
        
        //test 
        //SpawnWeapon(secondWeaponTest);
        //SpawnPassiveItem(firstPI);
        SpawnPassiveItem(secondPI); 
    }

    void Start()
    {
        experienceCap = levelRanges[0].experienceCapIncrease;//bir sonraki seviyeye geçmek için gereken xp miktarını ilk leveldeki miktara eşitlemek için bu kodu kullandım
        
        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
        
        GameManager.instance.AssignChosenCharacterUI(characterData);
        
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
        
    }

    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }else if (isInvincible)
        {
            isInvincible = false;
        }
        
        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;
        
        LevelUpChecker();
        
        UpdateExpBar();
    }

    void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;
            int experienceCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap+= experienceCapIncrease;
            UpdateLevelText();
            GameManager.instance.StartLevelUp(); 
        }
    }

    void UpdateExpBar()
    {
        expBar.fillAmount = (float)experience / (float)experienceCap;
    }
    void UpdateLevelText()
    {
        levelText.text = "LV" + level.ToString();
    }

    public void TakeDamage(float damage)
    {
        if (!isInvincible)
        {
            CurrentHealth -= damage;
            invincibilityTimer = invincibilityDuration;
            isInvincible = true;
            if (CurrentHealth <=0)
            {
                Kill();
            }
            UpdateHealthBar();
        }
        
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = CurrentHealth / characterData.MaxHealth;
    }

    public void Kill()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameManager.instance.AssignLevelReachedUI(level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver(); 
        }
    }

    public void RestoreHealth(float amount)
    {
        if (CurrentHealth<characterData.MaxHealth)
        {
            CurrentHealth += amount;
            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    void Recover()
    {
        if (CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            if (CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }
        }
    }

    public void SpawnWeapon(GameObject weapon)
    {
        if (weaponIndex >= inventory.weaponSlots.Count -1)
        {
            Debug.LogError("Inventory slots already full!");
        }
        
        GameObject spawnedWeapon = LeanPool.Spawn(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>()); //sıkıntılı bi satır

        weaponIndex++;
    }
    public void SpawnPassiveItem(GameObject passiveItem)
    {
        if (passiveItemIndex >= inventory.passiveItemSlots.Count -1)
        {
            Debug.LogError("Inventory slots already full!");
        }
        
        GameObject spawnedPassiveItem = LeanPool.Spawn(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>()); //sıkıntılı bi satır

        passiveItemIndex++;
    }
}
