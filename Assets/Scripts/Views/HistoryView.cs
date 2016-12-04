using UnityEngine;
using System.Collections;

public class HistoryView : MonoBehaviour
{
	private HistoryController historyController = null;

	void Awake()
	{
		historyController = HistoryController.Instance;
	}

	void OnEnable()
	{
		OnRefeshClick ();
	}

	public void OnRefeshClick()
	{
		historyController.GetHistory ();
	}

	public void OnBackClick()
	{
		ViewManager.Instance.BackToBeforeView ();
	}

	public void OnClearClick()
	{
		historyController.ClearHistory ();
	}
}

