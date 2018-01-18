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

	private int p = 0;
	private int index = 0;
	private int maxVal;
	private bool T; 	//boolean to check whether temp or light sensor
    float ChartHeight;

	void Start () {
		ChartHeight = Screen.height + GetComponent<RectTransform> ().sizeDelta.y;
        //Display the graph
        if (label == "T") //check whether we are in the temperature bar graph scene
        {
			T = true;
            p = Singleton.GetInstance().LastTemperaturePointer + 1;
            DisplayGraph(Singleton.GetInstance().TemperatureStorage, p);
            // DisplayGraph(testArray, p);
        }
		else
        {
			T = false;
            p = Singleton.GetInstance().LastLightPointer + 1;
            DisplayGraph(Singleton.GetInstance().LightStorage, p);
            // DisplayGraph(testArray, p);
        } 
		StartCoroutine (waitTimeSec (6));
    }

	private IEnumerator waitTimeSec (int _s){
		float normalizeVal;
		int ptr;
		yield return new WaitForSeconds(_s); //wait _s sec

		//update the latest element
		//Checker whether temp or light sensor and normalize value
		if (T){
			ptr = Singleton.GetInstance ().LastTemperaturePointer;
			normalizeVal = (float)Singleton.GetInstance().TemperatureStorage[ptr] / (float)maxVal;
		}
		else{
			ptr = Singleton.GetInstance().LastLightPointer;
			normalizeVal = (float)Singleton.GetInstance().LightStorage[ptr] / (float)maxVal;
		}
		
		RectTransform rt = bars[ptr].bar.GetComponent<RectTransform> ();
		rt.sizeDelta = new Vector2 (rt.sizeDelta.x, ChartHeight * normalizeVal);
		//StartCoroutine(waitTimeSec(6));
	}

    void DisplayGraph(int[] vals, int initialPointer){
        int p = 0;
		maxVal = vals.Max ();

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
			newBar.label.text	= label + (p+1).ToString();

             

            //set value label
            newBar.barValue.text = vals[p].ToString();
			if (rt.sizeDelta.y < 30f) {
				newBar.barValue.GetComponent<RectTransform>().pivot = new Vector2 (0.5f, 0f);
				newBar.barValue.GetComponent<RectTransform> ().anchoredPosition = Vector2.zero;  
			}

			// add newBar to global List.
			bars.Add (newBar);
		}
	}
}
