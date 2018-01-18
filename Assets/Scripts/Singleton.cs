using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton {
	private static Singleton instance;

    public int[] TemperatureStorage { get; set; }
    public int[] LightStorage { get; set; }
    public int LastTemperaturePointer { get; set; }
    public int LastLightPointer { get; set; }
    public bool isTemperatureUpdated { get; set; }
    public bool isLightUpdated { get; set; }

    private Singleton(){}

	public static Singleton GetInstance() {
		
		if (instance == null)
		{
            instance = new Singleton
            {
                TemperatureStorage = new int[10],
                LightStorage = new int[10],
                LastTemperaturePointer = 0,
                LastLightPointer = 0,
                isTemperatureUpdated = false,
                isLightUpdated = false
        };

        }

		return instance;

	}


}
