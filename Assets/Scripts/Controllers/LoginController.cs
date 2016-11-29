using UnityEngine;
using System.Collections;

public class LoginController : SingletonUnity<LoginController> {

    private LoginApi login = null;

    void Awake()
    {
        login = LoginApi.Instance;
    }

    public void Login(string token)
    {
        StartCoroutine(login.LoginPost(token)) ;

		StartCoroutine (TimeTool.Delay (1));

        if(login.IsDone)
        {
            if(login.IsLoginSucceess)
            {
                login.Restart ();
            }
            login.Restart ();
        }    

    }

}
