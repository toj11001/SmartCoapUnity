using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BarChart : MonoBehaviour {

	public Bar barPrefab;
	public int[] InputValues;
	List<Bar> bars = new List<Bar>(); //List to store the Bars
	public string[] labels; //Input labels

	float ChartHeight;

	void Start () {
		ChartHeight = Screen.height + GetComponent<RectTransform> ().sizeDelta.y;

		//Display the graph
		DisplayGraph(InputValues);
	
	}

	void DisplayGraph(int[] vals){
		int maxVal = vals.Max ();

		for (int i = 0; i < vals.Length; i++) {
			Bar newBar = Instantiate (barPrefab) as Bar; 
			newBar.transform.SetParent (transform);
			RectTransform rt = newBar.bar.GetComponent<RectTransform> ();
			float normalizeVal = (float)vals [i] / (float)maxVal;
			rt.sizeDelta = new Vector2 (rt.sizeDelta.x, ChartHeight * normalizeVal);

			if (labels.Length <= i) { //check whether enough labels are available for each bar
				newBar.label.text = "UNDEFINED";
			} else {
				newBar.label.text = labels[i];
			}
//			newBar.barValue.text = ;
		}
	}
}
