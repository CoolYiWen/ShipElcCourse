using UnityEngine;
using System.Collections;
using System;

public class TimeTool : MonoBehaviour
{

	static public IEnumerator Delay(int s)
	{
		yield return new WaitForSeconds (s);
	}

	public static long ConvertDateTimeToInt(System.DateTime time)  
	{          
		System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));          
		long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
		return t;  
	}  

	public static DateTime ConvertStringToDateTime(long timeStamp)        
	{            
		DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));            
		long lTime = long.Parse(timeStamp.ToString() + "0000");            
		TimeSpan toNow = new TimeSpan(lTime); 
		return dtStart.Add(toNow);        
	} 

}

