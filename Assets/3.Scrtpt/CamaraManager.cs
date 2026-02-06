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
    public bool zoomArrived;
    
    #region 인벤토리
    //인벤토리 관련 카메라
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
    #endregion
    Vector3 dragStart;
    public void StartZoom()
    {
        Debug.Log("camera zoomStart");
        zoomArrived = false;
        isZooming = true;
        followCamera.gameObject.SetActive(false);
        zoomCamera.gameObject.SetActive(true);
        zoomCamera.Lens.OrthographicSize = 7;
        
        dragStart = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }
    public void EndZoom()
    {
        Debug.Log("camera zoomEnd");

        isZooming =false;
        followCamera.gameObject.SetActive(true);
        zoomCamera.gameObject.SetActive(false);
        
        Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        if(isZooming)
        {
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction =  dragStart - worldPos;

            Vector3 targetPos = zoomCamera.transform.position + direction;

            Vector3 playerPos = Player.Instance.transform.position;
            float maxDistance = 10f;
            Vector3 offset = targetPos - playerPos;

            if (offset.magnitude > maxDistance)
            {
                offset = offset.normalized * maxDistance;
                targetPos = playerPos + offset;
            }
            
            zoomCamera.transform.position = targetPos;
        }
    }

    
}
