using UnityEngine;
using System.Collections;

public class TimeTool : MonoBehaviour
{

	static public IEnumerator Delay(int s)
	{
		yield return new WaitForSeconds (s);
	}

}

