using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class PortsDropdown: MonoBehaviour
{
    public Dropdown dropdown;
    List<string> opciones = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        LlenarOpciones();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LlenarOpciones()
    {
        GameObject ConnectButton;
        ConnectButton = GameObject.Find("ConnectButton");
        string[] puertos_disponibles = SerialPort.GetPortNames();
        print("Onclick");
        if (puertos_disponibles.Length != 0)
        {
            print("hello");
            foreach (string puerto in puertos_disponibles)
            {
                opciones.Add(puerto);
            }
            ConnectButton.GetComponent<Button>().enabled = true;
            
            
        }
        else
        {
            opciones.Add("No hay puertos disponibles");
            ConnectButton.GetComponent<Button>().enabled = false;
        }
       
        
        dropdown.AddOptions(opciones);
    }
}
