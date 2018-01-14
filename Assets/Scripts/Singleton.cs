using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton {
	private static Singleton instance;

	public float latitudeGps {get; set;}
	public float longitudeGps { get; set;}

	private Singleton(){}

	public static Singleton GetInstance() {
		
			if (instance == null)
			{
				instance = new Singleton();
			}
			return instance;

	}


}
