using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HistoryView : MonoBehaviour
{
    public GameObject HistoryBg;

	private HistoryController historyController = null;

	private float minBgHight = 324f;
	private float maxBgHight = 476f;
	private float bgWidth = 650f;


	void Awake()
	{
		historyController = HistoryController.Instance;
	}

    void Start()
    {
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

