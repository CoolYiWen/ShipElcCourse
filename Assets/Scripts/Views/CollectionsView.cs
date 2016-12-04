using UnityEngine;
using System.Collections;

public class CollectionsView : MonoBehaviour
{

	private CollectionsController collectionsController = null;

	void Awake()
	{
		collectionsController = CollectionsController.Instance;
	}

	void OnEnable()
	{
		OnRefeshClick ();
	}

	public void OnRefeshClick()
	{
		collectionsController.GetCollections ();
	}

	public void OnNewEQClick()
	{
		ViewManager.Instance.StartViewByPanelName (Constant.InputPanel);
		ViewManager.Instance.CurrentView.GetComponent<InputView> ().ClearView ();
	}

	public void OnHistoryClick()
	{
		ViewManager.Instance.StartViewByPanelName (Constant.HistoryPanel);
	}
}

