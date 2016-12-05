using UnityEngine;
using System.Collections;

public class LoginController : SingletonUnity<LoginController> {

	private LoginApi login = null;
	private string token = "";

    private float time = 0;
    private bool isBegin = true;

	void Awake()
	{
		login = LoginApi.Instance;
	}

    public void Login(string token)
    {
		this.token = token;
        StartCoroutine(login.LoginPost(token)) ;  
    }

    public void OffLineLogin()
    {
        ViewManager.Instance.StartUserView ();
        ViewManager.Instance.StartViewByPanelName (Constant.InputPanel);
    }

	void Update()
	{
		if(login.IsDone)
		{
            if(login.IsOffInternet)
            {
                if(!isBegin)
                {
                    UserView.Instance.CancelUser ();
                }

                token = "";
                ViewManager.Instance.ShowMessageView("错误：无法连接网络");

                login.Restart ();
                return;
            }

			if(login.IsLoginSucceess)
			{
                isBegin = false;
				BlackBoard.Instance.SetValue (Constant.BB_Name, login.result.name);
				BlackBoard.Instance.SetValue (Constant.BB_Token, token);
                BlackBoard.Instance.SetValue (Constant.BB_Power, login.result.identity == "1" ? true : false);

				GameObject loginView = ViewManager.Instance.CurrentView;
				if(loginView.name == Constant.LoginPanel)
				{
					loginView.GetComponent<LoginView> ().Token.text = "";
				}

				ViewManager.Instance.StartViewByPanelName(Constant.CollectionPanel);
				ViewManager.Instance.StartUserView ();
			}
			else
			{
                token = "";

				GameObject loginView = ViewManager.Instance.CurrentView;
				if(loginView.name == Constant.LoginPanel)
				{
					loginView.GetComponent<LoginView> ().WarnTokenError ();
				}
			}

			login.Restart ();
		} 

        //登录心跳包
        time += Time.fixedDeltaTime;
        if(time > 5)
        {
            time = 0f;

            if (token != "")
            {
                Login (token);
            }
        }

	}

}
