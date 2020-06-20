using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MagicksAndTechinicsPanelManager : PanelManager
{
    bool toActions = false;
    bool toBlackMages = false;
    public override Text[] GetTexts()
    {
        List<Text> newTexts = new List<Text>();
        Text[] texts = this.gameObject.GetComponentsInChildren<Text>();
        for (int i = 0; i < texts.Length - 1; i++)
        {
            newTexts.Add(texts[i + 1]);
        }
        texts = newTexts.ToArray();
        return texts;
    }

    public override UIManager.HUDstate processState(bool xPressed, bool bPressed)
    {
        Debug.Log(GetTexts().Length);
        if(xPressed)
        {
            this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
            toActions = true;
            
        }
       
        else if(bPressed)
        {
            if(currentIndex == 0)
            {
                this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
                toBlackMages = true;
                
            }
        }
        if (this.gameObject.GetComponent<PopUpUiEffect>().IsClosed())
        {
            if(toActions)
                return UIManager.HUDstate.actions;
            if(toBlackMages)
                return UIManager.HUDstate.blackMages;
        }
        return UIManager.HUDstate.magicksETechinics;
    }
    
}
