using UnityEngine;
using System.Collections;


public class CollectionApi : SingletonUnity<CollectionApi>
{
    private string uri = "collection";


    public bool IsDone = false;
    public bool IsGetCollectionsSucceess = false;
    public bool IsRemoveCollectionSucceess = false;

    public GetCollectionsResp result = null;

    /// <summary>
    /// 获取收藏的设备
    /// </summary>
    public IEnumerator CollectionsGet()
    {
        WWW www = new WWW (Constant.BaseUrl + uri);
        yield return www;

        if(www.error != null)
        {
            Debug.Log (www.error);
            yield return null;
        }
        else
        {
            GetCollectionsJson getCollectionsJson = JsonTool.JsonToClass<GetCollectionsJson> (www.text);

            Debug.Log (www.text);

            if(getCollectionsJson.status == Constant.Status_OK)
            {
                result = getCollectionsJson.resp;
                IsGetCollectionsSucceess = true;
            }
            else
            {
                IsGetCollectionsSucceess = false;
            }

            IsDone = true;
        }

    }

    /// <summary>
    /// 根据name删除收藏的设备
    /// </summary>
    public IEnumerator RemoveCollectionsPost(string name)
    {
        WWWForm form = new WWWForm ();
        form.AddField ("name", name);

        WWW www = new WWW (Constant.BaseUrl + uri, form);
        yield return www;

        if(www.error != null)
        {
            Debug.Log (www.error);
            yield return null;
        }
        else
        {
            RemoveCollectionsJson removeCollectionsJson = JsonTool.JsonToClass<RemoveCollectionsJson> (www.text);

            Debug.Log (www.text);

            if(removeCollectionsJson.status == Constant.Status_OK)
            {
                IsRemoveCollectionSucceess = true;
            }
            else
            {
                IsRemoveCollectionSucceess = false;
            }

            IsDone = true;
        }

    }

    public void Restart()
    {
        IsDone = false;
        IsGetCollectionsSucceess = false;
        IsRemoveCollectionSucceess = false;
        result = null;
    }

}

public class GetCollectionsJson
{
    public string status = "";
    public GetCollectionsResp resp = null;

    public GetCollectionsJson()
    {}
}

public class GetCollectionsResp
{
    public Entity[] entities;

    public GetCollectionsResp()
    {}
}

public class RemoveCollectionsJson
{
    public string status = "";

    public RemoveCollectionsJson()
    {}
}
