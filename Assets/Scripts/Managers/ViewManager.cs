using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ViewManager : SingletonUnity<ViewManager> {

	public CanvasScaler mainScaler;
	private float deltaScreenHeight = 0f;

	public GameObject UserView;
	public GameObject Pf_MessageView;

	private GameObject rootView;
	[HideInInspector]
	public GameObject CurrentView;


	public List<string> BeforePanelNameList = new List<string> ();

	void Awake()
	{
		Screen.SetResolution (1024, 768, false);
	}

	void Start()
	{
		rootView = GameObject.Find ("View");

		StartViewByPanelName (Constant.LoginPanel);
	}

	void Update()
	{
		deltaScreenHeight = (float)Screen.height - 768.0f;

		if(deltaScreenHeight > 0)
		{
			mainScaler.scaleFactor = 1 + 0.001602f * deltaScreenHeight;
		}
		else
		{
			mainScaler.scaleFactor = 1 + 0.001094f * deltaScreenHeight;
		}
	}

	public void StartViewByPanelName(string panelName)
	{
		if(CurrentView != null)
		{
			if(BeforePanelNameList.Count > 0)
			{
				if(BeforePanelNameList[BeforePanelNameList.Count - 1] != panelName)
				{
					BeforePanelNameList.Add (CurrentView.name);
				}
			}
			else
			{
				BeforePanelNameList.Add (CurrentView.name);
			}

			CurrentView.SetActive (false);
		}

		CurrentView = rootView.transform.Find (panelName).gameObject;
		CurrentView.SetActive (true);
	}

	public void BackToBeforeView()
	{
		StartViewByPanelName (BeforePanelNameList [BeforePanelNameList.Count - 1]);
		BeforePanelNameList.RemoveAt (BeforePanelNameList.Count - 1);
	}

	public void StartUserView()
	{
		UserView.SetActive (true);
	}

	public void CloseUserView()
	{
		UserView.SetActive (false);
	}


	public void ShowMessageView(string msg)
	{
		GameObject view = Instantiate (Pf_MessageView) as GameObject;
		view.transform.transform.SetParent (rootView.transform);
		view.transform.localPosition = new Vector2 (0, 0);
		view.GetComponent<MessageView> ().SetMessage (msg);
	}
}
