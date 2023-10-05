using System;
using UnityEngine;

public class PlayerID : MonoBehaviour
{
    public static string PLAYER_UNIQ_ID = "PLAYER_UNIQ_ID";

    private void Awake()
    {
        if (PlayerPrefs.HasKey(PLAYER_UNIQ_ID) == false)
        {
            uint playerIDu = SmallXXHash.Seed(DateTime.Now.Minute).Eat(DateTime.Now.Second).Eat(DateTime.Now.Millisecond);
            int playerID = (int)playerIDu;
            PlayerPrefs.SetInt(PLAYER_UNIQ_ID, playerID);
        }
    }
}
