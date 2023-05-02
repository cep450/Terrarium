using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SatisfactionSlider : MonoBehaviour
{
	[SerializeField] GameObject satisfactionItem;
	List<GameObject> satisfactionItems;
	List<Need> satisfactions;
	// Start is called before the first frame update
	void Awake()
	{
		satisfactionItems = new List<GameObject>();
	}

	// Update is called once per frame
	public void Tick()
	{
		
		satisfactions = AgentDirector.SatisfactionHorizontalList();
		foreach (GameObject g in satisfactionItems)
		{

			g.GetComponentInChildren<Slider>().value = satisfactions[satisfactionItems.IndexOf(g)].value;

		}
	}
	public void PopulateList()
	{

		if (AgentDirector.SatisfactionHorizontalList().Count > 0)
		{
			satisfactions = AgentDirector.SatisfactionHorizontalList();
			
			if (satisfactionItems.Count < satisfactions.Count)
			{
				foreach (Need n in satisfactions)
				{

					GameObject g = Instantiate(satisfactionItem, gameObject.GetComponentInChildren<HorizontalLayoutGroup>().gameObject.transform);
					satisfactionItems.Add(g);
					g.GetComponentInChildren<TextMeshProUGUI>().text = satisfactions[satisfactionItems.IndexOf(g)].needName;
					g.GetComponentInChildren<Image>().sprite = Sim.resourceInfo[Resource.IdByName(satisfactions[satisfactionItems.IndexOf(g)].needName)].icon;
					g.GetComponentInChildren<Image>().color = Sim.resourceInfo[Resource.IdByName(satisfactions[satisfactionItems.IndexOf(g)].needName)].color;
					g.GetComponentInChildren<TextMeshProUGUI>().color = Sim.resourceInfo[Resource.IdByName(satisfactions[satisfactionItems.IndexOf(g)].needName)].color;


				}
			}

		}

	}
}
