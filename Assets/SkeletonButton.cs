using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkeletonButton : MonoBehaviour
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
        var state = Common.BodySkeletonVisualizer.showSkeleton;

        if (state == Common.ShowSkeletonState.full)
        {
            Common.BodySkeletonVisualizer.showSkeleton = Common.ShowSkeletonState.hands;
            textComponent.text = "Hands";
        }
        else if (state == Common.ShowSkeletonState.hands)
        {
            Common.BodySkeletonVisualizer.showSkeleton = Common.ShowSkeletonState.off;
            textComponent.text = "Marker";
        }
        else
        {
            Common.BodySkeletonVisualizer.showSkeleton = Common.ShowSkeletonState.full;
            textComponent.text = "Body";
        }


    }
}
