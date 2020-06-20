using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="OneHandedWeapon", menuName ="Attack/OneHandedWeapon")]
public class Weapon : AttackDefinition
{
    // Start is called before the first frame update
    public Rigidbody weaponRb;

    public bool CanAttackTarget(GameObject attacker, GameObject defender)
    {
        if (defender == null)
            return false;
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
            return false;
        if (!attacker.transform.IsFacingTarget(defender.transform))
            return false;
        return true;
    }
    public bool ExecuteAttack(GameObject attacker, GameObject defender)
    {
        if (!CanAttackTarget(attacker, defender))
            return false;
        
        var attackerStats = attacker.GetComponent<CharacterStats>();

        var defenderStats = defender.GetComponent<CharacterStats>();

        var attack = AttackCreate(attackerStats, defenderStats);

        var attackables = defender.GetComponentsInChildren(typeof(IAttackable));
        
        foreach (IAttackable a in attackables)
        {
            a.OnDamage(attacker, attack);
        }
        return true;
    }
    
}
