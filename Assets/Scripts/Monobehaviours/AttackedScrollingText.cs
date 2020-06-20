using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackedScrollingText : MonoBehaviour, IAttackable
{
    public ScrollingText Text;
    public Color TextColor;

    public void OnDamage(GameObject attacker, Attack attack)
    {
        var text = attack.Damage.ToString();
        Vector3 position = new Vector3(transform.position.x + 1     , transform.position.y + gameObject.GetComponent<BoxCollider>().size.y, transform.position.z);
        var scrollingText = Instantiate(Text, position, Quaternion.identity);
        if (attack.Block)
        {

            scrollingText.SetText("BLOCK");
            return;
        }
        if (attack.Crit)
        {
            scrollingText.SetText("CRITCAL");
            return;
        }
        
        scrollingText.SetText(text);
        //scrollingText.SetColor(TextColor);
    }
}