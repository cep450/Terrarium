using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceListUI : MonoBehaviour
{
	public string ResourceListText;
	public string SatisfactionText;
	[SerializeField] GameObject resourceItem;
	List<GameObject> resourceItems;
	// Start is called before the first frame update
	void Start()
	{
		resourceItems = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		ResourceListText = ResourceStockpilesAndDeltas();

		SatisfactionText = AgentDirector.SatisfactionsList();
		gameObject.GetComponent<TextMeshProUGUI>().text = SatisfactionText;
		if (resourceItems.Count > 0)
		{
			foreach (GameObject item in resourceItems)
			{
				item.GetComponentInChildren<TextMeshProUGUI>().text = Resource.NameById(resourceItems.IndexOf(item)) + ": " + Tracker.resourcesNet[resourceItems.IndexOf(item)].ToString("+0;-#");
				item.GetComponentInChildren<TextMeshProUGUI>().color = Tracker.resourcesNet[resourceItems.IndexOf(item)] >= 0 ? Color.green : Color.red;
				if (Sim.resourceGlobalCaps[resourceItems.IndexOf(item)] != 0) // housing-like resources have a 0 cap, and will set slider to max
				{
					item.GetComponentInChildren<Slider>().value = GlobalPool.resources[resourceItems.IndexOf(item)] / Sim.resourceGlobalCaps[resourceItems.IndexOf(item)];
				}
				else
				{
					item.GetComponentInChildren<Slider>().value = 1;
				}
			}
		}
	}
	public void Init()
	{
		PopulateList();
	}
	public void PopulateList()
	{
		for (int i = 0; i < Sim.resources.Length; i++)
		{
			GameObject item = Instantiate(resourceItem, gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform);
			resourceItems.Add(item);
		}
	}
	string ResourceStockpilesAndDeltas()
	{
		string text = new string("Resource List: ");

		//if game is still loading, return 
		if (Tracker.resourcesNet == null || GlobalPool.resources == null)
		{
			return text;
		}

		int[] resourceDeltas = Tracker.resourcesNet;
		int[] resourceStockpiles = GlobalPool.resources;

		for (int i = 0; i < resourceStockpiles.Length; i++)
		{
			string name = Resource.NameById(i);
			int stockpileValue = resourceStockpiles[Resource.IdByName(name)];
			//string stockpileText = stockpileValue.ToString("+0;-#");
			int deltaValue = resourceDeltas[Resource.IdByName(name)];
			string deltaText = deltaValue.ToString("+0;-#");
			text += "\n" + name + ": " + "\n" + "Stockpile: " + stockpileValue + " Units   Change: " + deltaText + " Units\n";

		}

		return text;
	}

}
