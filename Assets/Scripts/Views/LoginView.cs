using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
	public InputField Token;

    private LoginController loginController = null;

    void Awake()
    {
        loginController = LoginController.Instance;
    }

    public void OnLoginClick()
	{
		loginController.Login (Token.text);
	}

    public void OnOffLineLoginClick()
    {
        loginController.OffLineLogin ();
    }

	public void WarnTokenError()
	{
		Token.text = "";
        ViewManager.Instance.ShowMessageView ("口令输入错误");
	}

}

