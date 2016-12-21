using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionsView : MonoBehaviour
{
    public GameObject CollectionBg;

    private float minBgHight;
    private float maxBgHight = 534f;
    private float bgWidth;

	private CollectionsController collectionsController = null;

    void Start()
    {
        minBgHight = CollectionBg.GetComponent<RectTransform>().sizeDelta.y;
        bgWidth = CollectionBg.GetComponent<RectTransform>().sizeDelta.x;
    }

	void Awake()
	{
		collectionsController = CollectionsController.Instance;
	}

	void OnEnable()
	{
		OnRefeshClick ();

        UserView.Instance.Button_Back.SetActive (false);
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.RemoveAllListeners ();
        UserView.Instance.SetProfilePosition (false);
	}

    public void SetCollectionBgView(int size)
    {
        CollectionBg.GetComponent<RectTransform> ().sizeDelta = new Vector2 (bgWidth, Mathf.Clamp (minBgHight + 76 * (size - 2), minBgHight, maxBgHight));
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

    public void OnMotorOutputClick()
    {
        MotorOutputController.Instance.StartMotorOutputView (CollectionsController.Instance.GetEntities());
    }
}

