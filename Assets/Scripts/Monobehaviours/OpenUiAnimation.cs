using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUiAnimation : MonoBehaviour
{
    bool isAnimating = false;
    // Start is called before the first frame update

    void Start()
    {
        
    }

    public void BeginAnimation()
    {
        if(!isAnimating)
            iTween.ScaleFrom(this.gameObject, iTween.Hash("scale", new Vector3(0, 0, 0), "time", 1f, "onstart", "Init", "oncomplete", "Exit"));
    }
    void Init()
    {
        isAnimating = true;
    }
    void Exit()
    {
        isAnimating = false;
        UiEventMananer.TriggerEvent("OpenAnimationEnded");
        DestroyImmediate(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
