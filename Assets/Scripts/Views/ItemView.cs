using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
	public Text Id;
	public Text Num;
	public Text Name;
	public Image Status_Run;
	public Image Status_Start;
	public Image Status_WaterWork;
	public Image Status_Stop;

	public void SetItemView(int id, int num, string name, bool status_Run, bool status_Start, bool status_WaterWork, bool status_Stop)
	{
		Id.text = id.ToString();
		Num.text = num.ToString ();
		Name.text = name;
		Status_Run.enabled = status_Run;
		Status_Start.enabled = status_Start;
		Status_WaterWork.enabled = status_WaterWork;
		Status_Stop.enabled = status_Stop;
	}

}

