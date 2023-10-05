using UnityEngine;

namespace _Scripts.LevelCreator
{
	public static class LevelSerializer
	{
		public static string SerializeLevel(LevelSO levelSO)
		{
			return JsonUtility.ToJson(levelSO);
		}
	}
}