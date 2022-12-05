using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connector : MonoBehaviour
{
    public Block3D startBlock;
    public Block3D endBlock;

    private Vector3 mousePos;
    private Vector3 startMousePos;
    //This is the distance the clickable plane is from the camera. Set it in the Inspector before running.
    public float m_DistanceZ;
    private Plane m_Plane;
    private Vector3 m_DistanceFromCamera;

    public LineRenderer lineRend;
    private MapEditor3D mapEditor;

    void Start()
    {
        mapEditor = FindObjectOfType<MapEditor3D>();
        
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 2;

        //This is how far away from the Camera the plane is placed
        m_DistanceFromCamera = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z - m_DistanceZ);

        //Create a new plane with normal (0,0,1) at the position away from the camera you define in the Inspector. This is the plane that you can click so make sure it is reachable.
        m_Plane = new Plane(Vector3.forward, m_DistanceFromCamera);
    }

    void Update()
    {
        if(mapEditor.currentMode != Mode.Connect) return;
        
        if(!startBlock) return;
        // if(Input.GetMouseButtonDown(0))
        // {
        //     startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // }

        if(Input.GetMouseButton(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.y = startBlock.transform.position.y;
            lineRend.SetPosition(0, startBlock.transform.position);
            lineRend.SetPosition(1, endBlock.transform.position);
        }

        if(Input.GetMouseButtonUp(0))
        {
            startBlock.next = endBlock;
            startBlock = null;
            endBlock = null;
        }
    }
}
