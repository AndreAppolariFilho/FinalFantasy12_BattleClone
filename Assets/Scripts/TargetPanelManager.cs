using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetPanelManager : PanelManager
{
    bool toPreviousPanel = false;
    bool toExitPanel = false;

    public override UIManager.HUDstate processState(bool xPressed, bool bPressed)
    {
        if (xPressed)
        {
            this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
            FindObjectOfType<GameManager>().hero.StopDraw();
            toPreviousPanel = true;
            
        }
        else if (bPressed)
        {
            this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
            FindObjectOfType<GameManager>().hero.selectTarget(FindObjectOfType<GameManager>().hero.nearbyEnemies()[currentIndex]);
            toExitPanel = true;

        }
        if (this.gameObject.GetComponent<PopUpUiEffect>().IsClosed())
        {
            if (toPreviousPanel)
            {
                toPreviousPanel = false;
                return this.previousPanel;
            }
            if (toExitPanel)
            {
                toExitPanel = false;
                return UIManager.HUDstate.exit;
            }
        }
        else
        {
            FindObjectOfType<GameManager>().hero.DrawTarget(FindObjectOfType<GameManager>().hero.nearbyEnemies()[currentIndex]);
            return UIManager.HUDstate.targets;
        }
        return UIManager.HUDstate.targets;
    }

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

    public override void CreatePanel()
    {
        base.CreatePanel();
        List<GameObject> enemies = FindObjectOfType<GameManager>().heroController.nearbyEnemies();
        
        float posy = this.gameObject.GetComponentsInChildren<Text>()[0].rectTransform.localPosition.y - (this.gameObject.GetComponentsInChildren<Text>()[0].rectTransform.rect.height) * 2;
        for (int i = 0; i < enemies.Count; i++)
        {
            Text actualText = Instantiate(textPrefab, this.gameObject.transform).GetComponent<Text>();
            actualText.text = enemies[i].name;
            posy -= (actualText.rectTransform.rect.height) * 2;
            CreateText(actualText, posy);
        }
    }
    public override void DestroyPanel()
    {
        base.DestroyPanel();
        Text[] texts = this.gameObject.GetComponentsInChildren<Text>();
        for (int i = 1; i < texts.Length; i++)
        {
            DestroyImmediate(texts[i]);
        }


    }

}
