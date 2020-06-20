using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttackedAnimation : MonoBehaviour, IAttackable
{
    public void OnDamage(GameObject attacker, Attack attack)
    {
        GetComponent<EnemyController>().TriggerAttackAnimation();
    }
    
}
