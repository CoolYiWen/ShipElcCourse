using UnityEngine;
using System.Collections;

public class ReportController : SingletonUnity<ReportController>
{
	public ReportView reportView = null;

	private CheckApi checkApi = null;
	private CollectApi collectApi = null;

	private int checkId;

	void Awake()
	{
		checkApi = CheckApi.Instance;
		collectApi = CollectApi.Instance;
	}

	public void CheckOnServer(Entity entity)
	{
		Debug.Log("check:reCon");
		StartCoroutine (checkApi.CheckPost (BlackBoard.Instance.GetValue(Constant.BB_Token, ""), entity));
	}

	public void SubmitEquipment(string cover = "0")
	{
		StartCoroutine (collectApi.CollectPost (checkId, cover));
	}

	void Update()
	{
		if(checkApi.IsDone)
		{
			if(checkApi.IsCheckSucceess)
			{
				checkId = checkApi.result.checkId;
				SubmitEquipment ();
			}

			checkApi.Restart ();
		}

		if(collectApi.IsDone)
		{
			if(collectApi.IsCollectSucceess)
			{
				ViewManager.Instance.SetMessageView ("设备提交成功！");
			}
			else
			{
				if (collectApi.result.exist == Constant.Exist)
				{
					reportView.ShowCoverView ();
				}
				else
				{
					ViewManager.Instance.SetMessageView ("提交失败！");
				}
			}
			collectApi.Restart ();
		}

	}

	public void CoverToSubmit()
	{
		SubmitEquipment (Constant.Submit_Cover);
	}



}

