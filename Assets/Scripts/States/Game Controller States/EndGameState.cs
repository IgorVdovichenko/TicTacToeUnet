using UnityEngine;
using System.Collections;
using TicTacToe;

public class EndGameState : BaseGameState 
{
	public override void Enter ()
	{
		base.Enter ();

		if (Game.winner == Mark.None)
		{
			GameStateLabel.text = "Draw!";
		}
		else if (Game.winner == LocalPlayer.mark)
		{
			GameStateLabel.text = "You Win!";
			LocalPlayer.score++;
		}
		else
		{
			GameStateLabel.text = "You Lose!";
			RemotePlayer.score++;
		}

		RefreshPlayerLabels();

		if (!LocalPlayer.isServer)
			StartCoroutine(Restart());
	}

	IEnumerator Restart ()
	{
		yield return new WaitForSeconds(3);
		LocalPlayer.CmdCoinToss();
	}
}