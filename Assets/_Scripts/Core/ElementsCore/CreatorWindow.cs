using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Controllers;
using _Scripts.Controllers.LevelTaskText;
using _Scripts.Core.Elements;
using _Scripts.Patterns;
using UnityEngine;
using UnityEngine.Animations;


public class CreatorWindow : Singleton<CreatorWindow>
{
    [SerializeField] private ElementInCreatorWindow elementInCreatorWindowPrefab;
    [SerializeField] private ElementInEditMode elementInEditModePrefab;
    [SerializeField] private Transform spawnedElementsParent;
    
    [Header("Sets Dynamically")]
    public ElementInEditMode currentTaskTarget;
    public GameObject currentSelectionTaskTarget;

    private bool inited;
    
    private Dictionary<ElementData, List<ElementInEditMode>> spawnedElements = new Dictionary<ElementData, List<ElementInEditMode>>();
    
    public Transform SpawnedElementsParent => spawnedElementsParent;
    public ElementInEditMode ElementInEditModePrefab => elementInEditModePrefab;

    
    private void OnEnable()
    {
        Init();
    }
    
    private void Init()
    {
        if(inited) return;

        List<ElementData> elementDatas = ElementsDatabase.Instance.elements;
    
        ElementInCreatorWindow[] elements = new ElementInCreatorWindow[elementDatas.Count];

        for (int i = 0; i < elements.Length; i++)
        {
            if (i == 0)
            {
                elements[i] = elementInCreatorWindowPrefab;
            }
            else
            {
                elements[i] = Instantiate(elementInCreatorWindowPrefab, elementInCreatorWindowPrefab.transform.parent);
            }
             
            elements[i].Init(this, elementDatas[i]);
        }

        inited = true;
    }

    public void AddSpawnedElement(ElementInEditMode element)
    {
        if (spawnedElements.ContainsKey(element.elementData) == false)
        {
            spawnedElements.Add(element.elementData, new List<ElementInEditMode>());
        }
        
        spawnedElements[element.elementData].Add(element);
    }

    public void SetElementAsTaskTarget(ElementInEditMode element)
    {
        if(currentTaskTarget != null) currentTaskTarget.SetAsTaskTarget(false, null);
        if(currentSelectionTaskTarget != null) DestroyImmediate(currentSelectionTaskTarget);

        if (currentTaskTarget == element)
        {
            currentTaskTarget = null;
            return;
        }

        currentSelectionTaskTarget = Instantiate(ElementsDatabase.Instance.selectionTaskTargetPrefab,
            element.transform.position, Quaternion.identity, element.transform);

        element.SetAsTaskTarget(true, currentSelectionTaskTarget);
        
        currentTaskTarget = element;
    }

    public void RemoveSpawnedElement(ElementInEditMode element)
    {
        if (spawnedElements.ContainsKey(element.elementData) == false)
        {
            spawnedElements.Add(element.elementData, new List<ElementInEditMode>());
        }

        spawnedElements[element.elementData].Remove(element);
    }

    public void ClearSpawnedElements()
    {
        foreach (List<ElementInEditMode> spawnedElementsList in spawnedElements.Values)
        {
            for (int i = spawnedElementsList.Count - 1; i >= 0; i--)
            {
                spawnedElementsList[i].DeleteElement();               
            }
        }
    }

    public void EditExistingLevelConfig()
    {
        LevelTextureHolder.Instance.SetTexture(LevelManager.Instance.levelSo.DeserializeSavedImage());
        ClearSpawnedElements();

        foreach (LevelSO.LevelComponentData levelComponentData in LevelManager.Instance.levelSo.elementsData)
        {
            ElementInEditMode spawnedElementInEditMode =
                Instantiate(
                    ElementInEditModePrefab, 
                    Vector3.zero, 
                    levelComponentData.elementRotation, 
                    SpawnedElementsParent);


            spawnedElementInEditMode.transform.localPosition = levelComponentData.elementLocalPos;
            spawnedElementInEditMode.Init(this, ElementsDatabase.Instance.GetElementData(levelComponentData.elementName));
            spawnedElementInEditMode.CorrectPressOrder = levelComponentData.elementCorrectPressOrder;
            spawnedElementInEditMode.ElementState = levelComponentData.elementState;
            spawnedElementInEditMode.SetNewLocalScale(levelComponentData.elementScale);
            
            if(levelComponentData.isTaskTarget) SetElementAsTaskTarget(spawnedElementInEditMode);
        }
    }

    public List<ElementInEditMode> GetListOfSpawnedElements(ElementInEditMode elementType)
    {
        if (spawnedElements.ContainsKey(elementType.elementData) == false)
        {
            spawnedElements.Add(elementType.elementData, new List<ElementInEditMode>());
        }

        return spawnedElements[elementType.elementData];
    }

    public void FillInLevelDataToConfig()
    {
        LevelManager.Instance.levelSo.AddElementsToDataList(spawnedElements);
        LevelManager.Instance.levelSo.SetLevelTaskText(LevelTaskTextCreatorScene.Instance.LevelTaskText);
    }
}
