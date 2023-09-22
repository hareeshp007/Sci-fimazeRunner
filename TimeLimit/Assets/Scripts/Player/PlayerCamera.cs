using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform PlayerObject;
    private float mouseX;
    private float mouseY;
    [SerializeField]
    private float MouseSensitivity = 100f;
    [SerializeField]
    private float xRotation = 0f;
    void Start()
    {
        //PlayerObject=PlayerService.Instance.GetPlayer().gameObject.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        HandleInput();
        MouseLookControl();
    }
    private void HandleInput()
    {
        mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
    }

    private void MouseLookControl()
    {
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerObject.Rotate(Vector3.up * mouseX);
    }
    public void SetPlayer(Transform Player)
    {
        PlayerObject = Player;
    }
}
