using UnityEngine;
using System.Collections;


public class HistoryApi : SingletonUnity<HistoryApi>
{
    private string uri = "history";


    public bool IsDone = false;
    public bool IsGetHistorySucceess = false;
    public bool IsClearHistorySucceess = false;

	public Entity[] result = null;

    /// <summary>
    /// 根据token获取历史记录
    /// </summary>
    public IEnumerator HistoryGet(string token)
    {
        WWW www = new WWW (Constant.BaseUrl + uri + "/get/" + token);
        yield return www;

        if(www.error != null)
        {
            Debug.Log (www.error);
            yield return null;
        }
        else
        {
            GetHistoryJson getHistoryJson = JsonTool.JsonToClass<GetHistoryJson> (www.text);

            Debug.Log (www.text);

            if(getHistoryJson.status == Constant.Status_OK)
            {
                result = getHistoryJson.resp;
                IsGetHistorySucceess = true;
            }
            else
            {
                IsGetHistorySucceess = false;
            }

            IsDone = true;
        }

    }
    /// <summary>
    /// 根据token清除历史记录
    /// </summary>
    public IEnumerator ClearHistoryPost(string token)
    {
        WWWForm form = new WWWForm ();
        form.AddField ("token", token);

        WWW www = new WWW (Constant.BaseUrl + uri + "/clear", form);
        yield return www;

        if(www.error != null)
        {
            Debug.Log (www.error);
            yield return null;
        }
        else
        {
            ClearHistoryJson clearHistoryJson = JsonTool.JsonToClass<ClearHistoryJson> (www.text);

            Debug.Log (www.text);

            if(clearHistoryJson.status == Constant.Status_OK)
            {
                IsClearHistorySucceess = true;
            }
            else
            {
                IsClearHistorySucceess = false;
            }

            IsDone = true;
        }

    }

    public void Restart()
    {
        IsDone = false;
        IsGetHistorySucceess = false;
        IsClearHistorySucceess = false;
        result = null;
    }

}

public class GetHistoryJson
{
    public string status = "";
	public Entity[] resp = null;

    public GetHistoryJson()
    {}
}

public class ClearHistoryJson
{
    public string status = "";

    public ClearHistoryJson()
    {}
}
