using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    public Text token = null;

    private LoginController loginController = null;

    void Awake()
    {
        loginController = LoginController.Instance;
    }

    public void OnLoginClick()
    {
        loginController.Login (token.text);
    }

}

