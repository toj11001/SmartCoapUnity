using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton {
	private static Singleton instance;

    public int[] TemperatureStorage { get; set; }
    public int[] LightStorage { get; set; }
    public int LastTemperaturePointer { get; set; }
    public int LastLightPointer { get; set; }

    private Singleton(){}

	public static Singleton GetInstance() {
		
		if (instance == null)
		{
            instance = new Singleton
            {
                LastTemperaturePointer = 0,
                LastLightPointer = 0
            };

        }

		return instance;

	}


}
