using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float moveSpeed;
    public float turnSpeed;
    public float acceleration; // ���ٶ�
    public float scrollSpeed;
    public float panSpeed;
    public float turnSpeedAdjustmentFactor = 0.1f; // ���ڵ���ת���ٶȵ�ϵ��
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
        // ���Shift���Ƿ񱻰��£�����ǣ���ӱ��ƶ��ٶ�
        float speedMultiplier = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? 2.0f : 1.0f;
        float panMultiplier = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? 2.0f : 1.0f;

        // WASD �ƶ�
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // ��������ʱ�����ӵ�ǰ�ٶ�
        if (h != 0 || v != 0)
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else
        {
            currentSpeed = moveSpeed;
        }

        // �����ֿ���ǰ������
        if (!(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))) // Altδ������
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            transform.Translate(0, 0, scroll * scrollSpeed, Space.Self);
        }

        // ��̬����ת���ٶ�
        float adjustedTurnSpeed = turnSpeed / (1 + currentSpeed * turnSpeedAdjustmentFactor);

        // ���ת��
        if (Input.GetMouseButton(1) && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))) // �Ҽ�������ʱ�ſ�����ת
        {
            yaw += adjustedTurnSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
            pitch -= adjustedTurnSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

            // Ӧ���ٶȳ���
            currentSpeed = Mathf.Clamp(currentSpeed, moveSpeed, moveSpeed * 3) * speedMultiplier;
            transform.Translate(new Vector3(h, 0, v) * currentSpeed * Time.deltaTime);
        }

        // ��������ק����ƽ��
        currentpanSpeed = Mathf.Clamp(currentpanSpeed, panSpeed, panSpeed * 3) * panMultiplier;
        if (Input.GetMouseButton(0) && !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && !Input.GetMouseButton(1))
        {
            float panH = -Input.GetAxis("Mouse X") * currentpanSpeed;
            float panV = -Input.GetAxis("Mouse Y") * currentpanSpeed;
            transform.Translate(panH, panV, 0, Space.Self);
        }

        // ����Ƿ��� Alt ��
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
        {
            // Alt + �Ҽ����������ǰ������
            if (Input.GetMouseButton(1))
            {
                float zoom = (Input.GetAxis("Mouse Y") - Input.GetAxis("Mouse X")) * zoomSpeed;
                transform.Translate(0, 0, zoom, Space.Self);
            }

            // Alt + ������л�����ת
            if (Input.GetMouseButton(0) && !Input.GetMouseButton(1))
            {
                float altYawDelta = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
                float altPitchDelta = turnSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");

                yaw += altYawDelta;
                pitch -= altPitchDelta;
                Vector3 orbitPoint = transform.position + transform.forward * rotationCenter; // ������ת���ĵ�
                transform.RotateAround(orbitPoint, transform.right, -Input.GetAxis("Mouse Y") * turnSpeed * Time.deltaTime);
                transform.RotateAround(orbitPoint, Vector3.up, Input.GetAxis("Mouse X") * turnSpeed * Time.deltaTime);
            }

            // Alt ����������һ��ʹ��ʱ���������λ�ý�������
            if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            {
                float zoom = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

                // ��ȡ�������Ļ�ϵ�λ�ã�������ת��Ϊ�ӿ�����
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseViewportPosition = GetComponent<Camera>().ScreenToViewportPoint(mouseScreenPosition);

                // ���������ӿ������������������λ�ã�ģ�������ָ��λ�õ�����
                Vector3 zoomDirection = GetComponent<Camera>().transform.forward + (mouseViewportPosition - new Vector3(0.5f, 0.5f, 0)) * 2.0f; // 0.5fΪ�ӿ�����
                GetComponent<Camera>().transform.position += zoomDirection * zoom;
            }

        }

    }
}

