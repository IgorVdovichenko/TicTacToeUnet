using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TicTacToe;

public class PlayerController : NetworkBehaviour 
{
	#region Notifications
	public const string Started = "PlayerController.Start";
	public const string StartedLocal = "PlayerController.StartedLocal";
	public const string Destroyed = "PlayerController.Destroyed";
	public const string CoinToss = "PlayerController.CoinToss";
	public const string RequestMarkSquare = "PlayerController.RequestMarkSquare";
	#endregion

	#region Fields & Properties
	public int score;
	public Mark mark;
	#endregion

	#region NetworkBehaviour
	public override void OnStartClient ()
	{
		base.OnStartClient ();
		this.PostNotification(Started);
	}

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		this.PostNotification(StartedLocal);
	}

	void OnDestroy ()
	{
		this.PostNotification(Destroyed);
	}
	#endregion

	#region Networking
	[Command]
	public void CmdCoinToss ()
	{
		RpcCoinToss(Random.value < 0.5);
	}

	[ClientRpc]
	void RpcCoinToss (bool coinToss)
	{
		this.PostNotification(CoinToss, coinToss);
	}

	[Command]
	public void CmdMarkSquare (int index)
	{
		RpcMarkSquare(index);
	}

	[ClientRpc]
	void RpcMarkSquare (int index)
	{
		this.PostNotification(RequestMarkSquare, index);
	}
	#endregion
}