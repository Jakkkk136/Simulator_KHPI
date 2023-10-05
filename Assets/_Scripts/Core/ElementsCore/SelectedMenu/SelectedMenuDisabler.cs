using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.Core.Elements.SelectedMenu
{
	public class SelectedMenuDisabler : MonoBehaviour, IPointerDownHandler
	{
		public void OnPointerDown(PointerEventData eventData)
		{
			if (SelectedMenu.Instance)
			{
				SelectedMenu.Instance.HideMenu();
			}
		}
	}
}