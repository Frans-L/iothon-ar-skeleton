using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum StartButtonState
{
    on = 1,
    off = 2
}

public class StartButton : MonoBehaviour
{

    public Text textComponent;


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        var state = Common.BodySkeletonVisualizer.trackingState;

        if (state == Common.TrackingState.off)
        {
            Common.BodySkeletonVisualizer.trackingState = Common.TrackingState.on;
            textComponent.text = "Stop";
        }
        else
        {
            Common.BodySkeletonVisualizer.trackingState = Common.TrackingState.off;
            textComponent.text = "Start";
            ClientConn.Send("end");
        }


    }
}
