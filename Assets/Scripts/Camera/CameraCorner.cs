using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class CameraCorner : MonoBehaviour {
    
    private Camera cam;
    /*[SerializeField]
    private Shader replacement;*/
    [SerializeField]
    private Camera minimapCam;
    [SerializeField]
    private Material myMaterial;

    private Canvas canvas;
    private LineRenderer lineT_RL;
    private LineRenderer lineB_RL;
    private LineRenderer lineR_BT;
    private LineRenderer lineL_BT;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Use this for initialization
    void Start () {
        // Lines Creation
        if (minimapCam != null)
        {
            //minimapCam.SetReplacementShader(replacement, null);

            GameObject goCanvas = new GameObject("Canvas");
            goCanvas.transform.SetParent(minimapCam.transform, false);
            goCanvas.layer = LayerMask.NameToLayer("minimapIcon");
            canvas = goCanvas.AddComponent<Canvas>();
            goCanvas.AddComponent<CanvasScaler>();
            goCanvas.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = minimapCam;
            canvas.planeDistance = 1f;
            
            GameObject goLineT_RL = new GameObject("CamProjectionLine-Top");
            goLineT_RL.layer = LayerMask.NameToLayer("minimapIcon");
            goLineT_RL.transform.SetParent(goCanvas.transform, false);
            GameObject goLineB_RL = new GameObject("CamProjectionLine-Bottom");
            goLineB_RL.layer = LayerMask.NameToLayer("minimapIcon");
            goLineB_RL.transform.SetParent(goCanvas.transform, false);
            GameObject goLineR_BT = new GameObject("CamProjectionLine-Right");
            goLineR_BT.layer = LayerMask.NameToLayer("minimapIcon");
            goLineR_BT.transform.SetParent(goCanvas.transform, false);
            GameObject goLineL_BT = new GameObject("CamProjectionLine-Left");
            goLineL_BT.layer = LayerMask.NameToLayer("minimapIcon");
            goLineL_BT.transform.SetParent(goCanvas.transform, false);

            lineT_RL = goLineT_RL.AddComponent<LineRenderer>();
            lineT_RL.material = myMaterial;
            lineT_RL.startColor = Color.white;
            lineT_RL.endColor = Color.white;
            lineT_RL.useWorldSpace = false;
            lineT_RL.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineT_RL.receiveShadows = false;

            lineB_RL = goLineB_RL.AddComponent<LineRenderer>();
            lineB_RL.material = myMaterial;
            lineB_RL.startColor = Color.white;
            lineB_RL.endColor = Color.white;
            lineB_RL.useWorldSpace = false;
            lineB_RL.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineB_RL.receiveShadows = false;

            lineR_BT = goLineR_BT.AddComponent<LineRenderer>();
            lineR_BT.material = myMaterial;
            lineR_BT.startColor = Color.white;
            lineR_BT.endColor = Color.white;
            lineR_BT.useWorldSpace = false;
            lineR_BT.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineR_BT.receiveShadows = false;

            lineL_BT = goLineL_BT.AddComponent<LineRenderer>();
            lineL_BT.material = myMaterial;
            lineL_BT.startColor = Color.white;
            lineL_BT.endColor = Color.white;
            lineL_BT.useWorldSpace = false;
            lineL_BT.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            lineL_BT.receiveShadows = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        ComputeCorners();
	}

    void ComputeCorners()
    {
        float camFar = cam.farClipPlane;
        float camFov = cam.fieldOfView;
        float camAspect = cam.aspect;

        float fovWHalf = camFov * 0.5f;

        Vector3 toRight = cam.transform.right * Mathf.Tan(fovWHalf * Mathf.Deg2Rad) * camAspect;
        Vector3 toTop = cam.transform.up * Mathf.Tan(fovWHalf * Mathf.Deg2Rad);

        Vector3 topLeft = (cam.transform.forward - toRight + toTop);
        float camScale = topLeft.magnitude * camFar;

        topLeft.Normalize();
        topLeft *= camScale;

        Vector3 topRight = (cam.transform.forward + toRight + toTop);
        topRight.Normalize();
        topRight *= camScale;

        Vector3 bottomRight = (cam.transform.forward + toRight - toTop);
        bottomRight.Normalize();
        bottomRight *= camScale;

        Vector3 bottomLeft = (cam.transform.forward - toRight - toTop);
        bottomLeft.Normalize();
        bottomLeft *= camScale;

        Debug.DrawRay(cam.transform.position, topLeft, Color.green);
        Debug.DrawRay(cam.transform.position, topRight, Color.blue);
        Debug.DrawRay(cam.transform.position, bottomLeft, Color.red);
        Debug.DrawRay(cam.transform.position, bottomRight, Color.cyan);

        //Test plane
        float distance;
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = new Ray(cam.transform.position, topLeft);
        if (plane.Raycast(ray, out distance))
        {
            topLeft = minimapCam.WorldToScreenPoint(ray.origin + ray.direction * distance) - new Vector3(canvas.pixelRect.width / 2f, canvas.pixelRect.height / 2f);
            Debug.DrawLine(ray.origin + ray.direction * distance, ray.origin + ray.direction * distance + Vector3.up * 20f, Color.yellow);
        }
        ray = new Ray(cam.transform.position, topRight);
        if (plane.Raycast(ray, out distance))
        {
            topRight = minimapCam.WorldToScreenPoint(ray.origin + ray.direction * distance) - new Vector3(canvas.pixelRect.width / 2f, canvas.pixelRect.height / 2f);
            Debug.DrawLine(ray.origin + ray.direction * distance, ray.origin + ray.direction * distance + Vector3.up * 20f, Color.yellow);
        }
        ray = new Ray(cam.transform.position, bottomLeft);
        if (plane.Raycast(ray, out distance))
        {
            bottomLeft = minimapCam.WorldToScreenPoint(ray.origin + ray.direction * distance) - new Vector3(canvas.pixelRect.width / 2f, canvas.pixelRect.height / 2f);
            Debug.DrawLine(ray.origin + ray.direction * distance, ray.origin + ray.direction * distance + Vector3.up * 20f, Color.yellow);
        }
        ray = new Ray(cam.transform.position, bottomRight);
        if (plane.Raycast(ray, out distance))
        {
            bottomRight = minimapCam.WorldToScreenPoint(ray.origin + ray.direction * distance) - new Vector3(canvas.pixelRect.width / 2f, canvas.pixelRect.height / 2f);
            Debug.DrawLine(ray.origin + ray.direction * distance, ray.origin + ray.direction * distance + Vector3.up * 20f, Color.yellow);
        }

        // Lines Update
        lineT_RL.SetPosition(0, topLeft);
        lineT_RL.SetPosition(1, topRight);

        lineB_RL.SetPosition(0, bottomLeft);
        lineB_RL.SetPosition(1, bottomRight);

        lineR_BT.SetPosition(0, bottomRight);
        lineR_BT.SetPosition(1, topRight);

        lineL_BT.SetPosition(0, bottomLeft);
        lineL_BT.SetPosition(1, topLeft);
    }


}
