using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackable
{
    void OnDamage(GameObject attacker, Attack attack);
    
}
