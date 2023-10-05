using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Controllers;
using _Scripts.Core.Elements;
using UnityEngine;

public class LevelSetuper : MonoBehaviour
{
	[SerializeField] private Transform parentForSpawnedElements;

	private void Awake()
	{
		LevelTextureHolder.Instance.SetTexture(LevelManager.Instance.levelSo.DeserializeSavedImage());

		foreach (LevelSO.LevelComponentData levelComponentData in LevelManager.Instance.levelSo.elementsData)
		{
			ElementInGame newElement = ElementsDatabase.Instance.GetNewElement(levelComponentData.elementName);

			newElement.InitElementOnGameScene(
				parentForSpawnedElements,
				levelComponentData.elementLocalPos,
				levelComponentData.elementScale,
				levelComponentData.elementRotation,
				levelComponentData.elementState,
				levelComponentData.elementCorrectPressOrder,
				levelComponentData.isTaskTarget);
		}
	}
}
