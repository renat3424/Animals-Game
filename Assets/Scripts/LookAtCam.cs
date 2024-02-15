using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    public Transform cam;
    // Start is called before the first frame update

    private void Awake()
    {
        cam = Camera.main.transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        transform.LookAt(cam.position+cam.forward);
    }
}
