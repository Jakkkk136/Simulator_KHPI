using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Core.Elements.SelectedMenu
{
	public class SelectedMenu : Patterns.Singleton<SelectedMenu>
	{
		[SerializeField] private Button setTaskTargetButton;
		[SerializeField] private Button deleteButton;
		[SerializeField] private Button duplicateButton;
		[SerializeField] private Button scaleButton;
		[SerializeField] private Button rotateButton;
		[SerializeField] private Button switchStateButton;
		[Space] 
		[SerializeField] private TMP_InputField correctPressOrderInputField;
		[Space]
		[SerializeField] private GameObject scaleToolPanel;
		[SerializeField] private Slider horizontalScaleSlider;
		[SerializeField] private Slider verticalScaleSlider;

		private Camera cam;

		private ElementInEditMode currentElement;

		private List<ElementInEditMode> currentEditedElements;

		private void Awake()
		{
			cam = Camera.main;
		}
		

		public void ActivateNearElement(ElementInEditMode element)
		{
			currentElement = element;

			RectTransform elementRect = element.transform as RectTransform;
			Vector3 elementPos = elementRect.position;
			Vector3 thisPos = elementPos;
			
			bool activateFromLeft = cam.ScreenToViewportPoint(elementPos).x > 0.5f;
			float xOffset = Screen.width * 0.05f;
			thisPos.x = activateFromLeft ? elementPos.x - xOffset : elementPos.x + xOffset;

			transform.position = thisPos;
			
			ActivateMenu();
		}

		private void SetElementAsTaskTarget()
		{
			CreatorWindow.Instance.SetElementAsTaskTarget(currentElement);
		}
		
		private void DeleteElement()
		{
			CloseScaleTool();
			currentElement.DeleteElement();
		}

		private void DuplicateElement()
		{
			CloseScaleTool();
			currentElement.DuplicateElement();
		}

		private void RotateElement()
		{
			CloseScaleTool();

			currentElement.RotateElement();
		}

		private void SwitchElementState()
		{
			CloseScaleTool();

			currentElement.ElementState = !currentElement.ElementState;
		}

		private void OnChangeInputField(String newInput)
		{
			if(string.IsNullOrEmpty(newInput)) return;
			if (Int32.TryParse(newInput, out int result))
			{
				currentElement.CorrectPressOrder = result;
			}
		}

		private void OpenScaleTool()
		{
			scaleButton.onClick.RemoveAllListeners();
			scaleButton.onClick.AddListener(CloseScaleTool);
			
			scaleToolPanel.SetActive(true);

			Vector2 savedScaleParams = currentElement.elementData.scale;

			horizontalScaleSlider.SetValueWithoutNotify(Mathf.InverseLerp(0.01f, 1f, savedScaleParams.x));
			verticalScaleSlider.SetValueWithoutNotify(Mathf.InverseLerp(0.01f, 1f, savedScaleParams.y));

			currentEditedElements = currentElement.creatorWindow.GetListOfSpawnedElements(currentElement);
			
			horizontalScaleSlider.onValueChanged.RemoveAllListeners();
			verticalScaleSlider.onValueChanged.RemoveAllListeners();
			
			horizontalScaleSlider.onValueChanged.AddListener(ChangeHorizontalScale);
			verticalScaleSlider.onValueChanged.AddListener(ChangeVerticalScale);
		}

		public void CloseScaleTool()
		{
			scaleButton.onClick.RemoveAllListeners();
			scaleButton.onClick.AddListener(OpenScaleTool);
			
			scaleToolPanel.SetActive(false);
		}

		
		private void ChangeHorizontalScale(float normalizedValue)
		{
			Vector2 newScale = currentElement.elementData.scale;
			newScale.x = Mathf.Lerp(0.01f, 1f, normalizedValue);

			foreach (ElementInEditMode element in currentEditedElements)
			{
				element.SetNewLocalScale(newScale);
			}

			currentElement.elementData.SetXScaleParam(newScale.x);
		}

		private void ChangeVerticalScale(float normalizedValue)
		{
			Vector2 newScale = currentElement.elementData.scale;
			newScale.y = Mathf.Lerp(0.01f, 1f, normalizedValue);

			foreach (ElementInEditMode element in currentEditedElements)
			{
				element.SetNewLocalScale(newScale);
			}

			currentElement.elementData.SetYScaleParam(newScale.y);
		}

		private void ActivateMenu()
		{
			setTaskTargetButton.gameObject.SetActive(true);
			deleteButton.gameObject.SetActive(true);
			duplicateButton.gameObject.SetActive(true);
			scaleButton.gameObject.SetActive(true);
			rotateButton.gameObject.SetActive(true);
			switchStateButton.gameObject.SetActive(true);
			
			setTaskTargetButton.onClick.RemoveAllListeners();
			deleteButton.onClick.RemoveAllListeners();
			duplicateButton.onClick.RemoveAllListeners();
			scaleButton.onClick.RemoveAllListeners();
			rotateButton.onClick.RemoveAllListeners();
			switchStateButton.onClick.RemoveAllListeners();
			
			correctPressOrderInputField.gameObject.SetActive(true);
			correctPressOrderInputField.onValueChanged.RemoveAllListeners();
			correctPressOrderInputField.onValueChanged.AddListener(OnChangeInputField);
			
			setTaskTargetButton.onClick.AddListener(SetElementAsTaskTarget);
			deleteButton.onClick.AddListener(DeleteElement);
			duplicateButton.onClick.AddListener(DuplicateElement);
			scaleButton.onClick.AddListener(OpenScaleTool);
			rotateButton.onClick.AddListener(RotateElement);
			switchStateButton.onClick.AddListener(SwitchElementState);
		}

		public void HideMenu()
		{
			setTaskTargetButton.gameObject.SetActive(false);
			deleteButton.gameObject.SetActive(false);
			duplicateButton.gameObject.SetActive(false);
			scaleButton.gameObject.SetActive(false);
			rotateButton.gameObject.SetActive(false);
			switchStateButton.gameObject.SetActive(false);

			correctPressOrderInputField.gameObject.SetActive(false);

			scaleToolPanel.SetActive(false);
		}
	}
}