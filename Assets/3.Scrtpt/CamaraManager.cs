using UnityEngine;
using Unity.Cinemachine;


public class CamaraManager : MonoBehaviour
{
    public static CamaraManager Instance;
    public Camera mainCamera;
    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;
    }

    public CinemachineCamera followCamera;
    public CinemachineCamera zoomCamera;
    public CinemachineCamera mainInventoryCamera;
    public bool isZooming;

    public void OpenMainInventoryCamera()
    {
        Debug.Log("MainInventoryCamera Start");
        followCamera.gameObject.SetActive(false);
        mainInventoryCamera.gameObject.SetActive(true);
    }
    public void CloseMainInventoryCamera()
    {
        Debug.Log("MainInventoryCamera End");
        followCamera.gameObject.SetActive(true);
        mainInventoryCamera.gameObject.SetActive(false);
    }
    public void StartZoom()
    {
        Debug.Log("camera zoomStart");

        isZooming =true;
        followCamera.gameObject.SetActive(false);
        zoomCamera.gameObject.SetActive(true);
        //zoomCamera.Lens.OrthographicSize = 7;
    }
    public void EndZoom()
    {
        Debug.Log("camera zoomEnd");

        isZooming =false;
        followCamera.gameObject.SetActive(true);
        zoomCamera.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(isZooming)
        {
            //Vector2 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            //zoomCamera.transform.position = worldPos;
        }
    }
}
