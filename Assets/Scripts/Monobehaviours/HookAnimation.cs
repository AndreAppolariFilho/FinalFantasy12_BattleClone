using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    void Attack()
    {
        GetComponentInParent<HeroController>().Attack();
    }
    void StopAttack()
    {
        GetComponentInParent<HeroController>().StopAttack();
    }
}
