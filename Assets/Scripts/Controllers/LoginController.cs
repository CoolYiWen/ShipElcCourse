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
        StartCoroutine (Delay (3));

        if(login.IsDone)
        {
            if(login.IsLoginSucceess)
            {
                Debug.Log (login.result.name);
                login.Restart ();
            }
            login.Restart ();
        }    

    }

    public IEnumerator Delay(int second)
    {
        yield return new WaitForSeconds (second);
    }


}
