using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;


public class PopUpUiEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isAnimating = false;
    public Vector3 size;
    public GameObject openAnimationUi = null;
    public GameObject closeAnimationUi = null;
    public bool isOpened = false;
    public bool isClosed = false;
    [SerializeField] private GameObject prefab;
    public delegate void AnimationEnded();

    public void Start()
    {
        
    }
    public void OnEnable()
    {
        size = this.gameObject.GetComponent<RectTransform>().lossyScale;
        UiEventMananer.StartListening("OpenAnimationEnded", showUI);
        UiEventMananer.StartListening("CloseAnimationEnded", disableUI);

        //if (!duplicateInstantiate)
        //{
        //    Debug.Log("Instantiate");
        //    duplicateInstantiate = true;
        //    duplicate = Instantiate(prefab);
        //    
        //    duplicate.SetActive(false);
        //}
        //duplicate.SetActive(false);
    }
    public void OnDisable()
    {
        UiEventMananer.StopListening("OpenAnimationEnded", showUI);
        UiEventMananer.StartListening("CloseAnimationEnded", disableUI);
    }
    public void BeginAlpha()
    {
        if(!isOpened)
        { 
            openAnimationUi = Instantiate(prefab, this.gameObject.transform.parent);
            openAnimationUi.SetActive(true);
            openAnimationUi.AddComponent<OpenUiAnimation>();
            openAnimationUi.AddComponent<CanvasGroup>();
            openAnimationUi.GetComponent<CanvasGroup>().alpha = 0;
            openAnimationUi.GetComponent<CanvasGroup>().blocksRaycasts = true;
            openAnimationUi.GetComponent<CanvasGroup>().interactable = false;
            openAnimationUi.GetComponent<CanvasGroup>().ignoreParentGroups = false;

            this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
             openAnimationUi.GetComponent<CanvasGroup>().alpha = 1;
             openAnimationUi.GetComponent<OpenUiAnimation>().BeginAnimation();
            isOpened = true;
        }

    }
    public void showUI()
    {
        this.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        
    }
    public bool IsOpened()
    {
        return isOpened;
    }
    public bool IsClosed()
    {
        return isClosed;
    }
    public void ResetEffect()
    {
        this.isClosed = false;
        this.isOpened = false;
        this.isAnimating = false;
    }
    public void disableUI()
    {
        Debug.Log("disableUI");
        isClosed = true;
        this.gameObject.GetComponent<CanvasGroup>().alpha = 0;

    }
    public void CloseUI()
    {
        if(!isAnimating && !isClosed)
        {
            isAnimating = true;
            closeAnimationUi = Instantiate(prefab, this.gameObject.transform.parent);
            closeAnimationUi.SetActive(true);
            closeAnimationUi.AddComponent<CloseUiAnimation>();
            closeAnimationUi.AddComponent<CanvasGroup>();
            closeAnimationUi.GetComponent<CanvasGroup>().alpha = 0;
            closeAnimationUi.GetComponent<CanvasGroup>().blocksRaycasts = true;
            closeAnimationUi.GetComponent<CanvasGroup>().interactable = false;
            closeAnimationUi.GetComponent<CanvasGroup>().ignoreParentGroups = false;

            this.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            closeAnimationUi.GetComponent<CanvasGroup>().alpha = 1;
            closeAnimationUi.GetComponent<CloseUiAnimation>().BeginAnimation();
        }
        
    }
    
   
}
