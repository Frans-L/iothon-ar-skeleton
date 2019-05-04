using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IpTextUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = ClientConn.host;
    }

    public void OnInputEnd(InputField input)
    {
        Debug.Log("input ended");
        if(input.text.Length > 0)
        {
            ClientConn.Connect(input.text, ClientConn.port);
        }
       
    }
}
