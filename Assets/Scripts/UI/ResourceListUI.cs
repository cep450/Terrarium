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
	List<ResourceStockMarket> resourceItems;

	// Update is called once per frame
	public void Tick()
	{
		if (resourceItems.Count > 0)
		{

			//Resource list is parallel with Sim list of resources and ids.
			for (int i = 0; i < resourceItems.Count; i++)
			{
				resourceItems[i].text.text = Sim.resourceInfo[i].displayName + ": " + Tracker.resourcesNet[i].ToString("+0;-#") + "\n " + GlobalPool.resources[i] + "/" + Sim.resourceGlobalCaps[i]; //showing stockpile numbers for debug purposes feel free to delete


				if (Tracker.resourcesNet[i] == 0)
				{
					resourceItems[i].text.color = Globals.colorNeutral;
				}
				else
				{
					resourceItems[i].text.color = Tracker.resourcesNet[i] >= 0 ? Globals.colorPositive : Globals.colorNegative;
				}

				if (Sim.resourceGlobalCaps[i] != 0) // housing-like resources have a 0 cap, and will set slider to max
				{
					resourceItems[i].slider.value = (float)GlobalPool.resources[i] / (float)Sim.resourceGlobalCaps[i];
				}
				else
				{
					resourceItems[i].slider.value = 1;
				}


				//update satisfaction bar if applicable 
				if(resourceItems[i].needIndex > -1) {
					resourceItems[i].satisfactionSlider.value = AgentDirector.AverageSatisfactionNeedFloat(resourceItems[i].needIndex);
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
		resourceItems = new List<ResourceStockMarket>();
		for (int i = 0; i < Sim.resourceInfo.Length; i++)
		{
			ResourceStockMarket item = Instantiate(resourceItem, gameObject.GetComponentInChildren<VerticalLayoutGroup>().gameObject.transform).GetComponent<ResourceStockMarket>();
			item.SetResource(Sim.resourceInfo[i]);
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
