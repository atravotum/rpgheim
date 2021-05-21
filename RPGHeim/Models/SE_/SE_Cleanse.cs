using System;
using System.Collections.Generic;
using UnityEngine;

public class SE_Cleanse : StatusEffect
{
	public Player myPlayer;
	public override void Setup(Character player)
	{
		if (player != null)
        {
			myPlayer = (Player)player;
			Cleanse();
		}
	}

	public override void Stop()
	{
		Cleanse();
	}

	private void Cleanse()
    {
		Console.print("Ok I'm cleansing");
		try
        {
			myPlayer.m_seman.RemoveStatusEffect("Burning");
			myPlayer.m_seman.RemoveStatusEffect("Frost");
			myPlayer.m_seman.RemoveStatusEffect("Poison");
			myPlayer.m_seman.RemoveStatusEffect("Smoke");
			myPlayer.m_seman.RemoveStatusEffect("Wet");
		}
		catch (Exception err)
        {
			Console.print(err);
        }
	}
}