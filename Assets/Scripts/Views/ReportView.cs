using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReportView : MonoBehaviour {

	private ReportController reportController = null;

	public GameObject SelectView;

	public GameObject Content_Run;
	public GameObject Content_Start;
	public GameObject Content_Water;
	public GameObject Content_Stop;

	public Text T_No;
	public Text T_Name;
	public Text T_num;
	public Text T_pmax;
	public Text T_p1;
	public Text T_e;
	public Text T_k21;
	public Text T_k01;
	public Text T_type1;
	public Text T_k22;
	public Text T_k02;
	public Text T_type2;
	public Text T_k23;
	public Text T_k03;
	public Text T_type3;
	public Text T_k24;
	public Text T_k04;
	public Text T_type4;

	public Text T_p4;
	public Text T_p5;
	public Text T_k1;
	public Text T_k31;
	public Text T_pn1;
	public Text T_k32;
	public Text T_pn2;
	public Text T_k33;
	public Text T_pn3;
	public Text T_k34;
	public Text T_pn4;

	public Entity entity = new Entity();

	void Awake()
	{
		reportController = ReportController.Instance;
	}

    void OnEnable()
    {
        UserView.Instance.SetProfilePosition (true);
        UserView.Instance.Button_Back.SetActive (true);
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.RemoveAllListeners ();
        UserView.Instance.Button_Back.GetComponent<Button> ().onClick.AddListener (delegate {
            OnBackClick();
        });
    }

	public void SetView(Entity entity)
	{
		this.entity = entity;

		T_No.text = entity.id.ToString();
		T_Name.text = entity.name;
		T_num.text = entity.num.ToString();
		T_pmax.text = entity.pmax.ToString();
		T_p1.text = entity.p1.ToString();
		T_e.text = (entity.e * 100).ToString();
		T_k21.text = entity.k21.ToString();
		T_k01.text = entity.k01.ToString();
		T_k22.text = entity.k22.ToString();
		T_k02.text = entity.k02.ToString();
		T_k23.text = entity.k23.ToString();
		T_k03.text = entity.k03.ToString();
		T_k24.text = entity.k24.ToString();
		T_k04.text = entity.k04.ToString();

        if(entity.bRun)
        {
            Content_Run.SetActive (true);
        }
        else
        {
            Content_Run.SetActive (false);
        }
        if(entity.bStartBack)
        {
            Content_Start.SetActive (true);
        }
        else
        {
            Content_Start.SetActive (false);
        }
        if(entity.bWaterWork)
        {
            Content_Water.SetActive (true);
        }
        else
        {
            Content_Water.SetActive (false);
        }
        if(entity.bStop)
        {
            Content_Stop.SetActive (true);
        }
        else
        {
            Content_Stop.SetActive (false);
        }

		switch(entity.type1)
		{
			case 1:
				T_type1.text = "一类";
				break;
			case 2:
				T_type1.text = "二类";
				break;
			case 3:
				T_type1.text = "三类";
				break;
			default:
				break;
		}
		switch(entity.type2)
		{
		case 1:
			T_type2.text = "一类";
			break;
		case 2:
			T_type2.text = "二类";
			break;
		case 3:
			T_type2.text = "三类";
			break;
		default:
			break;
		}
		switch(entity.type3)
		{
		case 1:
			T_type3.text = "一类";
			break;
		case 2:
			T_type3.text = "二类";
			break;
		case 3:
			T_type3.text = "三类";
			break;
		default:
			break;
		}
		switch(entity.type4)
		{
		case 1:
			T_type4.text = "一类";
			break;
		case 2:
			T_type4.text = "二类";
			break;
		case 3:
			T_type4.text = "三类";
			break;
		default:
			break;
		}

		T_p4.text = entity.p4.ToString();
		T_p5.text = entity.p5.ToString();
		T_k1.text = entity.k1.ToString();
		T_k31.text = entity.k31.ToString();
		T_pn1.text = entity.pn1.ToString();
		T_k32.text = entity.k32.ToString();
		T_pn2.text = entity.pn2.ToString();
		T_k33.text = entity.k33.ToString();
		T_pn3.text = entity.pn3.ToString();
		T_k34.text = entity.k34.ToString();
		T_pn4.text = entity.pn4.ToString();


	}

	public void OnCoverOKClick()
	{
		SelectView.SetActive (false);
		reportController.CoverToSubmit ();
	}

	public void OnSelectNoClick()
	{
		SelectView.SetActive (false);
	}

    public void OnDeleteOKClick()
    {
        CollectionsController.Instance.RemoveEquipment (entity.name);
    }

	public void ShowSelectView()
	{
		SelectView.SetActive (true);
	}

	public void OnBackClick()
	{
		ViewManager.Instance.BackToBeforeView ();
	}

	public void OnSubmitClick()
	{
        if(UserView.Instance.user_power)
        {
            reportController.CheckOnServer (entity);
        }
        else
        {
            ViewManager.Instance.ShowMessageView("错误：您没有权限执行此操作");
        }
	}

	public void OnServerEditClick()
	{
		ViewManager.Instance.StartViewByPanelName (Constant.InputPanel);
		ViewManager.Instance.CurrentView.GetComponent<InputView> ().SetView (entity);
	}

	public void OnServerDeleteClick()
	{
        if(UserView.Instance.user_power)
        {
            ShowSelectView ();
        }
        else
        {
            ViewManager.Instance.ShowMessageView("错误：您没有权限执行此操作");
        }
	}
}
