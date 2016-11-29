using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TaskView : MonoBehaviour {

	public Scrollbar scrollbar = null;
	public GameObject G_Item = null;
	public GameObject G_content = null;
	public GameObject G_ItemStart = null;
	private float itemHight;

	public Entity[] entities = null;
	public List<GameObject> ItemList = new List<GameObject> ();

	private Vector3 startPoint = new Vector3 (250, 0, 0);

	void Start()
	{
		itemHight = G_Item.GetComponent<RectTransform>().sizeDelta.y;
	}

	void OnItemClick(string sender)
	{
		Debug.Log (sender);
	}

	public void SetView(Entity[] entities)
	{
		this.entities = entities;
		G_content.GetComponent<RectTransform> ().sizeDelta += new Vector2 (0, (itemHight + 1) * entities.Length);

		ClearView ();

		for(int i = 0; i < entities.Length; i++)
		{
			GameObject item = Instantiate (G_Item) as GameObject;
			ItemList.Add (item);
			Entity entity = entities [i];
			item.GetComponent<ItemView> ().SetItemView (entity.id, entity.num, entity.name, entity.bRun, entity.bStartBack, entity.bWaterWork, entity.bStop);
			item.transform.transform.SetParent (G_content.transform);
			item.transform.localPosition = startPoint;
			startPoint += new Vector3 (0, -45, 0);

			item.GetComponent<Button> ().onClick.AddListener (delegate {
				this.OnItemClick(entity.name);
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
		startPoint = new Vector3 (250, 0, 0);
	}
}
