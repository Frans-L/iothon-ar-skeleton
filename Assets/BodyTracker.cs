namespace BodyTrackerSpace
{
    using UnityEngine;
    using System.Collections.Generic;
    using HuaweiARUnitySDK;
    using Common;
    using HuaweiARInternal;
    public class BodyTracker : MonoBehaviour
    {
        [Tooltip("body prefabs")]
        public GameObject bodyPrefabs;

        private List<ARBody> newBodys = new List<ARBody>();

        private void Start()
        {
            DeviceChanged.OnDeviceChange += ARSession.SetDisplayGeometry;
        }

        public void Update()
        {
            _DrawBody();
        }

        private void _DrawBody()
        {
            newBodys.Clear();
            ARFrame.GetTrackables<ARBody>(newBodys, ARTrackableQueryFilter.NEW);

            if (newBodys.Count > 0)
            {
                var i = 0;
                GameObject planeObject = Instantiate(bodyPrefabs, Vector3.zero, Quaternion.identity, transform);
                planeObject.GetComponent<BodySkeletonVisualizer>().Initialize(newBodys[i]);
            }
        }

    }
}
