using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CollectionsView : MonoBehaviour
{
    public GameObject CollectionBg;
    public GameObject HistoryBtn;

    private float minBgHight = 324f;
    private float maxBgHight = 476f;
    private float bgWidth = 650f;

	private CollectionsController collectionsController = null;

    void Start()
    {
        bgWidth = CollectionBg.GetComponent<RectTransform>().sizeDelta.x;
    }

	void Awake()
	{
		collectionsController = CollectionsController.Instance;
	}

	void OnEnable()
	{
		OnRefeshClick ();

        if(BlackBoard.Instance.GetValue<string>("name", "") == "")
        {
            HistoryBtn.SetActive (false);
        }
        else
        {
            HistoryBtn.SetActive (true);
        }

        UserView.Instance.Button_Back.SetActive (false);
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.RemoveAllListeners ();
        UserView.Instance.SetProfilePosition (false);
	}

    public void SetCollectionBgView(int size)
    {
        CollectionBg.GetComponent<RectTransform> ().sizeDelta = new Vector2 (bgWidth, Mathf.Clamp (minBgHight + 76 * (size - 1), minBgHight, maxBgHight));
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
        if(CollectionsController.Instance.GetEntities() != null)
        {
            MotorOutputController.Instance.StartMotorOutputView (CollectionsController.Instance.GetEntities());
        }
    }
}

