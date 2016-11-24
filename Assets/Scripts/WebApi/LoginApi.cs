using UnityEngine;
using System.Collections;

using JsonFx.Json;

public class LoginApi : SingletonUnity<LoginApi>
{
    private string uri = "login";

    public bool IsDone = false;
    public bool IsLoginSucceess = false;

    public LoginResp result = null;

    public IEnumerator LoginPost(string token)
    {
        WWWForm form = new WWWForm ();
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
            LoginJson loginJson = ReadFileTool.JsonToClass<LoginJson> (www.text);

            Debug.Log (www.text);

            if(loginJson.status == "1")
            {
                result = loginJson.resp;
                Debug.Log (result.name);
                IsLoginSucceess = true;
            }
            else
            {
                IsLoginSucceess = false;
            }

            IsDone = true;
        }

    }

    public void Restart()
    {
        IsDone = false;
        IsLoginSucceess = false;
        result = null;
    }

}

public class LoginJson
{
    public string status = "";
    public LoginResp resp = null;

    public LoginJson()
    {}
}

public class LoginResp
{
    public string name = "";
    public string identity = "";

    public LoginResp()
    {}
}