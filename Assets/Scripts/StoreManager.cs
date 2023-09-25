using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{

	private static int Coins;
	public static string KEY_COINS = "STORECOINS";
	public static string Total_Spins = "TotalSpins";
	public static string Best_Score = "BestScore";
	public static string Video_Coins = "VideoCoins";
	public static string BG_Selected = "BG";
	public static string ShowHelp1 = "showHelp1";
	public static string ShowHelp2 = "showHelp2";
	public static string SelectedSpinner = "spinner";
	public static string joyStickType="joystickType";


	public static void AddCoins(int coinscount)
	{
		Coins = PlayerPrefs.GetInt (KEY_COINS,0);
		Coins = Coins + coinscount;
		PlayerPrefs.SetInt (KEY_COINS,Coins);
	}
	public static void ReduceCoins(int coinscount)
	{
		Coins = PlayerPrefs.GetInt (KEY_COINS,0);
		Coins = Coins - coinscount;
		PlayerPrefs.SetInt (KEY_COINS,Coins);
	}
	public static int GetCoins()
	{
		Coins = PlayerPrefs.GetInt (KEY_COINS,0);
		return Coins;
	}
}
