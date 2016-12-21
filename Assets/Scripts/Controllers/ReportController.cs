using UnityEngine;
using System.Collections;

public class ReportController : SingletonUnity<ReportController>
{
	public ReportView reportView = null;

	private CheckApi checkApi = null;
	private CollectApi collectApi = null;

	private int checkId;
    private Entity entity;

	void Awake()
	{
		checkApi = CheckApi.Instance;
		collectApi = CollectApi.Instance;
	}

	public void CheckOnServer(Entity entity)
	{
        this.entity = entity;

        if(BlackBoard.Instance.GetValue<string> (Constant.BB_Name, "") == "")
        {
            SaveOffLineCollection (entity);

        }
        else
        {
            StartCoroutine (checkApi.CheckPost (BlackBoard.Instance.GetValue(Constant.BB_Token, ""), entity));
        }
		
	}

    public void SaveOffLineCollection(Entity entity, bool cover = false)
    {
        if(cover)
        {
            DataManager.Instance.SaveCollection (entity);
            ViewManager.Instance.ShowMessageView ("设备提交成功！");
        }
        else
        {
            if(DataManager.Instance.IsCollectionExist (entity))
            {
                reportView.ShowSelectView ();
            }
            else
            {
                DataManager.Instance.SaveCollection (entity);
                ViewManager.Instance.ShowMessageView ("设备提交成功！");
            }
        }

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
				if(UserView.Instance.user_power)
				{
					checkId = checkApi.result.checkId;
					SubmitEquipment ();
				}
				else
				{
					ViewManager.Instance.ShowMessageView ("验算成功，但您没有权限提交设备");
				}
			}
            else
            {
                ViewManager.Instance.ShowMessageView ("错误：服务器同步验算失败");
            }

			checkApi.Restart ();
		}

		if(collectApi.IsDone)
		{
			if(collectApi.IsCollectSucceess)
			{
				ViewManager.Instance.ShowMessageView ("设备提交成功！");
			}
			else
			{
				if (collectApi.result.exist == Constant.Exist)
				{
					reportView.ShowSelectView ();
				}
				else
				{
					ViewManager.Instance.ShowMessageView ("错误：提交失败！");
				}
			}
			collectApi.Restart ();
		}

	}

	public void CoverToSubmit()
	{
        if(BlackBoard.Instance.GetValue<string> (Constant.BB_Name, "") == "")
        {
            SaveOffLineCollection (entity, true);

        }
        else
        {
            SubmitEquipment (Constant.Submit_Cover);
        }
		
	}



}

