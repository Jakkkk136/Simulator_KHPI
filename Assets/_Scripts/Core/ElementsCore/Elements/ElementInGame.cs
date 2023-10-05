using System;
using System.Collections.Generic;
using System.Linq;
using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Controllers;
using _Scripts.Controllers.LevelTaskText;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Core.Elements
{
	public class ElementInGame : ElementBase, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private static List<ElementInGame> clickedElements = new List<ElementInGame>();
		
		private bool pressed = false;
		private bool pressedInCorrectOrder = false;

		private int pressedElementsCount;
		private int currentElementsGroup;

		public static int CorrectPressedElements => clickedElements.Count(e => e.pressedInCorrectOrder);
		
		private void OnDestroy()
		{
			clickedElements.Clear();
		}
		
		public void OnPointerDown(PointerEventData eventData)
		{
			_elementState = !_elementState;
			pressed = !pressed;

			if (pressed)
			{
				clickedElements.Add(this);
				pressedElementsCount = clickedElements.Count;
				TestIndexIsCorrect();
				UpdateTextIndex();
				SetCorrectSprite();
			}
			else
			{
				SetDefaultSprite();
				clickedElements.Remove(this);
				SetClearTextIndex();
				UpdateElementsWithHigherIndexes(pressedElementsCount);
			}
			
			LevelManager.Instance.CheckLevelEnd(CorrectPressedElements);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (isTaskTarget)
			{
				LevelTaskTextGame.Instance.ShowLevelTaskText(true);
			}
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (isTaskTarget)
			{
				LevelTaskTextGame.Instance.ShowLevelTaskText(false);
			}
		}

		public ElementInGame Init(ElementData data)
		{
			elementData = data;
			return this;
		}

		public void InitElementOnGameScene(Transform parent, Vector3 localPos, Vector3 scale, Quaternion rotation,
			bool currentState, int correctPressOrder, bool isTaskTarget)
		{
			transform.SetParent(parent);
			transform.localPosition = localPos;
			transform.rotation = rotation;
			SetNewLocalScale(scale);
			
			orderPressText.transform.rotation = Quaternion.identity;
			
			ElementState = currentState;

			if (correctPressOrder > 0)
			{
				_correctPressOrder = correctPressOrder;
			}
			else
			{
				_correctPressOrder = -10;
			}

			if(isTaskTarget) SetAsTaskTarget(true, null);
			
			SetClearTextIndex();
		}
		
		private void UpdateElementsWithHigherIndexes(int removedElementIndex)
		{
			foreach (var element in clickedElements)
				if (element.pressedElementsCount >= removedElementIndex)
					element.DecreaseIndexByOne();

			foreach (var element in clickedElements)
				if (element.pressedElementsCount >= removedElementIndex)
					element.TestIndexIsCorrect();

			foreach (var element in clickedElements)
				if (element.pressedElementsCount >= removedElementIndex)
					element.SetCorrectSprite();

			foreach (var element in clickedElements)
				if (element.pressedElementsCount >= removedElementIndex)
					element.UpdateTextIndex();
		}
		
		private void DecreaseIndexByOne()
		{
			pressedElementsCount--;
		}

		private void TestIndexIsCorrect()
		{
			int elementsInGroupCounter = 0;

			int[] elementsAmountsInElementsGroups = LevelManager.Instance.levelSo.elementsAmountsInElementsGroups;
			bool groupFound = false;
			
			for (int i = 0; i < elementsAmountsInElementsGroups.Length; i++)
			{
				elementsInGroupCounter += elementsAmountsInElementsGroups[i];
				
				if (pressedElementsCount <= elementsInGroupCounter)
				{
					currentElementsGroup = i + 1;
					groupFound = true;
					break;
				}
			}

			if (groupFound == true)
			{
				pressedInCorrectOrder = _correctPressOrder == currentElementsGroup;
			}
			else
			{
				int correctGroupsSum = elementsAmountsInElementsGroups.Sum();
				currentElementsGroup = correctGroupsSum + clickedElements.Count - correctGroupsSum - 1;
			}
		}

		private void SetDefaultSprite()
		{
			activeStateSpriteHolder.sprite =
				_elementState ? elementData.activeStateSprite : elementData.inactiveStateSprite;
		}

		private void SetCorrectSprite()
		{
			if (_elementState == true)
			{
				activeStateSpriteHolder.sprite = elementData.activeStateSprite;

			}
			else
			{
				activeStateSpriteHolder.sprite = elementData.inactiveStateSprite;
			}
		}

		private void UpdateTextIndex()
		{
			int pressGroupToShow = currentElementsGroup;

			if (pressGroupToShow <= 0)
			{
				SetClearTextIndex();
			}
			else
			{
				orderPressText.SetText(pressGroupToShow.ToString());
			}
		}

		private void SetClearTextIndex()
		{
			orderPressText.SetText(string.Empty);
		}

		public override void SetAsTaskTarget(bool isTaskTarget, GameObject selectionGameObject)
		{
			GameObject currentSelectionTaskTarget = Instantiate(ElementsDatabase.Instance.selectionTaskTargetPrefab, transform.position, Quaternion.identity, transform.parent);
			float maxElementScale = Mathf.Max(transform.localScale.x, transform.localScale.y);
			currentSelectionTaskTarget.transform.localScale =
				new Vector3(maxElementScale, maxElementScale, maxElementScale);

			base.SetAsTaskTarget(isTaskTarget, currentSelectionTaskTarget);
		}
	}
}