using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton {
	private static Singleton instance;

    public int[] TemperatureStorage { get; set; }
    public int[] LightStorage { get; set; }
    public int lastTemperaturePointer = 0;
    public int lastLightPointer = 0;

    private Singleton(){}

	public static Singleton GetInstance() {
		
			if (instance == null)
			{
				instance = new Singleton();
			}
			return instance;

	}


}
