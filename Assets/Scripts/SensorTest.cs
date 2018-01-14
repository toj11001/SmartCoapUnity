﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SensorTest : MonoBehaviour {

	//public GameObject label;

    [SerializeField]
	private CoapManager coapManager;

    // Use this for initialization
    void Start () {

        //Add code to start the Service:TestLight that will test light sensor value
        StartCoroutine("TestLight");
        StartCoroutine("TestTemperature");

    }


	public IEnumerator TestLight()
    {
        string uri;
        for (; ; )
        {
            uri = coapManager.GetUri("147.83.118.80", "light");
            coapManager.DoGet(uri);
            yield return new WaitForSeconds(6);
        }      
    }

    public IEnumerator TestTemperature()
    {
        //This method executes forever
        //Every X seconds send a Request to the coap server for the light service
        string uri;
        for (; ; )
        {
            uri = coapManager.GetUri("147.83.118.80", "temperature");
            coapManager.DoGet(uri);
            yield return new WaitForSeconds(6);
        }
    }

    //    public void ResponseReceived(object sender, ResponseReceivedEventArgs e)
    //{
    //    if (e.Resource == "light")
    //    {
    //        label_light.text = "Sensors " + e.Resource + " : " + e.Data;
    //    }
    //    else
    //    {
    //        label_temp.text = "Sensors " + e.Resource + " : " + e.Data;
    //    }

    //    Debug.Log("La respuesta es: " + e.Resource + " : " + e.Data);
    //}

}
