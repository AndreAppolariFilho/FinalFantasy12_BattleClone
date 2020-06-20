using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Manager<GameManager>
{
    // Start is called before the first frame update
    
    public enum GameState
    {
        HUDMode,
        Running
    }
    public GameState gameState = GameState.Running;
    public HeroController heroController;
    public HeroController hero
    {
        get
        {
            if(heroController == null)
            {
                heroController = FindObjectOfType<HeroController>();
                
            }
            return heroController;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        bool squarePressed = Input.GetButtonDown("x button");
        
        if(squarePressed)
        { 
            
        }
    }
}
