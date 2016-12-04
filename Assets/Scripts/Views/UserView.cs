using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserView : SingletonUnity<UserView>
{
	public Text UserInfo;
	public Text UserCancel;

	private string user_name = "";
	public bool user_power = false;

	void OnEnable()
	{
		user_name = BlackBoard.Instance.GetValue<string> (Constant.BB_Name, "");
		user_power = BlackBoard.Instance.GetValue<bool> (Constant.BB_Power, false);

		SetUserView (true);
	}

	void SetUserView(bool isOnline)
	{
		if(isOnline)
		{
			UserInfo.text = user_name + "，欢迎使用！";
			UserCancel.text = "注销";
		}
		else
		{
			UserInfo.text = "离线模式";
			UserCancel.text = "登陆";
		}

		UserCancel.GetComponent<Button> ().onClick.AddListener (delegate {
			CancelUser ();
		});
	} 

	void CancelUser()
	{
		user_name = "";
		user_power = false;
		BlackBoard.Instance.SetValue(Constant.BB_Name, "");
		BlackBoard.Instance.SetValue(Constant.BB_Power, false);
		BlackBoard.Instance.SetValue (Constant.BB_Token, "");
		ViewManager.Instance.CloseUserView ();
		ViewManager.Instance.StartViewByPanelName (Constant.LoginPanel);
	}

}

