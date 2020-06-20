using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentIndex = 0;
    protected GameObject cursor;
    [SerializeField] private GameObject cursorPrefab;
    [SerializeField] protected GameObject textPrefab;
    [SerializeField] protected GameObject cursorPositionReference;
    protected UIManager.HUDstate previousPanel ;
    
    public void OnEnable()
    {
        CreatePanel();
    }
    public void OnDisable()
    {
        DestroyImmediate(cursor);
        this.gameObject.GetComponent<PopUpUiEffect>().ResetEffect();
        //Debug.Log("Disabling");
        //DestroyPanel();
    }
    public virtual Text[] GetTexts()
    {
        return this.GetComponentsInChildren<Text>();
    }
    public virtual void CreatePanel()
    {       
        this.gameObject.GetComponent<PopUpUiEffect>().BeginAlpha();
        currentIndex = 0;
        FindObjectOfType<UIManager>().GetComponent<AudioSource>().clip = FindObjectOfType<UIManager>().cursorConfirmSound;
        FindObjectOfType<UIManager>().GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<UIManager>().cursorConfirmSound);
        cursor = Instantiate(cursorPrefab, this.gameObject.transform).gameObject;

        cursor.transform.localPosition = cursorPositionReference.transform.localPosition;
    }
    protected void CreateText(Text text, float posy)
    {
        text.transform.localPosition = new Vector3(text.transform.localPosition.x, posy, text.transform.localPosition.z);

        if ((text.transform.localPosition.y + text.GetComponent<RectTransform>().rect.y) < -(this.gameObject.GetComponent<RectTransform>().rect.height / 2.0f))
        {
            this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + text.rectTransform.rect.height * 2);
        }
    }
    public abstract UIManager.HUDstate processState(bool xPressed, bool bPressed);
    public void MoveCursorVertical(float yAxis)
    {
        Text[] texts = GetTexts();
        if (yAxis < 0)
        {
            if (currentIndex < texts.Length - 1)
            {
                currentIndex += 1;
                FindObjectOfType<UIManager>().GetComponent<AudioSource>().clip = FindObjectOfType<UIManager>().cursorMoveSound;
                FindObjectOfType<UIManager>().GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<UIManager>().cursorMoveSound);
            }
        }
        if (yAxis > 0)
        {
            if (currentIndex > 0)
            {
                currentIndex -= 1;
                FindObjectOfType<UIManager>().GetComponent<AudioSource>().clip = FindObjectOfType<UIManager>().cursorMoveSound;
                FindObjectOfType<UIManager>().GetComponent<AudioSource>().PlayOneShot(FindObjectOfType<UIManager>().cursorMoveSound);
            }
        }
       if (cursor)
        {
            cursor.transform.position = new Vector3(cursorPositionReference.transform.position.x, texts[currentIndex].gameObject.transform.position.y + texts[currentIndex].flexibleHeight / 2, cursor.transform.position.z);
        }
    }
    public void SetPreviousPanel(UIManager.HUDstate previousPanel)
    {
        this.previousPanel = previousPanel;
    }

    public virtual void DestroyPanel()
    {
        //Debug.Log("Deactivating");
        DestroyImmediate(cursor);
        //this.gameObject.SetActive(false);
        this.gameObject.GetComponent<PopUpUiEffect>().CloseUI();
        

    }

}
