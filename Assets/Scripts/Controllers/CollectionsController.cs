using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CollectionsController : SingletonUnity<CollectionsController>
{
	private CollectionApi collection = null;
	public TaskView taskView = null;
    public CollectionsView collectionsView = null;

	private Entity[] entities = null;

	void Awake()
	{
		collection = CollectionApi.Instance;
	}

	public void GetCollections()
	{
		StartCoroutine(collection.CollectionsGet()) ;

	}

	public void RemoveEquipment(string name)
	{
		StartCoroutine(collection.RemoveCollectionsPost (name)) ;
	}

	void Update()
	{
		if(collection.IsGetDone)
		{
			if(collection.IsGetCollectionsSucceess)
			{
				entities = collection.result;
				Algorithm.QuickSortEntities (entities, 0, entities.Length - 1);

                collectionsView.SetCollectionBgView (entities.Length);
				taskView.SetView (entities);
			}
            else
            {
                ViewManager.Instance.ShowMessageView ("错误：设备获取失败");
            }

			collection.Restart ();

		}    

		if(collection.IsRemoveDone)
		{
			if(collection.IsRemoveCollectionSucceess)
			{
				ViewManager.Instance.StartViewByPanelName (Constant.CollectionPanel);
				ViewManager.Instance.ShowMessageView ("设备删除成功！");
			}
			else
			{
				ViewManager.Instance.ShowMessageView ("设备删除失败！");
			}

			collection.Restart ();
		}


	}

}

