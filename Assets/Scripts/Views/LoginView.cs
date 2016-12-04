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

	void OnEnable()
	{
		Token.placeholder.color = new Color (0, 0, 0, 125);
	}

    public void OnLoginClick()
	{
		loginController.Login (Token.text);
	}

	public void WarnTokenError()
	{
		Token.text = "";
		Token.placeholder.color = new Color (255, 0, 0);
	}

}

