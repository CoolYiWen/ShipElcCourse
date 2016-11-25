using UnityEngine;
using System.Collections;


public class CollectApi : SingletonUnity<CollectApi>
{
    private string uri = "collect";


    public bool IsDone = false;
    public bool IsCollectSucceess = false;

    public CollectResp result = null;

    /// <summary>
    /// 根据checkId，是否覆盖，进行收藏
    /// </summary>
    public IEnumerator CollectPost(int checkId, string cover)
    {
        WWWForm form = new WWWForm ();
        form.AddField ("checkId", checkId);
        form.AddField ("cover", cover);

        WWW www = new WWW (Constant.BaseUrl + uri, form);
        yield return www;

        if(www.error != null)
        {
            Debug.Log (www.error);
            yield return null;
        }
        else
        {
            CollectJson collectJson = JsonTool.JsonToClass<CollectJson> (www.text);

            Debug.Log (www.text);

            if(collectJson.status == Constant.Status_OK)
            {
                result = collectJson.resp;
                IsCollectSucceess = true;
            }
            else
            {
                IsCollectSucceess = false;
                result = collectJson.resp;
            }

            IsDone = true;
        }

    }

    public void Restart()
    {
        IsDone = false;
        IsCollectSucceess = false;
        result = null;
    }

}

public class CollectJson
{
    public string status = "";
    public CollectResp resp = null;

    public CollectJson()
    {}
}

public class CollectResp
{
    public string exist = "";

    public CollectResp()
    {}
}
