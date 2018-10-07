using UnityEngine;
using System.Collections;

namespace TicTacToe
{
	public enum Mark
	{
		None,
		X,
		O
	}

	public class Game : MonoBehaviour
	{
		#region Notifications
		public const string DidBeginGameNotification = "Game.DidBeginGameNotification";
		public const string DidMarkSquareNotification = "Game.DidMarkSquareNotification";
		public const string DidChangeControlNotification = "Game.DidChangeControlNotification";
		public const string DidEndGameNotification = "Game.DidEndGameNotification";
		#endregion

		#region Fields & Properties
		public int boardSize = 3;
		public Mark control { get; private set; }
		public Mark winner { get; private set; }
		public Mark[] board { get; private set; }
		int[][] wins = new int[][]
		{
			// Horizontal Wins
			new int[] { 0, 1, 2 },
			new int[] { 3, 4, 5 },
			new int[] { 6, 7, 8 },

			// Vertical Wins
			new int[] { 0, 3, 6 },
			new int[] { 1, 4, 7 },
			new int[] { 2, 5, 8 },

			// Diagonal Wins
			new int[] { 0, 4, 8 },
			new int[] { 2, 4, 6 }
		};
		#endregion

		#region Monobehaviour
		private void Start()
		{
			board = new Mark[boardSize * boardSize];
		}
		#endregion

		//#region Constructor
		//public Game ()
		//{
		//	board = new Mark[boardSize*boardSize];
		//}
		//#endregion

		#region Public
		public void Reset ()
		{
			for (int i = 0; i < board.Length; ++i)
				board[i] = Mark.None;

			control = Mark.X;
			winner = Mark.None;

			this.PostNotification(DidBeginGameNotification);
		}

		public void Place (int index)
		{
			if (board[index] != Mark.None)
				return;

			board[index] = control;
			this.PostNotification(DidMarkSquareNotification, index);

			CheckForGameOver();
			if (control != Mark.None)
				ChangeTurn();
		}
		#endregion

		#region Private
		void ChangeTurn ()
		{
			control = (control == Mark.X) ? Mark.O : Mark.X;
			this.PostNotification(DidChangeControlNotification);
		}

		void CheckForGameOver ()
		{
			if (CheckForWin() || CheckForStalemate())
			{
				control = Mark.None;
				this.PostNotification(DidEndGameNotification);
			}
		}

		bool CheckForWin ()
		{
			for (int i = 0; i < 8; ++i)
			{
				Mark a = board[wins[i][0]];
				Mark b = board[wins[i][1]];
				Mark c = board[wins[i][2]];

				if (a == b && b == c && a != Mark.None)
				{
					winner = a;
					return true;
				}
			}
			return false;
		}

		bool CheckForStalemate ()
		{
			for (int i = 0; i < 9; ++i)
				if (board[i] == Mark.None)
					return false;
			return true;
		}
		#endregion
	}
}