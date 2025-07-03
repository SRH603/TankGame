using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float moveSpeed;
    public float turnSpeed;
    public float acceleration; // 加速度
    public float scrollSpeed;
    public float panSpeed;
    public float turnSpeedAdjustmentFactor = 0.1f; // 用于调整转向速度的系数
    public float zoomSpeed;
    public float rotationCenter;

    private float currentSpeed;
    private float currentpanSpeed;
    private float yaw = 0.0f;
    private float pitch = 0.0f;


    private void Start()
    {

    }
    void Update()
    {
        // 检查Shift键是否被按下，如果是，则加倍移动速度
        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? 2.0f : 1.0f;
        float panMultiplier = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? 2.0f : 1.0f;

        // WASD 移动
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 当有输入时，增加当前速度
        if (h != 0 || v != 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        // 鼠标滚轮控制前进后退
        if (!(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))) // Alt未被按下
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * scrollSpeed, Space.Self);
        }

        // 动态调整转向速度
        float adjustedTurnSpeed = turnSpeed / (1 + currentSpeed * turnSpeedAdjustmentFactor);

        // 鼠标转向
        if (Input.GetMouseButton(1) && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))) // 右键被按下时才可以旋转
        {
            yaw += adjustedTurnSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
            pitch -= adjustedTurnSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            // 应用速度乘数
            currentSpeed = Mathf.Clamp(currentSpeed, moveSpeed, moveSpeed * 3) * speedMultiplier;
            transform.Translate(new Vector3(h, 0, v) * currentSpeed * Time.deltaTime);
        }

        // 鼠标左键拖拽进行平移
        currentpanSpeed = Mathf.Clamp(currentpanSpeed, panSpeed, panSpeed * 3) * panMultiplier;
        if (Input.GetMouseButton(0) && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && !Input.GetMouseButton(1))
        {
            float panH = -Input.GetAxis("Mouse X") * currentpanSpeed;
            float panV = -Input.GetAxis("Mouse Y") * currentpanSpeed;
            transform.Translate(panH, panV, 0, Space.Self);
        }

        // 检测是否按下 Alt 键
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            // Alt + 右键控制摄像机前进后退
            if (Input.GetMouseButton(1))
            {
                float zoom = (Input.GetAxis("Mouse Y") - Input.GetAxis("Mouse X")) * zoomSpeed;
                transform.Translate(0, 0, zoom, Space.Self);
            }

            // Alt + 左键进行环绕旋转
            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                float altYawDelta = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
                float altPitchDelta = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");

                yaw += altYawDelta;
                pitch -= altPitchDelta;
                Vector3 orbitPoint = transform.position + transform.forward * rotationCenter; // 定义旋转中心点
                transform.RotateAround(orbitPoint, transform.right, -Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime);
                transform.RotateAround(orbitPoint, Vector3.up, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime);
            }

            // Alt 键和鼠标滚轮一起使用时，根据鼠标位置进行缩放
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                float zoom = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

                // 获取鼠标在屏幕上的位置，并将其转换为视口坐标
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseViewportPosition = GetComponent<Camera>().ScreenToViewportPoint(mouseScreenPosition);

                // 根据鼠标的视口坐标来调整摄像机的位置，模拟向鼠标指针位置的缩放
                Vector3 zoomDirection = GetComponent<Camera>().transform.forward + (mouseViewportPosition - new Vector3(0.5f, 0.5f, 0)) * 2.0f; // 0.5f为视口中心
                GetComponent<Camera>().transform.position += zoomDirection * zoom;
            }

        }

    }
}

