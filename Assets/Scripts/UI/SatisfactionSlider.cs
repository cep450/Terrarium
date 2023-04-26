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
	void Start()
	{
		//satisfactions = new List<Need>();
		satisfactionItems = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		/*if (satisfactions.Count>0)
		{
			for (int i = 0; i<satisfactionItems.Count;i++)
			{
				satisfactionItems[i].GetComponentInChildren<Slider>().value = satisfactions[i].value;
			}
		}*/
		PopulateList();
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
				}
			}
			else
			{
				foreach (GameObject g in satisfactionItems)
				{
					g.GetComponentInChildren<TextMeshProUGUI>().text = satisfactions[satisfactionItems.IndexOf(g)].needName;
					g.GetComponentInChildren<Slider>().value = satisfactions[satisfactionItems.IndexOf(g)].value;
					g.GetComponentInChildren<Image>().sprite = Sim.resourceInfo[Resource.IdByName(satisfactions[satisfactionItems.IndexOf(g)].needName)].icon;
				}
			}



		}

	}
}
