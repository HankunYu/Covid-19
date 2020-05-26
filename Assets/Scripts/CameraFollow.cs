using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    [Range (3f,10f)]
    public float Yoffset,Zoffset;
    [Range(1f, 4f)]
    public float speed=1;
    public bool FollowRotation;
    public bool LookDown;
    [SerializeField]
    GameObject Cam,Cam2;
    // Start is called before the first frame update
    void Start()
    {
        Cam = GameObject.Find("Main Camera");
        Cam.SetActive(true);
        Cam2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!LookDown)
        {
            Cam.SetActive(true);
            Cam2.SetActive(false);
            Cam.transform.LookAt(target);
            Cam.transform.position = new Vector3(
                Mathf.Lerp(Cam.transform.position.x, target.position.x, Time.deltaTime * speed),
                Mathf.Lerp(Cam.transform.position.y, target.position.y + Yoffset, Time.deltaTime * speed),
                Mathf.Lerp(Cam.transform.position.z, target.position.z - Zoffset, Time.deltaTime * speed));
            if (FollowRotation)
            {

            }
        }
        else
        {
            Cam.SetActive(false);
            Cam2.SetActive(true);
        }
    }
}
