using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This class manages al the User Interface actions.
/// <para>
/// Button events actions and label writing text.
/// </para>
/// </summary>
public class UiManager : MonoBehaviour
{
    [SerializeField]
    private CoapManager coapManager;

    [SerializeField]
    private Button[] buttons;

    [SerializeField]
    private Text label;

    [SerializeField]
    private Text label_temp;

    [SerializeField]
    private Text label_light;





    void Start()
	{
        coapManager.ResponseReceivedHandler += ResponseReceived;
        Singleton.GetInstance().LightStorage = new int[10];
        Singleton.GetInstance().TemperatureStorage = new int[10];
    }



    public void OnLedOnPressed()
    {
        DisableAllButtons();
        ChangeLedState("1");
    }

    public void OnChangeViewPressed()
    {
        DisableAllButtons();
        //string uri = coapManager.GetUri("147.83.118.80", "light");
        //      coapManager.DoGet(uri);
        EnableAllButtons();

    }

    public void OnLedOffPressed()
    {
        DisableAllButtons();
        ChangeLedState("0");
    }

    private void ChangeLedState(string state)
    {
		string uri = coapManager.GetUri("147.83.118.80", "led");
        coapManager.DoPut(uri, state);
    }

    public void ResponseReceived(object sender, ResponseReceivedEventArgs e)
    {
        int i = Singleton.GetInstance().lastLightPointer;
        int j = Singleton.GetInstance().lastTemperaturePointer;
        int arrayLength = Singleton.GetInstance().LightStorage.Length;
        EnableAllButtons();
        //label.text = "UiManager "+e.Resource + " : " + e.Data;
        Debug.Log("La respuesta es: " + e.Resource + " : " + e.Data);

        if (e.Resource == "light")
        {
            label_light.text = e.Resource + " : " + e.Data;
            Singleton.GetInstance().LightStorage[i] = int.Parse(e.Data);
            i = i++ % arrayLength;
            Singleton.GetInstance().lastLightPointer = i;
            Debug.Log(Singleton.GetInstance().lastLightPointer);
        }
        else if (e.Resource == "temperature")
        {
            label_temp.text = e.Resource+ " : " + e.Data;
            Singleton.GetInstance().TemperatureStorage[j] = int.Parse(e.Data);
            j = j++ % arrayLength;
            Singleton.GetInstance().lastTemperaturePointer = j;
            Debug.Log(Singleton.GetInstance().lastTemperaturePointer);
        }
        else
        {
            label.text = "UiManager " + e.Resource + " : " + e.Data;
        }
    }

    private void DisableAllButtons()
    {
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }
    }

    private void EnableAllButtons()
    {
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }
}
