using UnityEngine;
using System.Collections;

public class LoginApi : SingletonUnity<LoginApi>
{
    private string uri = "login";

    public bool IsDone = false;
    public bool IsLoginSucceess = false;
    public bool IsOffInternet = false;

    public LoginResp result = null;

    /// <summary>
    /// 根据token进行登录验证
    /// </summary>
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
            LoginJson loginJson = JsonTool.JsonToClass<LoginJson> (www.text);

            if(loginJson == null)
            {
                IsOffInternet = true;
            }
            else
            {
                Debug.Log (www.text);

                if(loginJson.status == Constant.Status_OK)
                {
                    result = loginJson.resp;
                    IsLoginSucceess = true;
                }

            }

            IsDone = true;
        }

    }

    public void Restart()
    {
        IsDone = false;
        IsLoginSucceess = false;
        IsOffInternet = false;
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