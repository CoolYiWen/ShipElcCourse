using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaunchView : MonoBehaviour {

	private float time = 0f;

	void Start ()
	{
		float scale = ((float)Screen.width / Screen.height) / (1024f / 768f);
		if (scale < 1 && scale > 0)
			Camera.main.orthographicSize /= scale;
	}

	void Update()
	{
		time += Time.fixedDeltaTime;

		if(time > 3)
		{
			SceneManager.LoadScene ("Main");
		}
	}

}
