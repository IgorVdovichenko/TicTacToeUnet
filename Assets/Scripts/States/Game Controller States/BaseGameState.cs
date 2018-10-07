using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TicTacToe;

public abstract class BaseGameState : State 
{
	public GameController owner;

	public Board Board { get { return owner.board; }}
	public Text LocalPlayerLabel { get { return owner.localPlayerLabel; }}
	public Text RemotePlayerLabel { get { return owner.remotePlayerLabel; }}
	public Text GameStateLabel { get { return owner.gameStateLabel; }}
	public Game Game { get { return owner.game; }}
	public PlayerController LocalPlayer { get { return owner.matchController.localPlayer; }}
	public PlayerController RemotePlayer { get { return owner.matchController.remotePlayer; }}

	protected virtual void Awake ()
	{
		owner = GetComponent<GameController>();
	}

	protected void RefreshPlayerLabels ()
	{
		LocalPlayerLabel.text = string.Format("You: {0}\nWins: {1}", LocalPlayer.mark, LocalPlayer.score);
		RemotePlayerLabel.text = string.Format("Opponent: {0}\nWins: {1}", RemotePlayer.mark, RemotePlayer.score);
	}
}