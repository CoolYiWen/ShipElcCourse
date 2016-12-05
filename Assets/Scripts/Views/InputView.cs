using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputView : MonoBehaviour {

	public InputController inputController;

	public Toggle Toggle_Run;
	public Toggle Toggle_Start;
	public Toggle Toggle_Water;
	public Toggle Toggle_Stop;
	public GameObject Content_Run;
	public GameObject Content_Start;
	public GameObject Content_Water;
	public GameObject Content_Stop;

	public InputField IF_No;
	public InputField IF_Name;
	public InputField IF_num;
	public InputField IF_pmax;
	public InputField IF_p1;
	public InputField IF_e;
	public InputField IF_k21;
	public InputField IF_k01;
	public Dropdown Dd_type1;
	public InputField IF_k22;
	public InputField IF_k02;
	public Dropdown Dd_type2;
	public InputField IF_k23;
	public InputField IF_k03;
	public Dropdown Dd_type3;
	public InputField IF_k24;
	public InputField IF_k04;
	public Dropdown Dd_type4;

	private int No;
	private string Name;
	private int num;
	private float pmax;
	private float p1;
	private float e;
	private float k21;
	private float k01;
	private int type1;
	private float k22;
	private float k02;
	private int type2;
	private float k23;
	private float k03;
	private int type3;
	private float k24;
	private float k04;
	private int type4;

	private string errorMsg = "错误：参数填写不完整";

    void OnEnable()
    {
        if(BlackBoard.Instance.GetValue<string>(Constant.BB_Token, "") == "")
        {
            UserView.Instance.Button_Back.SetActive (false);
            UserView.Instance.SetProfilePosition (false);
        }
        else
        {
            UserView.Instance.SetProfilePosition (true);
            UserView.Instance.Button_Back.SetActive (true);
            UserView.Instance.Button_Back.GetComponent<Button> ().onClick.RemoveAllListeners ();
            UserView.Instance.Button_Back.GetComponent<Button> ().onClick.AddListener (delegate {
                OnBackClick();
            });
        }

    }

	public void OnCalculateClick()
	{
		//主属性
		if(IF_No.text == "" || IF_Name.text == "" || IF_num.text == "" || IF_pmax.text == "" || IF_p1.text == "" || IF_e.text == "")
		{
			ViewManager.Instance.ShowMessageView (errorMsg);
			return;
		}
		else
		{
			No = int.Parse(IF_No.text);
			Name = IF_Name.text;
			num = int.Parse(IF_num.text);
			pmax = float.Parse(IF_pmax.text);
			p1 = float.Parse(IF_p1.text);
			e = float.Parse(IF_e.text)/100;
		}
		//航行状态
		if(Toggle_Run.isOn)
		{
			if(IF_k21.text == "" || IF_k01.text == "")
			{
				ViewManager.Instance.ShowMessageView (errorMsg);
				return;
			}
			else
			{
				k21 = float.Parse(IF_k21.text);
				k01 = float.Parse(IF_k01.text);
				type1 = Dd_type1.value + 1;
			}
		}
		else
		{
			k21 = -1;
			k01 = -1;
			type1 = -1;
		}
		//进出港状态
		if(Toggle_Start.isOn)
		{
			if(IF_k22.text == "" || IF_k02.text == "")
			{
				ViewManager.Instance.ShowMessageView (errorMsg);
				return;
			}
			else
			{
				k22 = float.Parse(IF_k22.text);
				k02 = float.Parse(IF_k02.text);
				type2 = Dd_type2.value + 1;
			}
		}
		else
		{
			k22 = -1;
			k02 = -1;
			type2 = -1;
		}
		//水上作业
		if(Toggle_Water.isOn)
		{
			if(IF_k23.text == "" || IF_k03.text == "")
			{
				ViewManager.Instance.ShowMessageView (errorMsg);
				return;
			}
			else
			{
				k23 = float.Parse(IF_k23.text);
				k03 = float.Parse(IF_k03.text);
				type3 = Dd_type3.value + 1;
			}
		}
		else
		{
			k23 = -1;
			k03 = -1;
			type3 = -1;
		}
		//停泊状态
		if(Toggle_Stop.isOn)
		{
			if(IF_k24.text == "" || IF_k04.text == "")
			{
				ViewManager.Instance.ShowMessageView (errorMsg);
				return;
			}
			else
			{
				k24 = float.Parse(IF_k24.text);
				k04 = float.Parse(IF_k04.text);
				type4 = Dd_type4.value + 1;
			}
		}
		else
		{
			k24 = -1;
			k04 = -1;
			type4 = -1;
		}

		Input input = new Input (No, Name, num, pmax, p1, e,
			k21, k01, type1, 
			k22, k02, type2, 
			k23, k03, type3, 
			k24, k04, type4);

		inputController.Calculate (input);
	}

	public void SetView(Entity entity)
	{
		if(entity.bRun)
		{
			Toggle_Run.isOn = true;
			Content_Run.SetActive (true);
			IF_k21.text = entity.k21.ToString();
			IF_k01.text = entity.k01.ToString();
			Dd_type1.value = entity.type1 - 1;
		}
		else
		{
			Toggle_Run.isOn = false;
			Content_Run.SetActive (false);
			IF_k21.text = "";
			IF_k01.text = "";
		}

		if(entity.bStartBack)
		{
			Toggle_Start.isOn = true;
			Content_Start.SetActive (true);
			IF_k22.text = entity.k22.ToString();
			IF_k02.text = entity.k02.ToString();
			Dd_type2.value = entity.type2 - 1;
		}
		else
		{
			Toggle_Start.isOn = false;
			Content_Start.SetActive (false);
			IF_k22.text = "";
			IF_k02.text = "";
		}

		if(entity.bWaterWork)
		{
			Toggle_Water.isOn = true;
			Content_Water.SetActive (true);
			IF_k23.text = entity.k23.ToString();
			IF_k03.text = entity.k03.ToString();
			Dd_type3.value = entity.type3 - 1;
		}
		else
		{
			Toggle_Water.isOn = false;
			Content_Water.SetActive (false);
			IF_k23.text = "";
			IF_k03.text = "";
		}

		if(entity.bStop)
		{
			Toggle_Stop.isOn = true;
			Content_Stop.SetActive (true);
			IF_k24.text = entity.k24.ToString();
			IF_k04.text = entity.k04.ToString();
			Dd_type4.value = entity.type4 - 1;
		}
		else
		{
			Toggle_Stop.isOn = false;
			Content_Stop.SetActive (false);
			IF_k24.text = "";
			IF_k04.text = "";
		}

		IF_No.text = entity.id.ToString();
		IF_Name.text = entity.name;
		IF_num.text = entity.num.ToString();
		IF_pmax.text = entity.pmax.ToString();
		IF_p1.text = entity.p1.ToString();
		IF_e.text = (entity.e * 100).ToString();

	}

	public void ClearView()
	{
		Toggle_Run.isOn = true;
		Content_Run.SetActive (true);
		IF_k21.text = "";
		IF_k01.text = "";
		Toggle_Start.isOn = true;
		Content_Start.SetActive (true);
		IF_k22.text = "";
		IF_k02.text = "";
		Toggle_Water.isOn = true;
		Content_Water.SetActive (true);
		IF_k23.text = "";
		IF_k03.text = "";
		Toggle_Stop.isOn = true;
		Content_Stop.SetActive (true);
		IF_k24.text = "";
		IF_k04.text = "";

		IF_No.text = "";
		IF_Name.text = "";
		IF_num.text = "";
		IF_pmax.text = "";
		IF_p1.text = "";
		IF_e.text = "";
	}

	public void OnBackClick()
	{
		ViewManager.Instance.StartViewByPanelName (Constant.CollectionPanel);
		ViewManager.Instance.BeforePanelNameList.Clear ();
	}
		
	public void IsRunStatusChanged()
	{
		if(Toggle_Run.isOn)
		{
			Content_Run.SetActive (true);
		}
		else
		{
			Content_Run.SetActive (false);
		}
	}
	public void IsStartStatusChanged()
	{
		if(Toggle_Start.isOn)
		{
			Content_Start.SetActive (true);
		}
		else
		{
			Content_Start.SetActive (false);
		}
	}
	public void IsWaterStatusChanged()
	{
		if(Toggle_Water.isOn)
		{
			Content_Water.SetActive (true);
		}
		else
		{
			Content_Water.SetActive (false);
		}
	}
	public void IsStopStatusChanged()
	{
		if(Toggle_Stop.isOn)
		{
			Content_Stop.SetActive (true);
		}
		else
		{
			Content_Stop.SetActive (false);
		}
	}

}
  