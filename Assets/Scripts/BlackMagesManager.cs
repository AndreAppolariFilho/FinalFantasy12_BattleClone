using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackMagesManager : PanelManager
{
    // Start is called before the first frame update
    bool toMagicksPanel = false;
    bool toTargetsPanel = false;

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
        if (xPressed)
        {
            this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
            toMagicksPanel = true;
            
        }
        if (bPressed)
        {
            this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
            toTargetsPanel = true;
            FindObjectOfType<GameManager>().hero.addAttack(FindObjectOfType<GameManager>().heroController.GetBlackMages()[currentIndex]);
            
        }
        if (this.gameObject.GetComponent<PopUpUiEffect>().IsClosed())
        {
            if (toMagicksPanel)
            {
                toMagicksPanel = false;
                return UIManager.HUDstate.magicksETechinics;
            }
            if (toTargetsPanel)
            {
                toTargetsPanel = false;
                return UIManager.HUDstate.targets;
            }
        }

        return UIManager.HUDstate.blackMages;
    }

    public override void CreatePanel()
    {
        base.CreatePanel();
        List<AttackDefinition> attacks = FindObjectOfType<GameManager>().heroController.GetBlackMages();
        float posy = this.gameObject.GetComponentsInChildren<Text>()[0].rectTransform.localPosition.y - (this.gameObject.GetComponentsInChildren<Text>()[0].rectTransform.rect.height) * 2;
        for (int i = 0; i < attacks.Count; i++)
        {
            Text actualText = Instantiate(textPrefab, this.gameObject.transform).GetComponent<Text>();
            actualText.text = attacks[i].name;
            posy -= (actualText.rectTransform.rect.height) * 2;
            CreateText(actualText, posy);
        }
    }
}
