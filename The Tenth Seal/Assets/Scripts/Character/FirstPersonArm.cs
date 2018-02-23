using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonArm : MonoBehaviour {

    public Rigidbody rb;
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;

    public Camera cameraPrincipal;

    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;

    void Start () {

        m_CharacterTargetRot = this.transform.localRotation;
        m_CameraTargetRot = cameraPrincipal.transform.localRotation;

    }
	
	void Update () {
        float forward = Input.GetAxis("Vertical");
        float side = Input.GetAxis("Horizontal");
        float rotY = Input.GetAxis("Mouse X");

        gameObject.transform.Rotate(0, rotY, 0);
        Vector3 speed = new Vector3(forward, 0.0f, side);
        rb.AddForce(speed);

        

    }


    public void LookRotation(Transform character, Transform camera)
    {
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;

        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
        
        
    }
}
