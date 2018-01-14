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

    }



    public void OnButtonPressed()
    {
        DisableAllButtons();
        ChangeLedState("1");
    }

    public void OnButton2Pressed()
    {
        DisableAllButtons();
		string uri = coapManager.GetUri("147.83.118.80", "light");
        coapManager.DoGet(uri);
    }

    public void OnButton3Pressed()
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
        EnableAllButtons();
        //label.text = "UiManager "+e.Resource + " : " + e.Data;
        Debug.Log("La respuesta es: " + e.Resource + " : " + e.Data);

        if (e.Resource == "light")
        {
            label_light.text = e.Resource + " : " + e.Data;
        }
        else if (e.Resource == "temperature")
        {
            label_temp.text = e.Resource+ " : " + e.Data;
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
