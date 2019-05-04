
namespace Common
{
    using UnityEngine;
    using HuaweiARUnitySDK;
    using System.Collections.Generic;
    using HuaweiARInternal;
    using System.Text;

    public enum ShowSkeletonState
    {
        off = 0,
        hands = 1,
        full = 2
    }

    public enum TrackingState
    {
        off = 0,
        on = 1
    }

    public class BodySkeletonVisualizer : MonoBehaviour
    {

        public static ShowSkeletonState showSkeleton = ShowSkeletonState.off;
        public static TrackingState trackingState = TrackingState.off;

        private GameObject pointerObject;

        public Color bodyLineColor = new Color(0.95f, 0.95f, 0.95f, 0.75f);
        public float bodyLineWidth = 0.15f;
        public bool showBodyLine = true;

        public Color bodyJointColor = new Color(1, 1, 1, 0.2f);
        public float bodyJointRadius = 0.06f;
        public bool showBodyJoint = false;

        private ARBody m_body;
        StringBuilder printer = new StringBuilder();
        private static readonly int m_maxSkeletonCnt = 15;
        private static readonly int m_maxSkeletonConnectionCnt = 30;

        private GameObject[] m_skeletonPointObject = new GameObject[m_maxSkeletonCnt];
        private GameObject[] m_lines = new GameObject[m_maxSkeletonConnectionCnt];
        private LineRenderer[] m_skeletonConnectionRenderer = new LineRenderer[m_maxSkeletonConnectionCnt];
        private Material m_skeletonMaterial;

        private Camera m_skeletonCamera;
        private ARCameraConfig m_cameraConfig;

        private Vector2Int m_Imagesize = new Vector2Int(0, 0);
        private Vector2Int m_Texturesize = new Vector2Int(0, 0);

        private Dictionary<ARBody.SkeletonPointName, ARBody.SkeletonPointEntry> m_bodySkeletons = new Dictionary
            <ARBody.SkeletonPointName, ARBody.SkeletonPointEntry>();
        private List<KeyValuePair<ARBody.SkeletonPointName, ARBody.SkeletonPointName>> m_connections = new List
            <KeyValuePair<ARBody.SkeletonPointName, ARBody.SkeletonPointName>>();

        public void Initialize(ARBody body)
        {
            m_body = body;
            m_skeletonMaterial = new Material(Shader.Find("Diffuse"));
            m_cameraConfig = ARSession.GetCameraConfig();

            for (int i = 0; i < m_maxSkeletonCnt; i++)
            {
                m_skeletonPointObject[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_skeletonPointObject[i].transform.localScale = new Vector3(bodyJointRadius, bodyJointRadius, bodyJointRadius);
                m_skeletonPointObject[i].GetComponent<MeshRenderer>().material = m_skeletonMaterial;
                m_skeletonPointObject[i].GetComponent<MeshRenderer>().material.color = bodyJointColor;
                m_skeletonPointObject[i].SetActive(false);
            }

            for (int i = 0; i < m_maxSkeletonConnectionCnt; i++)
            {
                m_lines[i] = new GameObject("Lines");
                m_lines[i].transform.localScale = new Vector3(1f, 1f, 1f);
                m_lines[i].transform.position = new Vector3(0f, 0f, 0f);
                m_lines[i].transform.localPosition = new Vector3(0, 0, 0);
                m_lines[i].SetActive(false);

                m_skeletonConnectionRenderer[i] = m_lines[i].AddComponent<LineRenderer>();
                m_skeletonConnectionRenderer[i].material = new Material(Shader.Find("Sprites/Default"));

                m_skeletonConnectionRenderer[i].startColor = bodyLineColor;
                m_skeletonConnectionRenderer[i].endColor = bodyLineColor;
                m_skeletonConnectionRenderer[i].positionCount = 2;
                m_skeletonConnectionRenderer[i].startWidth = bodyLineWidth;
                m_skeletonConnectionRenderer[i].endWidth = bodyLineWidth;
                m_skeletonConnectionRenderer[i].numCapVertices = 6;
            }
            m_skeletonCamera = Camera.main;
            Update();
        }

        public void Start()
        {
            pointerObject = GameObject.Find("BodyPointer");
        }


        public void Update()
        {
            printer.Remove(0, printer.Length);
            printer.Append("BodyAction: " + m_body.GetBodyAction() + "\n");
            if (null == m_body)
            {
                return;
            }

            _DonotShowPointAndConnections();

            if (m_body.GetTrackingState() == ARTrackable.TrackingState.STOPPED)
            {
                Destroy(gameObject);
            }
            else if (m_body.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                _UpdateBody();
            }
        }


        private void _DonotShowPointAndConnections()
        {
            for (int i = 0; i < m_maxSkeletonCnt; i++)
            {
                m_skeletonPointObject[i].SetActive(false);
            }
            for (int i = 0; i < m_maxSkeletonConnectionCnt; i++)
            {
                m_lines[i].SetActive(false);
            }
        }

        private void _UpdateBody()
        {
            _Draw2DBody();
        }

        private void _Draw2DBody()
        {
            printer.Append("Skeleton Confidence: \n");
            m_body.GetSkeletons(m_bodySkeletons);

            string jsonData = "";

            foreach (var pair in m_bodySkeletons)
            {

                if (!pair.Value.Is2DValid)
                {
                    continue;
                }

                m_skeletonPointObject[(int)pair.Key].name = pair.Key.ToString();

                Vector3 glCoord = pair.Value.Coordinate2D;

                Vector3 worldCoord = new Vector3((glCoord.x + 1) / 2,
                    (glCoord.y + 1) / 2, 3);

                jsonData += "\"" + pair.Key.ToString() + "\": ";
                string vec = JsonUtility.ToJson(new Vector2(glCoord.x, glCoord.y));
                jsonData += vec + ",";

                m_skeletonPointObject[(int)pair.Key].transform.position = m_skeletonCamera.ViewportToWorldPoint(worldCoord);
                m_skeletonPointObject[(int)pair.Key].SetActive(showBodyJoint);


                if (pair.Key == ARBody.SkeletonPointName.Head_Top)
                {
                    Vector3 pointerCoord = m_skeletonCamera.ViewportToWorldPoint(worldCoord);
                    pointerCoord = new Vector3(pointerCoord.x, pointerCoord.y + 0.2f, pointerCoord.z);
                    pointerObject.transform.position = pointerCoord;

                    pointerObject.transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
                }
            }

            jsonData += "\"time\": " + Time.time;
            jsonData = "{" + jsonData + "}";

            if (trackingState == TrackingState.on)
            {
                ClientConn.Send(jsonData);
            }



            m_body.GetSkeletonConnection(m_connections);
            for (int i = 0; i < m_connections.Count; i++)
            {
                ARBody.SkeletonPointEntry skpStart;
                ARBody.SkeletonPointEntry skpEnd;
                if (!m_bodySkeletons.TryGetValue(m_connections[i].Key, out skpStart) ||
                    !m_bodySkeletons.TryGetValue(m_connections[i].Value, out skpEnd) ||
                    !skpStart.Is2DValid || !skpEnd.Is2DValid)
                {
                    continue;
                }
                Vector3 startGLScreenCoord = skpStart.Coordinate2D;
                Vector3 startScreenCoord = new Vector3((startGLScreenCoord.x + 1) / 2,
                    (startGLScreenCoord.y + 1) / 2, 3);

                Vector3 endGLScreenCoord = skpEnd.Coordinate2D;
                Vector3 endScreenCoord = new Vector3((endGLScreenCoord.x + 1) / 2,
                    (endGLScreenCoord.y + 1) / 2, 3);

                m_skeletonConnectionRenderer[i].SetPosition(0, m_skeletonCamera.ViewportToWorldPoint(startScreenCoord));
                m_skeletonConnectionRenderer[i].SetPosition(1, m_skeletonCamera.ViewportToWorldPoint(endScreenCoord));


                if (showSkeleton == ShowSkeletonState.full)
                {
                    m_skeletonConnectionRenderer[i].gameObject.SetActive(showBodyLine);
                }
                else if (showSkeleton == ShowSkeletonState.hands)
                {
                    ARBody.SkeletonPointName name = m_connections[i].Key;

                    bool show = name == ARBody.SkeletonPointName.Left_Elbow ||
                                name == ARBody.SkeletonPointName.Left_Wrist ||
                                name == ARBody.SkeletonPointName.Left_Shoulder ||
                                name == ARBody.SkeletonPointName.Right_Elbow ||
                                name == ARBody.SkeletonPointName.Right_Wrist ||
                                name == ARBody.SkeletonPointName.Right_Shoulder;

                    m_skeletonConnectionRenderer[i].gameObject.SetActive(show && showBodyLine);
                }

            }
        }

        void OnGUI()
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;
            bb.normal.textColor = new Color(1, 0, 0);
            bb.fontSize = 40;
            if (m_body.GetTrackingState() == ARTrackable.TrackingState.TRACKING)
            {
                pointerObject.SetActive(showSkeleton == ShowSkeletonState.off);

                // Live debug test
                // GUI.Label(new Rect(0, 0, 200, 200), printer.ToString(), bb);
            }
            else
            {
                pointerObject.SetActive(false);
            }
        }
    }
}
