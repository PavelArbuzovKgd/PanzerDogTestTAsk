using UnityEngine;

public class CameraController : MonoBehaviour, IOnUpdate,IOnStart//помещается на камеру
{
    #region Fields

    [SerializeField] private Transform mTarget;//цель камеры
    [SerializeField] private Vector3 offset = new Vector3(-0.6f, -0.6f, -0.6f);//данные перенести в scriptobj
    [SerializeField] private float zoomSpeed = 4f;//скорость зума
    [SerializeField] private  float minZoom = 18f;//мин близкий зум
    [SerializeField] private float maxZoom = 28f;//макс дальность зума
    [SerializeField] private float pitch = 4f;//наклон
    private Transform mTransform;//камера
    private float currentZoom = 10f;
    private float currentRot = 0f;
    private float prevMouseX;

    #endregion


    #region Properties

    public Transform Target { set { mTarget = value; } }

    #endregion


    #region Method 

    public void OnStart()
    {
        Target = MainController.Instance.Character.Gfx.transform;//объект
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

    #endregion

}