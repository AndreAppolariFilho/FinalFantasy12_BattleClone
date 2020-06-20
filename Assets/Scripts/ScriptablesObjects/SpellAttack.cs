using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpellCast", menuName = "Attack/SpellCast")]
public class SpellAttack : AttackDefinition
{
    // Start is called before the first frame update
    public GameObject magicEffectPrefab;
    public bool CanCastSpell(GameObject attacker, GameObject defender)
    {
        if (defender == null)
            return false;
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
            return false;
        return true;
    }
    public void CastSpell(GameObject attacker, GameObject defender)
    {
        if (!CanCastSpell(attacker, defender))
            return;

        Instantiate(magicEffectPrefab, defender.transform.position, Quaternion.identity);

        var attackerStats = attacker.GetComponent<CharacterStats>();

        var defenderStats = defender.GetComponent<CharacterStats>();

        var attack = AttackCreate(attackerStats, defenderStats);

        var attackables = defender.GetComponentsInChildren(typeof(IAttackable));

        foreach (IAttackable a in attackables)
        {
            a.OnDamage(attacker, attack);
        }
        
    }
}
