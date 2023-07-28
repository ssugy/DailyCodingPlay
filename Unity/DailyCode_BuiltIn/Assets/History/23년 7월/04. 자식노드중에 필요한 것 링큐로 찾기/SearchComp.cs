using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SearchComp : MonoBehaviour
{
    private TextMeshProUGUI test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.Find("test").GetComponent<TextMeshProUGUI>().text = "1234";
        }else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            List<TextMeshProUGUI> tmp = new List<TextMeshProUGUI>();
            transform.GetComponentsInChildren<TextMeshProUGUI>(true, tmp);

            test = tmp.Where(x => x.name.Equals("test")).First();
            test.text = "1212";
        }
    }
}
