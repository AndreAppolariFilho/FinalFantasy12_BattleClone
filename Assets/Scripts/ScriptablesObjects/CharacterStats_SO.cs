using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewStats", menuName = "Character/Stats")]
public class CharacterStats_SO : ScriptableObject
{
    public string name;
    public bool isHero;
    public Item weapon;
    public int currentDamage;
    public int baseDamage;
    public int currentHealth;
    public int maxHealth;
    public int currentMana;
    public int maxMana;
    public int currentATB;
    public int maxATB;
    public int baseResistance;
    [Range(0, 1)]
    public float blockChance;
    public List<AttackDefinition> attacks_meelee;
    public List<AttackDefinition> white_mage;
    public List<AttackDefinition> black_mage;
    public void EquipWeapon(Item weapon, GameObject weaponSlot)
    {
        this.weapon = weapon;
        Rigidbody newWeapon;
        if(weapon)
        { 
            newWeapon = Instantiate(weapon.itemDefinition.weapon.weaponRb, weaponSlot.transform);
            currentDamage = baseDamage + weapon.itemDefinition.strength;
        }
    }
}
