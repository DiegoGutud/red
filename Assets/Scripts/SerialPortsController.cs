using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System;

public class SerialPortsController : MonoBehaviour
{
    // Start is called before the first frame update
    private System.IO.Ports.SerialPort SP;
    private System.IO.Ports.SerialPort SP1;
    private string BufferIn;
    private string BufferOut;
    private bool PortOpen;

    void Start()
    {
        string[] puertos_disponibles = SerialPort.GetPortNames();
        Debug.Log("Puertos: /n");
        foreach (string puerto in puertos_disponibles) {
            Debug.Log("Puerto: " + puerto);
        }

        SP = new SerialPort();
        SP1 = new SerialPort();
        GameObject SendDataButton;
        SendDataButton = GameObject.Find("SendDataButton");
        SendDataButton.GetComponent<Button>().enabled = false;

        BufferIn = "";
        BufferOut = "";
        PortOpen = false;

        InputField ReceivedDataInputField;

        ReceivedDataInputField = GameObject.Find("ReceivedDataInputField").GetComponent<InputField>();
    
        ReceivedDataInputField.enabled = false;


        

    }

    // Update is called once per frame
    void Update()
    {
        UpdateDataReceived();
    }


    public void onClickConnectButton()
    {
        GameObject ConnectButton;
        GameObject SendDataButton;
        Dropdown RateDropdown;
        Dropdown PortsDropdown;

        
        int RateDropdownValue;
        int PortsDropdownValue;

        ConnectButton = GameObject.Find("ConnectButton");
        SendDataButton = GameObject.Find("SendDataButton");
        RateDropdown = GameObject.Find("RateDropdown").GetComponent<Dropdown>();
        PortsDropdown = GameObject.Find("PortsDropdown").GetComponent<Dropdown>();

        RateDropdownValue = RateDropdown.value;
        PortsDropdownValue = PortsDropdown.value;


        print("Onclick connect Button");
       Dropdown PlayerDropdown;
        int PlayerDropdownValue;


        PlayerDropdown = GameObject.Find("PlayerDropdown").GetComponent<Dropdown>();

        PlayerDropdownValue = PlayerDropdown.value;
        



        try
        {
            if (ConnectButton.GetComponentInChildren<Text>().text == "Conectar")
            {


                SP.BaudRate = int.Parse(RateDropdown.options[RateDropdownValue].text);
                SP.DataBits = 8;
                SP.Parity = Parity.None;
                SP.StopBits = StopBits.One;
                SP.Handshake = Handshake.None;
                SP.PortName = PortsDropdown.options[PortsDropdownValue].text;
            
                string TextSP1="";
                if (PlayerDropdown.options[PlayerDropdownValue].text=="Player 1")
                {
                    //PUERTOS DE LECTURA
                    TextSP1 = "COM3";
                }
                else if(PlayerDropdown.options[PlayerDropdownValue].text == "Player 2")
                {
                    TextSP1 = "COM5";
                }
                else if (PlayerDropdown.options[PlayerDropdownValue].text == "Player 3")
                {
                    TextSP1 = "COM7";
                }
                else if (PlayerDropdown.options[PlayerDropdownValue].text == "Player 4")
                {
                    TextSP1 = "COM9";
                }

                SP1.BaudRate = int.Parse(RateDropdown.options[RateDropdownValue].text);
                SP1.DataBits = 8;
                SP1.Parity = Parity.None;
                SP1.StopBits = StopBits.One;
                SP1.Handshake = Handshake.None;
                SP1.PortName = TextSP1;
                

                print("opcion seleccionada: " + PortsDropdown.options[PortsDropdownValue].text);

                try
                {
                    SP.Open();
                   SP1.Open();
                    ConnectButton.GetComponentInChildren<Text>().text = "Desconectar";
                    SendDataButton.GetComponent<Button>().enabled = true;
                    PortOpen = true;

                }
                catch (Exception e)
                {
                    print("Error de conexión: " + e.Message.ToString());
                }
            }
            else if (ConnectButton.GetComponentInChildren<Text>().text == "Desconectar")
            {
                SP1.Close();
                SP.Close();
                ConnectButton.GetComponentInChildren<Text>().text = "Conectar";
                SendDataButton.GetComponent<Button>().enabled = false;
                PortOpen = false;
            }
        }
        catch(Exception e)
        {
            print("Error: "+e.Message.ToString());
        }
    }

    public void onClickSendDataButton()
    {


        

        InputField SendDataInputField;
        Dropdown PlayerDropdown;

        int PlayerDropdownValue;

        SendDataInputField = GameObject.Find("SendDataInputField").GetComponent<InputField>();
        PlayerDropdown = GameObject.Find("PlayerDropdown").GetComponent<Dropdown>();

        PlayerDropdownValue = PlayerDropdown.value;

        try
        {
            SP.DiscardOutBuffer();
            BufferOut = PlayerDropdown.options[PlayerDropdownValue].text +": "+ SendDataInputField.text;
            print("mensaje: " +BufferOut);
            SP.Write(BufferOut);


        }
        catch (Exception e)
        {
            print("Error al enviar mensaje: " + e.Message.ToString());
        }

    }


    public void UpdateDataReceived()
    {
        if (PortOpen)
        {
            InputField ReceivedDataInputField;
            Text ReceivedDataInputFieldText;

            ReceivedDataInputField = GameObject.Find("ReceivedDataInputField").GetComponent<InputField>();
            ReceivedDataInputFieldText = GameObject.Find("ReceivedDataInputField").GetComponentInChildren<Text>();
            ReceivedDataInputField.enabled = false;

            string datos_recibidos = "";

            try
            {
                datos_recibidos = SP1.ReadExisting();
                //datos_recibidos = SP.ReadExisting();


            }
            catch (Exception e)
            {
                print("Error al recibir mensaje: " + e.Message.ToString());
            }

            if (datos_recibidos!="")
            {
                print("mensaje:Recibido " + datos_recibidos);
                ReceivedDataInputFieldText.text = datos_recibidos;
            }
           
        }
       



    }
}
