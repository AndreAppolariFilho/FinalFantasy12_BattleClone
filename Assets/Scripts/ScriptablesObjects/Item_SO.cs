using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="Item",menuName ="Item")]
public class Item_SO : ScriptableObject
{
    // Start is called before the first frame update
    public string itemName;
    public Material itemMaterial;
    public Weapon weapon;
    public int strength;
}
