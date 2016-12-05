using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HistoryView : MonoBehaviour
{
    public GameObject HistoryBg;

	private HistoryController historyController = null;

    private float minBgHight;
    private float maxBgHight = 534f;
    private float bgWidth;


	void Awake()
	{
		historyController = HistoryController.Instance;
	}

    void Start()
    {
        minBgHight = HistoryBg.GetComponent<RectTransform>().sizeDelta.y;
        bgWidth = HistoryBg.GetComponent<RectTransform>().sizeDelta.x;
    }

	void OnEnable()
	{
		OnRefeshClick ();

        UserView.Instance.SetProfilePosition (true);
        UserView.Instance.Button_Back.SetActive (true);
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.RemoveAllListeners ();
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.AddListener (delegate {
            OnBackClick();
        });
	}

    public void SetCollectionBgView(int size)
    {
        HistoryBg.GetComponent<RectTransform> ().sizeDelta = new Vector2 (bgWidth, Mathf.Clamp (minBgHight + 76 * (size - 2), minBgHight, maxBgHight));
    }

	public void OnRefeshClick()
	{
		historyController.GetHistory ();
	}

    private void OnBackClick()
	{
		ViewManager.Instance.BackToBeforeView ();
	}

	public void OnClearClick()
	{
		historyController.ClearHistory ();
	}
}

