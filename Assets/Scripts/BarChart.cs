using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BarChart : MonoBehaviour {

	public Bar barPrefab;
	public int[] InputValues;
	List<Bar> bars = new List<Bar>(); //List to store the Bars
	public string[] labels;	//Input labels
	public Color[] colors; 	//Input Colors
	public string label;	//find out which scene runs
    private int[] testArray = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    float ChartHeight;

	void Start () {
		ChartHeight = Screen.height + GetComponent<RectTransform> ().sizeDelta.y;
        int p = 0;
        //Display the graph
        if (label == "T") //check whether we are in the temperature bar graph scene
        {
            p = Singleton.GetInstance().LastTemperaturePointer + 1;
            //DisplayGraph(Singleton.GetInstance().TemperatureStorage, p);
            DisplayGraph(testArray, p);
        }
		else
        {
            p = Singleton.GetInstance().LastLightPointer + 1;
            //DisplayGraph(Singleton.GetInstance().LightStorage, p);
            DisplayGraph(testArray, p);
        }

    }
//	private IEnumerator Update()
//    {
//		yield return new WaitForSeconds(6); //wait 6 sec   
//    }

    void DisplayGraph(int[] vals, int initialPointer){
        int p = 0;
		int maxVal = vals.Max ();

		for (int i = 0; i < vals.Length; i++) {
            p = (i + initialPointer) % vals.Length;
			Bar newBar = Instantiate (barPrefab) as Bar; 
			newBar.transform.SetParent (transform);

			//size bar
			RectTransform rt = newBar.bar.GetComponent<RectTransform> ();
			float normalizeVal = (float)vals [p] / (float)maxVal;
			rt.sizeDelta = new Vector2 (rt.sizeDelta.x, ChartHeight * normalizeVal);

			//set bar color
			Color col;
			col = colors[i % colors.Length];
			col[3] = 1.0F; // Making sure that alpha is at 1,
			newBar.bar.GetComponent<Image>().color = col;

            //setting the label
//            if (labels.Length <= i) { //check whether enough labels are available for each bar
//				newBar.label.text = "UNDEFINED";
//			} else {
//				newBar.label.text = labels[i];
//			}
			newBar.label.text	= label + (i+1).ToString();

             

            //set value label
            newBar.barValue.text = vals[i].ToString();
			if (rt.sizeDelta.y < 30f) {
				newBar.barValue.GetComponent<RectTransform>().pivot = new Vector2 (0.5f, 0f);
				newBar.barValue.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;  
			}
		}
	}
}
