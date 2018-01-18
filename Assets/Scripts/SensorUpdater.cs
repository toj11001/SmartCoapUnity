using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SensorUpdater : MonoBehaviour {

	//public GameObject label;

    [SerializeField]
	private CoapManager coapManager;

    // Use this for initialization
    void Start () {

        //Add code to start the Service:TestLight that will test light sensor value
        StartCoroutine("UpdateLight");
        StartCoroutine("UpdateTemperature");

    }


	public IEnumerator UpdateLight()
    {
        string uri;
        yield return new WaitForSeconds(0.5f); // Check if Responses are colliding -> CONFIRMED
        for (; ; )
        {
            uri = coapManager.GetUri("147.83.118.80", "light");
            coapManager.DoGet(uri);
            yield return new WaitForSeconds(2);
        }      
    }

    public IEnumerator UpdateTemperature()
    {
        //This method executes forever
        //Every X seconds send a Request to the coap server for the light service
        string uri;
        for (; ; )
        {
            uri = coapManager.GetUri("147.83.118.80", "temperature");
            coapManager.DoGet(uri);
            yield return new WaitForSeconds(2);
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
