using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CollectionsController : SingletonUnity<CollectionsController>
{
	private CollectionApi collection = null;
	public TaskView taskView = null;

	private Entity[] entities = null;

	void Awake()
	{
		collection = CollectionApi.Instance;
	}

	public void GetCollections()
	{
		StartCoroutine(collection.CollectionsGet()) ;


	}

	void Update()
	{
		if(collection.IsDone)
		{
			if(collection.IsGetCollectionsSucceess)
			{
				entities = collection.result;
				collection.Restart ();
			}
			collection.Restart ();

			taskView.SetView (entities);
		}    


	}

}

