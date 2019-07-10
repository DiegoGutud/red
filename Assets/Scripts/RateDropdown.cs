using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateDropdown : MonoBehaviour
{
    public Dropdown dropdown;
    List<string> opciones = new List<string>() {"9600","19200","14400","4800","2400",};
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
        
        dropdown.AddOptions(opciones);
    }
}
