using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{
    public int _damage;
    public bool _crit;
    public bool _block;
    public Attack(int damage, bool crit, bool block)
    {
        this._damage = damage;
        this._block = block;
        this._crit = crit;

    }
    public int Damage { get { return _damage; } }
    public bool Crit { get { return _crit; } }
    public bool Block { get { return _block; } }
}
