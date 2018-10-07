using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using TicTacToe;

public class Board : MonoBehaviour, IPointerClickHandler
{
	#region Notifications
	public const string SquareClickedNotification = "Board.SquareClickedNotification";
	#endregion

	#region Fields
	[SerializeField] SetPooler xPooler;
	[SerializeField] SetPooler oPooler;
	#endregion

	#region Public
	public void Show (int index, Mark mark)
	{
		SetPooler pooler = mark == Mark.X ? xPooler : oPooler;
		GameObject instance = pooler.Dequeue().gameObject;

		int x = index % 3;
		int y = index / 3;

		instance.transform.localPosition = new Vector3( x + 0.5f, y + 0.5f, 0);
		instance.SetActive(true);
	}

	public void Clear ()
	{
		xPooler.EnqueueAll();
		oPooler.EnqueueAll();
	}
	#endregion

	#region Event Handlers
	void IPointerClickHandler.OnPointerClick (PointerEventData eventData)
	{
		Vector3 pos = eventData.pointerCurrentRaycast.worldPosition;
		int x = Mathf.FloorToInt(pos.x);
		int y = Mathf.FloorToInt(pos.y);

		if (x < 0 || y < 0 || x > 2 || y > 2)
			return;

		int index = y * 3 + x;
		this.PostNotification(SquareClickedNotification, index);
	}
	#endregion
}