using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsPanelManager : PanelManager
{
    bool advanceToAttack = false;
    bool advanceToMagicks = false;
    public override UIManager.HUDstate processState(bool xPressed, bool bPressed)
    {
        if(xPressed)
        {
            return UIManager.HUDstate.exit;
        }
        if (bPressed)
        {
            if (currentIndex == 0)
            {
                this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
                Debug.Log(this.gameObject.GetComponent<PopUpUiEffect>().IsClosed());
                advanceToAttack = true;
            }
            if (currentIndex == 1)
            {
                this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
                advanceToMagicks = true;
            }

        }
        if (this.gameObject.GetComponent<PopUpUiEffect>().IsClosed())
        {
            if (advanceToAttack)
            {
                advanceToAttack = false;
                return UIManager.HUDstate.attacks;
            }
            if (advanceToMagicks)
            {
                advanceToMagicks = false;
                return UIManager.HUDstate.magicksETechinics;
            }
        }
        
            
        else
        {
            
            return UIManager.HUDstate.actions;
        }
        return UIManager.HUDstate.actions;
    }
}
