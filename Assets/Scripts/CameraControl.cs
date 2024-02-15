using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public static CameraControl instance;
    public Transform cameraPoint;//камера(позиция и поворот)
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cameraPoint.position;
        transform.rotation = cameraPoint.rotation;
    }
}
