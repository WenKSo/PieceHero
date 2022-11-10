using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Camera cam;
    public LayerMask mask;

    void Update()
    {
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
                Debug.Log(hit.transform.root.name);
                MapManager mapManager = FindObjectOfType<MapManager>();
                Debug.Log(hit.collider.transform.GetComponent<Piece>());
                mapManager.piece = hit.transform.root.GetComponent<Piece>();
            }
        }
    }
}
