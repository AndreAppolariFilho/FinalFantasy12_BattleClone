using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDebug : MonoBehaviour,IAttackable
{
    public void OnDamage(GameObject attacker, Attack attack)
    {
        if (attack.Crit)
        {
            Debug.Log("CRITICAL");
        }
        if(attack.Block)
        {
            Debug.Log("BLOCK");
        }
        Debug.LogFormat("{0} attacked {1} for {2} damage.", attacker.name, name, attack.Damage);
    }

    
}
