using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserView : SingletonUnity<UserView>
{
	public Text UserName;
	public Text UserRightText;
    public GameObject Button_Back;
    public GameObject Profile;

    private Vector3 shortPos;
    private Vector3 longPos;

	private string user_name = "";
	public bool user_power = false;

	void OnEnable()
	{
		user_name = BlackBoard.Instance.GetValue<string> (Constant.BB_Name, "");
        user_power = BlackBoard.Instance.GetValue<bool> (Constant.BB_Power, false);

        if(user_name == "")
        {
            SetUserView (false);
        }
        else
        {
            SetUserView (true);
        }
	}

    void Awake()
    {
        longPos = Profile.transform.localPosition;
        shortPos = new Vector3 (longPos.x - 63.5f, longPos.y, 0);
    }

	void SetUserView(bool isOnline)
	{
		if(isOnline)
		{
			UserName.text = user_name;
			UserRightText.text = "注销";

		}
		else
		{
			UserName.text = "离线模式";
			UserRightText.text = "登陆";
		}

		UserRightText.GetComponent<Button> ().onClick.AddListener (delegate {
			CancelUser ();
		});
	} 

	public void CancelUser()
	{
		user_name = "";
		user_power = false;
		BlackBoard.Instance.SetValue(Constant.BB_Name, "");
		BlackBoard.Instance.SetValue(Constant.BB_Power, false);
		BlackBoard.Instance.SetValue (Constant.BB_Token, "");
		ViewManager.Instance.CloseUserView ();
		ViewManager.Instance.StartViewByPanelName (Constant.LoginPanel);
	}

    public void SetProfilePosition(bool isBackButtonShow)
    {
        if(isBackButtonShow)
        {
            Profile.transform.localPosition = longPos;
        }
        else
        {
            Profile.transform.localPosition = shortPos;
        }
    }

}

