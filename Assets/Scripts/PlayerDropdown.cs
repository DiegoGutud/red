using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDropdown : MonoBehaviour
{
    public Dropdown dropdown;
    List<string> opciones = new List<string>() { "Player 1", "Player 2", "Player 3", "Player 4"};
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