using UnityEngine;
using System.Collections;

public class LoginController : SingletonUnity<LoginController> {

	private LoginApi login = null;
	private string token;

	void Awake()
	{
		login = LoginApi.Instance;
	}

    public void Login(string token)
    {
		this.token = token;
        StartCoroutine(login.LoginPost(token)) ;  
    }

	void Update()
	{
		if(login.IsDone)
		{
			if(login.IsLoginSucceess)
			{
				BlackBoard.Instance.SetValue (Constant.BB_Name, login.result.name);
				BlackBoard.Instance.SetValue (Constant.BB_Power, login.result.identity);
				BlackBoard.Instance.SetValue (Constant.BB_Token, token);

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
				GameObject loginView = ViewManager.Instance.CurrentView;
				if(loginView.name == Constant.LoginPanel)
				{
					loginView.GetComponent<LoginView> ().WarnTokenError ();
				}
			}

			login.Restart ();
		}   
	}

}
