using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour {

	public void Change2Light(){
		SceneManager.LoadScene ("LightChart");
	}
	public void Change2Temp(){
		SceneManager.LoadScene ("TempChart");
	}
	public void Change2Home(){
		SceneManager.LoadScene ("StartScreen");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
