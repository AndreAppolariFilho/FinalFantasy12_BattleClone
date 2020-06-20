using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterStats_SO characterDefinition_Template;
    public CharacterStats_SO characterDefinition;
    // Start is called before the first frame update
    public GameObject characterWeaponSlot;
    void Start()
    {
        
    }
    private void Awake()
    {
        if (characterDefinition_Template != null)
            characterDefinition = Instantiate(characterDefinition_Template);
        characterDefinition.EquipWeapon(characterDefinition.weapon, characterWeaponSlot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int GetCurrentHealth()
    {
        return characterDefinition.currentHealth;
    }
    public int GetCurrentMana()
    {
        return characterDefinition.currentMana;
    }

    public int GetResistance()
    {
        return characterDefinition.baseResistance;
    }
    public int GetCurrentDamage()
    {
        return characterDefinition.currentDamage;
    }
    public int GetCurrentATB()
    {
        return characterDefinition.currentATB;
    }
    public void IncreaseATB( int amount)
    {
        if(characterDefinition.currentATB < characterDefinition.maxATB)
            characterDefinition.currentATB = characterDefinition.currentATB + amount;
    }
    public void clearATB()
    {
        characterDefinition.currentATB = 0;
    }
    public int GetMaxHealth()
    {
        return characterDefinition.currentHealth;
    }
    public int GetMaxMana()
    {
        return characterDefinition.currentMana;
    }
    public int GetMaxDamage()
    {
        return characterDefinition.currentDamage;
    }
    public int GetMaxATB()
    {
        return characterDefinition.maxATB;
    }
    public Weapon GetCurrentWeapon()
    {
        if (characterDefinition.weapon != null)
            return characterDefinition.weapon.itemDefinition.weapon;
        return null;
    }
    public List<AttackDefinition> GetMeelleAttacks()
    {
        return characterDefinition.attacks_meelee;
    }
    public List<AttackDefinition> GetBlackMages()
    {
        return characterDefinition.black_mage;
    }
}
