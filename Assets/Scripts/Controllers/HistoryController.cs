using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HistoryController : SingletonUnity<HistoryController>
{
	private HistoryApi historyApi = null;
	public TaskView taskView = null;
    public HistoryView historyView = null;

	private Entity[] entities = null;

	void Awake()
	{
		historyApi = HistoryApi.Instance;
	}

	public void GetHistory()
	{
		StartCoroutine(historyApi.HistoryGet(BlackBoard.Instance.GetValue<string>(Constant.BB_Token, "")));
	}

	public void ClearHistory()
	{
		StartCoroutine (historyApi.ClearHistoryPost (BlackBoard.Instance.GetValue<string> (Constant.BB_Token, "")));
	}

	void Update()
	{
		if(historyApi.IsDone)
		{
			if(historyApi.IsGetHistorySucceess)
			{
				entities = historyApi.result;
                historyView.SetCollectionBgView (entities.Length);
				taskView.SetView (entities);
			}
            else
            {
                ViewManager.Instance.ShowMessageView ("错误：历史记录获取失败");
            }

			historyApi.Restart ();

		}    

		if(historyApi.IsClearDone)
		{
			if(historyApi.IsClearHistorySucceess)
			{
				GetHistory ();
				ViewManager.Instance.ShowMessageView ("历史记录清除成功");
			}
			else
			{
				ViewManager.Instance.ShowMessageView ("错误：清除失败");
			}

			historyApi.Restart ();
		}
 
	}

}

