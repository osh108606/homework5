using UnityEngine;
using Unity.Cinemachine;

public class CamaraManager : MonoBehaviour
{
    public static CamaraManager Instance;
    public Camera mainCamear;
    private void Awake()
    {
        Instance = this;
        mainCamear = Camera.main;
    }

    public CinemachineCamera followCamera;
    public CinemachineCamera zoomCamera;
    public CinemachineCamera mainInentoryCamera;
    public bool isZooming;

    public void OpenMainIventoryCamera()
    {
        Debug.Log("MainIventoryCarmera Start");
        followCamera.gameObject.SetActive(false);
        mainInentoryCamera.gameObject.SetActive(true);
    }
    public void CloseMainIventoryCamera()
    {
        Debug.Log("MainIventoryCarmera End");
        followCamera.gameObject.SetActive(true);
        mainInentoryCamera.gameObject.SetActive(false);
    }
    public void StartZoom()
    {
        Debug.Log("carmera zoomStart");

        isZooming =true;
        followCamera.gameObject.SetActive(false);
        zoomCamera.gameObject.SetActive(true);
        //zoomCamera.Lens.OrthographicSize = 7;
    }
    public void EndZoom()
    {
        Debug.Log("carmera zoomEnd");

        isZooming =false;
        followCamera.gameObject.SetActive(true);
        zoomCamera.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(isZooming)
        {
            //Vector2 worldPos = mainCamear.ScreenToWorldPoint(Input.mousePosition);
            //zoomCamera.transform.position = worldPos;
        }
    }
}
