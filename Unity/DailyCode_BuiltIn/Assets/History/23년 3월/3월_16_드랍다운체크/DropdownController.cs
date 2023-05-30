using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropdownController : MonoBehaviour
{
    TMP_Dropdown drop;

    private void Start()
    {
        drop = GetComponent<TMP_Dropdown>();
        drop.options.Clear();

        for (int i = 0; i < 10; i++)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = $"{i}\n {(char)(i+97)}";
            drop.options.Add(option);
        }
        
    }
}
