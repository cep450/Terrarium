using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceListUI : MonoBehaviour
{
	public string ResourceListText;
	public string SatisfactionText;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		ResourceListText = ResourceStockpilesAndDeltas();
		SatisfactionText = AgentDirector.SatisfactionsList();
		gameObject.GetComponent<TextMeshProUGUI>().text = ResourceListText + "\n\n" + SatisfactionText;
	}

	string ResourceStockpilesAndDeltas()
	{
		string text = new string("Resource List: ");

		//if game is still loading, return 
		if(Tracker.resourcesNet == null || GlobalPool.resources == null) {
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
