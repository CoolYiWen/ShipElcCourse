using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageView : MonoBehaviour
{
	public Text T_Message;

	public void SetMessage(string msg)
	{
		T_Message.text = msg;
	}

	public void CloseErrorView()
	{
		Destroy (gameObject);
	}

}

