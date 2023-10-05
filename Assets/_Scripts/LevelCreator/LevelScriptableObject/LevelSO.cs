using System;
using System.Collections.Generic;
using System.Linq;
using _Configs.ScriptableObjectsDeclarations;
using _Scripts.Core.Elements;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
[CreateAssetMenu(fileName = "NewLevelSO", menuName = "KHPI/Level")]
public class LevelSO : ScriptableObject
{
	[Serializable]
	public class LevelComponentData
	{
        public string elementName;
        public Vector2 elementLocalPos;
        public bool elementState;
        public Quaternion elementRotation;
        public Vector3 elementScale;
        public int elementCorrectPressOrder = 0;
        public bool isTaskTarget;
        

        public LevelComponentData(string elementName, Vector2 elementLocalPos, bool elementState,
			Quaternion elementRotation, Vector3 elementScale, int elementCorrectPressOrder, bool isTaskTarget)
		{
			this.elementName = elementName;
			this.elementLocalPos = elementLocalPos;
			this.elementState = elementState;
			this.elementRotation = elementRotation;
			this.elementScale = elementScale;
			this.elementCorrectPressOrder = elementCorrectPressOrder;
			this.isTaskTarget = isTaskTarget;
		}
	}

	public List<LevelComponentData> elementsData = new List<LevelComponentData>() { };
	public int elementsCountForCorrectResult;
	[FormerlySerializedAs("elementsGroupsOnLevel")] public int[] elementsAmountsInElementsGroups;
	public byte[] serializedLevelTexture;
	public string levelPasswordHash;
	public string levelTaskText;
	public string levelName;
	
	public void SetLevelPasswordHash(string levelPasswordHash)
	{
		this.levelPasswordHash = levelPasswordHash;
	}

	public string GetPasswordHash()
	{
		return levelPasswordHash;
	}

	public void SetLevelTaskText(string taskText)
	{
		levelTaskText = taskText;
	}

	public void AddElementsToDataList(Dictionary<ElementData, List<ElementInEditMode>> spawnedElementsInCreatorMode)
	{
		elementsData.Clear();

		foreach (List<ElementInEditMode> spawnedElementsList in spawnedElementsInCreatorMode.Values)
		{
			foreach (ElementInEditMode element in spawnedElementsList)
			{
				var elementTransform = element.transform;
				elementsData.Add(new LevelComponentData(
					element.elementData.name, 
					elementTransform.localPosition, 
					element.ElementState, 
					elementTransform.rotation, 
					elementTransform.localScale, 
					element.CorrectPressOrder, 
					element.isTaskTarget));
			}
		}




		elementsAmountsInElementsGroups = elementsData
			.Where(e => e.elementCorrectPressOrder > 0)
			.OrderBy(e => e.elementCorrectPressOrder)
			.GroupBy(e => e.elementCorrectPressOrder)
			.Select(g => g.Count())
			.ToArray();	
		elementsCountForCorrectResult = elementsData.Count(e => e.elementCorrectPressOrder != 0);
	}
	
	
	public void SetLevelTexture(Texture2D texture)
	{
		serializedLevelTexture = texture.EncodeToPNG();
		LevelTextureHolder.Instance.SetTexture(texture);
	}

	public string GetLevelTaskText()
	{
		return levelTaskText;
	}

	public Texture2D DeserializeSavedImage()
	{
		if (serializedLevelTexture.Length == 0) return null;
		Texture2D deserializedImage = new Texture2D(1, 1);
		deserializedImage.LoadImage(serializedLevelTexture);
		return deserializedImage;
	}
}
