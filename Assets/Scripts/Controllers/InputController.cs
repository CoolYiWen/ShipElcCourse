using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {

	private Output output = null;

	void Start () {
	
	}
	
	void Update () {
	
	}

	public void Calculate(Input input)
	{
		
		output = Algorithm.NeedCoefficientMethod (input);

		Entity entity = new Entity ();
		entity.id = input.id;
		entity.name = input.name;
		entity.num = input.num;
		entity.pmax = input.pmax;
		entity.p1 = input.p1;
		entity.e = input.e;
		entity.k21 = input.k21;
		entity.k01 = input.k01;
		entity.type1 = input.type1;
		entity.k22 = input.k22;
		entity.k02 = input.k02;
		entity.type2 = input.type2;
		entity.k23 = input.k23;
		entity.k03 = input.k03;
		entity.type3 = input.type3;
		entity.k24 = input.k24;
		entity.k04 = input.k04;
		entity.type4 = input.type4;

		entity.p4 = output.p4;
		entity.p5 = output.p5;
		entity.k1 = output.k1;
		entity.k31 = output.k31;
		entity.pn1 = output.pn1;
		entity.k32 = output.k32;
		entity.pn2 = output.pn2;
		entity.k33 = output.k33;
		entity.pn3 = output.pn3;
		entity.k34 = output.k34;
		entity.pn4 = output.pn4;

		entity.bRun = output.bRun;
		entity.bStartBack = output.bStartBack;
		entity.bWaterWork = output.bWaterWork;
		entity.bStop = output.bStop;

        if(BlackBoard.Instance.GetValue<string>(Constant.BB_Token, "") == "")
        {
            ViewManager.Instance.StartViewByPanelName (Constant.OffLineReportView);
        }
        else
        {
            ViewManager.Instance.StartViewByPanelName (Constant.ReportView);
        }
        ViewManager.Instance.CurrentView.GetComponent<ReportView> ().SetView (entity);
	}
}
