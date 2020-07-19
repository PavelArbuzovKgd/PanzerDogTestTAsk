using UnityEngine;

public class CameraController : MonoBehaviour, IOnUpdate,IOnStart//помещается на камеру
{
    [SerializeField] private Transform mTarget;
    [SerializeField] private Vector3 offset = new Vector3(-0.6f, -0.6f, -0.6f);//данные перенести в scriptobj
    [SerializeField] private float zoomSpeed = 4f;
    [SerializeField] private  float minZoom = 18f;
    [SerializeField] private float maxZoom = 28f;
    [SerializeField] private float pitch = 4f;
    private Transform mTransform;
    private float currentZoom = 10f;
    private float currentRot = 0f;
    private float prevMouseX;
    public Transform Target { set { mTarget = value; } }     
    
    public void OnStart()
    {
        Target = MainController.Instance.Character.Gfx.transform;
        mTransform = Camera.main.transform;
    }

    public void OnUpdate()
    {
        if (mTarget != null)
        {
            mTransform.position = mTarget.position - offset * currentZoom;
            mTransform.LookAt(mTarget.position + Vector3.up * pitch);
            mTransform.RotateAround(mTarget.position, Vector3.up, currentRot);
        }
     
        if (mTarget != null)
        {
            currentZoom -= Input.GetAxis(StringManager.InputMouseScrollWheel) * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            if (Input.GetMouseButton(2))
            {
                currentRot += Input.mousePosition.x - prevMouseX;
            }
        }
        prevMouseX = Input.mousePosition.x;
    }
}