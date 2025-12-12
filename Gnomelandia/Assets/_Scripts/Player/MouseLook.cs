using UnityEngine;

public class MouseLook : MonoBehaviour
{

    enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }


    [Header("Degrees of Freedom")]
    [SerializeField] RotationAxes _axes = RotationAxes.MouseXAndY;

    [Space(height: 5)]
    [Header("Sensitivity")]
    [SerializeField] float _sensitivitiyHorizontal = 9.0f;
    [SerializeField] float _sensitivitiyVertical = 9.0f;

    [Space(height: 5)]
    [Header("Constraints")]
    [SerializeField] float _minVerticalAngle = -45.0f;
    [SerializeField] float _maxVerticalAngle = 45.0f;

    private float _verticalRotation = 0.0f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {

        switch (_axes)
        {
            case RotationAxes.MouseX:
                transform.Rotate(
                    xAngle: 0.0f,

                    yAngle: Input.GetAxis("Mouse X") * _sensitivitiyHorizontal,

                    zAngle: 0.0f);

                break;

            
            case RotationAxes.MouseY:
                _verticalRotation -= Input.GetAxis("Mouse Y") * _sensitivitiyVertical;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);

                float horizontalRotation = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(
                    _verticalRotation, horizontalRotation, 0.0f
                );

                break;

            case RotationAxes.MouseXAndY:
                _verticalRotation -= Input.GetAxis("Mouse Y") * _sensitivitiyVertical;
                _verticalRotation = Mathf.Clamp(_verticalRotation, _minVerticalAngle, _maxVerticalAngle);

                float deltaX = Input.GetAxis("Mouse X") * _sensitivitiyHorizontal;
                horizontalRotation = transform.localEulerAngles.y + deltaX;

                transform.localEulerAngles = new Vector3(
                    _verticalRotation, horizontalRotation, 0.0f
                );

                break;


        }


    }
}
