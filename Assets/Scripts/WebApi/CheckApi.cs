using UnityEngine;
using System.Collections;
using System.Text;

public class CheckApi : SingletonUnity<CheckApi>
{
    private string uri = "check";

    public bool IsDone = false;
    public bool IsCheckSucceess = false;

    public CheckResp result = null;

    /// <summary>
    /// 传入全部参数进行验算
    /// </summary>
    public IEnumerator CheckPost(string token, Entity entity)
    {
		Debug.Log("check:api");
        string jsonData = JsonTool.ClassToJson<Entity> (entity);
		WWWForm form = new WWWForm ();
		form.AddField ("data", jsonData);
		form.AddField ("token", token);
		WWW www = new WWW (Constant.BaseUrl + uri, form);
        yield return www;

        if(www.error != null)
        {
            Debug.Log (www.error);
            yield return null;
        }
        else
        {
            CheckJson checkJson = JsonTool.JsonToClass<CheckJson> (www.text);

            if(checkJson == null)
            {
                yield break;
            }

            Debug.Log (www.text);

            if(checkJson.status == Constant.Status_OK)
            {
                result = checkJson.resp;
                Debug.Log (result.checkId);
                IsCheckSucceess = true;
            }

            IsDone = true;
        }

    }

    public void Restart()
    {
        IsDone = false;
        IsCheckSucceess = false;
        result = null;
    }

}

public class CheckJson
{
    public string status = "";
    public CheckResp resp = null;

    public CheckJson()
    {}
}

public class CheckResp
{
    public int checkId;

    public CheckResp()
    {}
}
