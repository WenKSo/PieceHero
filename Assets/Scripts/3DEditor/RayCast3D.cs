using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast3D : MonoBehaviour
{
    public Camera cam;
    public LayerMask mask;
    private Connector connector;
    private MapEditor3D mapEditor;

    void Start()
    {
        connector = FindObjectOfType<Connector>();
        mapEditor = FindObjectOfType<MapEditor3D>();
    }

    void Update()
    {
        if(mapEditor.currentMode != Mode.Connect) return;

        //Draw Ray
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = cam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos-transform.position,Color.blue);

        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,100,mask))
            {
                connector.startBlock = hit.transform.GetComponent<Block3D>();
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, mask))
            {
                //if(connector.startBlock == hit.transform.GetComponent<Block3D>()) return;
                connector.endBlock = hit.transform.GetComponent<Block3D>();
            }
        }
    }
}
