using UnityEngine;
using System.Collections;

public class MyMouseLook : MonoBehaviour {

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float smoothTime = 5f;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != Cursor.lockState);
        m_CharacterTargetRot = this.transform.localRotation;
        m_CameraTargetRot = Camera.main.transform.localRotation;
    }
	
	// Update is called once per frame
	void Update () {

        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        this.transform.localRotation = m_CharacterTargetRot;
        Camera.main.transform.localRotation = m_CameraTargetRot;

        /*
        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
        Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        */
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -90, 90);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }

}
