using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager<UIManager>
{   
    public enum HUDstate
    {
        exit,
        actions,
        attacks,
        targets,
        magicksETechinics,
        blackMages
    }
    public HUDstate hudState = HUDstate.actions;
    public HUDstate previousState = HUDstate.actions;
    public AudioClip cursorMoveSound;
    public AudioClip cursorConfirmSound;
    public float timeBetweenInputs = 0.30f;
    private float currentTime = 0;
    private int currentIndex = 0;
    private bool initialized = false;
    private GameManager gameManager;
    public GameObject actionsPanel;
    public GameObject attacksPanel;
    public GameObject magicksTechinicsPanel;
    public GameObject blackMagicksPanel;
    public GameObject targetsPanel;
    public GameObject cursor;
    public Transform cursorPrefab;
    public Transform textPrefab;
    public Text currentHealth;
    public Text maxHealth;
    public Text currentMP;
    public Text movementText;
    public Image atbProgress;
    private Text [] texts;
    private AttackDefinition attackSelected;
    

    void Start()
    {
        
        gameManager = FindObjectOfType<GameManager>();
        
        Text[] texts = actionsPanel.GetComponentsInChildren<Text>();
        
        currentHealth.text = gameManager.hero.GetCurrentHealth().ToString();
        maxHealth.text = gameManager.hero.GetMaxHealth().ToString();
        currentMP.text = gameManager.hero.GetCurrentMana().ToString();
        atbProgress.fillAmount = (float)gameManager.hero.GetCurrentATB() / gameManager.hero.GetMaxATB();
    }
    public void InitializeUI()
    {   
        initialized = false;
        currentTime = timeBetweenInputs;
    }
    public void finishInitialazation()
    {
        Debug.Log("initialized");
        hudState = actionsPanel.GetComponent<ActionsPanelManager>().processState(false, false);
        initialized = false;
    }
    void LateUpdate()
    {
        bool bPressioned = Input.GetButtonUp("b button");
        bool xPressioned = Input.GetButtonUp("x button") ;
        atbProgress.fillAmount = (float)(gameManager.hero.GetCurrentATB()) / gameManager.hero.GetMaxATB();
        
        if (gameManager.hero.GetCurrentAttack()!= null)
        {
            if (!movementText.IsActive()) movementText.gameObject.SetActive(true);
            movementText.text = gameManager.hero.GetCurrentAttack().name;
        }
        else
        {
            if (movementText.IsActive()) movementText.gameObject.SetActive(false);
        }

        if (gameManager.gameState == GameManager.GameState.Running && xPressioned)
        {
            gameManager.gameState = GameManager.GameState.HUDMode;

        }
        else if (gameManager.gameState == GameManager.GameState.HUDMode)
        {
            texts = actionsPanel.GetComponentsInChildren<Text>();
            if(hudState == HUDstate.exit)
            {
                gameManager.gameState = GameManager.GameState.Running;
            }
            if (hudState == HUDstate.actions )
            {
                if(!actionsPanel.activeInHierarchy)
                {
                    actionsPanel.SetActive(true);
                }
                hudState = actionsPanel.GetComponent<ActionsPanelManager>().processState(xPressioned, bPressioned);
                
                
                if (initialized == true)
                {
                    initialized = false;
                }
                
                if(hudState != HUDstate.actions)
                {
                    actionsPanel.SetActive(false);
                }
            }
            else if (hudState == HUDstate.attacks)
            {
                if(!attacksPanel.activeInHierarchy)
                {   
                    attacksPanel.SetActive(true);
                }
                previousState = hudState;
                hudState = attacksPanel.GetComponent<AttacksPanelManager>().processState(xPressioned, bPressioned);
                
                
                if (hudState != HUDstate.attacks)
                {
                    
                    attacksPanel.SetActive(false);
                }
            }
            else if(hudState == HUDstate.targets)
            {
                if (!targetsPanel.activeInHierarchy)
                {
                    targetsPanel.SetActive(true);
                    targetsPanel.GetComponent<TargetPanelManager>().SetPreviousPanel(previousState);
                }
                
                hudState = targetsPanel.GetComponent<TargetPanelManager>().processState(xPressioned, bPressioned);
                if (hudState != HUDstate.targets)
                {
                    targetsPanel.SetActive(false);
                }
            }
            else if(hudState == HUDstate.magicksETechinics)
            {
                if (!magicksTechinicsPanel.activeInHierarchy)
                {
                    magicksTechinicsPanel.SetActive(true);
                }

                hudState = magicksTechinicsPanel.GetComponent<MagicksAndTechinicsPanelManager>().processState(xPressioned, bPressioned);
                if (hudState != HUDstate.magicksETechinics)
                {
                    magicksTechinicsPanel.SetActive(false);
                }
            }
            else if(hudState == HUDstate.blackMages)
            {
                if (!blackMagicksPanel.activeInHierarchy)
                {
                    blackMagicksPanel.SetActive(true);
                }
                previousState = hudState;
                hudState = blackMagicksPanel.GetComponent<BlackMagesManager>().processState(xPressioned, bPressioned);
                if (hudState != HUDstate.blackMages)
                {
                    blackMagicksPanel.SetActive(false);
                }
            }
            if (currentTime <= 0) {
                float vertical = Input.GetAxis("Left Y Axis");
                if (vertical != 0)
                    currentTime = timeBetweenInputs;
                switch(hudState)
                {
                    case HUDstate.actions:
                        actionsPanel.GetComponent<ActionsPanelManager>().MoveCursorVertical(vertical);
                        break;
                    case HUDstate.attacks:
                        attacksPanel.GetComponent<AttacksPanelManager>().MoveCursorVertical(vertical);
                        break;
                    case HUDstate.blackMages:
                        blackMagicksPanel.GetComponent<BlackMagesManager>().MoveCursorVertical(vertical);
                        break;
                    case HUDstate.magicksETechinics:
                        magicksTechinicsPanel.GetComponent<MagicksAndTechinicsPanelManager>().MoveCursorVertical(vertical);
                        break;
                    case HUDstate.targets:
                        targetsPanel.GetComponent<TargetPanelManager>().MoveCursorVertical(vertical);
                        break;

                }
            }
            else
            {
                if(currentTime > 0)
                {
                    currentTime -= Time.deltaTime;
                }
                else
                {
                    currentTime = 0;
                }
            }

            
        }
        else
        {
            hudState = HUDstate.actions;
        }

    }
}
