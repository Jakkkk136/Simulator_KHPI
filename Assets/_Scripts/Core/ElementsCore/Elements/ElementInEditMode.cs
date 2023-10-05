using System;
using _Configs.ScriptableObjectsDeclarations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Core.Elements
{
	[Serializable]
	public class ElementInEditMode : ElementInCreatorWindow
	{
		
		
		public int CorrectPressOrder
		{
			get => _correctPressOrder;
			set
			{
				_correctPressOrder = value;
				orderPressText.text = _correctPressOrder > 0 ? _correctPressOrder.ToString() : String.Empty;
			}
		}
		
		public override void OnPointerDown(PointerEventData eventData)
		{
			SetDragTarget(this);
			SelectedMenu.SelectedMenu.Instance.HideMenu();
		}
		
		public override void Init(CreatorWindow creatorWindow, ElementData data)
		{
			base.Init(creatorWindow, data);
			
			SetNewLocalScale(data.scale);
			
			ElementState = true;
			CorrectPressOrder = 0;

			orderPressText.transform.rotation = Quaternion.identity;

			creatorWindow.AddSpawnedElement(this);
		}

		protected override void SetDragTarget(ElementInEditMode target)
		{
			dragTarget = this;
		}

		public void DeleteElement()
		{
			creatorWindow.RemoveSpawnedElement(this);
			GameObject.DestroyImmediate(gameObject);
			SelectedMenu.SelectedMenu.Instance.HideMenu();
		}
		

		public void DuplicateElement()
		{
			Vector3 newElementPos = transform.position;
			newElementPos.x *= 1.075f;
			newElementPos.y *= 0.925f;
			
			Transform parentOfSelectionObject = selectionGameObject?.transform.parent;

			if (isTaskTarget && selectionGameObject != null)
			{
				selectionGameObject.transform.parent = null;
			}

			ElementInEditMode newElement = Instantiate(this, newElementPos, Quaternion.identity, transform.parent);
			newElement.Init(creatorWindow, elementData);
			newElement.isTaskTarget = false;
			newElement.SetNewLocalScale(transform.localScale);
			newElement.ElementState = ElementState;

			if (isTaskTarget && selectionGameObject != null)
			{
				selectionGameObject.transform.SetParent(parentOfSelectionObject, false);
				selectionGameObject.transform.localPosition = Vector3.zero;
				selectionGameObject.transform.localScale = Vector3.one;
			}
			
			SelectedMenu.SelectedMenu.Instance.HideMenu();
		}

		public void RotateElement()
		{
			transform.rotation *= Quaternion.Euler(0f, 0f, -90f);
			orderPressText.transform.rotation = Quaternion.identity;
		}
	}
}