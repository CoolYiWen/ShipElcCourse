using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TaskView : MonoBehaviour {

	public GameObject G_Item = null;
	public GameObject G_content = null;
	private float itemHight;
    private float minTaskHight;
    private float maxTaskHight;
    private float taskWidth;

	public Entity[] entities = null;
	public List<GameObject> ItemList = new List<GameObject> ();

	private Vector3 startPoint = new Vector3 (0, 0, 0);

	void Start()
	{
		itemHight = G_Item.GetComponent<RectTransform>().sizeDelta.y;
        minTaskHight = this.gameObject.GetComponent<RectTransform>().sizeDelta.y;
        maxTaskHight = minTaskHight + 2 * itemHight;
        taskWidth = this.gameObject.GetComponent<RectTransform>().sizeDelta.x;
	}

	void OnItemClick(Entity entity)
	{
		ViewManager.Instance.StartViewByPanelName (Constant.ServerReportPanel);
		ViewManager.Instance.CurrentView.GetComponent<ReportView> ().SetView (entity);
	}

	public void SetView(Entity[] entities)
	{
        ClearView ();

		this.entities = entities;
        this.gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (taskWidth, Mathf.Clamp (minTaskHight + itemHight * (entities.Length - 2), minTaskHight, maxTaskHight));

        G_content.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, itemHight * entities.Length);


		for(int i = 0; i < entities.Length; i++)
		{
			GameObject item = Instantiate (G_Item) as GameObject;
			ItemList.Add (item);
			Entity entity = entities [i];
			item.GetComponent<ItemView> ().SetItemView (entity.id, entity.num, entity.name, entity.bRun, entity.bStartBack, entity.bWaterWork, entity.bStop);
			item.transform.transform.SetParent (G_content.transform);
			item.transform.localPosition = startPoint;
            startPoint += new Vector3 (0, -itemHight, 0);

			item.GetComponent<Button> ().onClick.AddListener (delegate {
				this.OnItemClick(entity);
			});
		}

		G_content.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, itemHight * entities.Length);

	}
	
	private void ClearView()
	{
		if(ItemList == null)
		{
			return;
		}

		foreach(GameObject item  in ItemList)
		{
			Destroy (item);
		}

		ItemList.Clear ();
		startPoint = new Vector3 (0, 0, 0);
	}
}
