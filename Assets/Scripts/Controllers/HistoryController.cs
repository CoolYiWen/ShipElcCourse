using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HistoryController : SingletonUnity<HistoryController>
{
	private HistoryApi historyApi = null;
	public TaskView taskView = null;

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
				taskView.SetView (entities);
			}

			if(historyApi.IsClearHistorySucceess)
			{
				GetHistory ();
				ViewManager.Instance.SetMessageView ("历史记录清除成功");
			}

			historyApi.Restart ();

		}    
 
	}

}

