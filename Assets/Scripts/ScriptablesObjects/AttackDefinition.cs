using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="BasicAttack", menuName ="Attack/BasicAttack")]
public class AttackDefinition : ScriptableObject
{
    // Start is called before the first frame update
    public enum Type
    {
        Physical,
        Magical
    }
    public string name;
    public int timeToCast;
    public float range;
    public float minDamage;
    public float maxDamage;
    [Range(0,1)]
    public float criticalChance;
    public Type attackType = Type.Physical;
    public Attack AttackCreate(CharacterStats wielderStats, CharacterStats defenderStats)
    {
        float damage = wielderStats.characterDefinition.baseDamage;
        damage += Random.Range(minDamage, maxDamage);
        bool crit = Random.value < criticalChance;
        bool block = Random.value < defenderStats.characterDefinition.blockChance;
        if (crit)
            damage *= criticalChance;
        
        damage -= defenderStats.GetResistance();
        if (block)
            damage = 0;
        return new Attack((int) damage, crit, block);
    }
}
