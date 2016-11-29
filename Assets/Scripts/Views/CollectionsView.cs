using UnityEngine;
using System.Collections;

public class CollectionsView : MonoBehaviour
{

	private CollectionsController collectionsController = null;

	void Awake()
	{
		collectionsController = CollectionsController.Instance;
	}

	public void OnRefeshClick()
	{
		collectionsController.GetCollections ();
	}
}

