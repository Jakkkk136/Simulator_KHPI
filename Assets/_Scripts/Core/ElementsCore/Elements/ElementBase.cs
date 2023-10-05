using _Configs.ScriptableObjectsDeclarations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ElementBase : MonoBehaviour
{
    [SerializeField] protected Image activeStateSpriteHolder;
    [SerializeField] protected TextMeshProUGUI orderPressText;
    
    [Header("Sets Dynamically")]
    public ElementData elementData;

    protected bool _elementState;
    protected int _correctPressOrder;

    protected GameObject selectionGameObject;
    
    public bool isTaskTarget;
    
    public bool ElementState
    {
        get => _elementState;
        set
        {
            _elementState = value;
            activeStateSpriteHolder.sprite = _elementState ? elementData.activeStateSprite : elementData.inactiveStateSprite;
        }
    }
    
    
    public void SetNewLocalScale(Vector3 localScale)
    {
        transform.localScale = localScale;

        SetDefaultScaleOfText();
    }

    protected void SetDefaultScaleOfText()
    {
        /*Vector3 thisScale = transform.localScale;
        thisScale.z = 1f;
        Vector3 scaleForText = Vector3.one;

        scaleForText.x /= thisScale.x;
        scaleForText.y /= thisScale.y;
        scaleForText.z /= thisScale.z;

        orderPressText.transform.localScale = scaleForText;*/
        
        orderPressText.transform.localScale = Vector3.one;
    }

    public virtual void SetAsTaskTarget(bool isTaskTarget, GameObject selectionGameObject)
    {
        this.isTaskTarget = isTaskTarget;
        this.selectionGameObject = selectionGameObject;
    }
}
