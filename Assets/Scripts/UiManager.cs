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
        label.text = "Scene Init";
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
        int i = Singleton.GetInstance().LastLightPointer;
        int j = Singleton.GetInstance().LastTemperaturePointer;
        //int arrayLength = Singleton.GetInstance().LightStorage.Length;
        string line = "";
        EnableAllButtons();
        //label.text = "UiManager "+e.Resource + " : " + e.Data;
        Debug.Log("La respuesta es: " + e.Resource + " : " + e.Data);

        if (e.Resource == "light")
        {
            label_light.text = e.Data;
            Singleton.GetInstance().LightStorage[i] = int.Parse(e.Data);
            i = (i + 1) % 10;
            Singleton.GetInstance().LastLightPointer = i;
            Debug.Log(Singleton.GetInstance().LastLightPointer);
            foreach (var item in Singleton.GetInstance().LightStorage)
            {
                line += item.ToString() + ", ";
            }
            label.text = line;
        }
        else if (e.Resource == "temperature")
        {
            label_temp.text = e.Data;
            Singleton.GetInstance().TemperatureStorage[j] = int.Parse(e.Data);
            j = (j + 1) % 10;
            Singleton.GetInstance().LastTemperaturePointer = j;
            Debug.Log(Singleton.GetInstance().LastTemperaturePointer);
            foreach (var item in Singleton.GetInstance().TemperatureStorage)
            {
                line += item.ToString() + ", ";
            }
            label.text = line;
        }
        else
        {
            label.text = "UiManager " + e.Resource + " : " + e.Data;
        }
        //label.text = System.DateTime.Now.ToString();
        
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
