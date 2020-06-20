using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUiAnimation : MonoBehaviour
{

    bool isAnimating = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void BeginAnimation()
    {
        if (!isAnimating)
            iTween.ScaleTo(this.gameObject, iTween.Hash("scale", new Vector3(0, 0, 0),  "onstart", "Init", "oncomplete", "Exit"));
    }
    void Init()
    {
        isAnimating = true;
    }
    void Exit()
    {
        isAnimating = false;
        UiEventMananer.TriggerEvent("CloseAnimationEnded");
        DestroyImmediate(this.gameObject);
    }
    
    void Update()
    {
        
    }
}
