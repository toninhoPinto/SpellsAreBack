using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawingManager : MonoBehaviour {
    private MyMouseLook mouselookScript;
    private MyCharController charControllerScript;

    public GameObject canvas;

    public bool drawing;
    public bool mouseInside;
    Vector3 mousePos;
    private List<Vector2> points;
    private int vertexCount = 0;

    public MyLineRenderer currentGestureLineRenderer;
    public float ZLineCoord;
    public GameObject character;
    public GameObject scroll;

    // Use this for initialization
    void Start () {
        mouselookScript = character.GetComponent<MyMouseLook>();
        charControllerScript = character.GetComponent<MyCharController>();
        drawing = false;
        mouseInside = false;
        points = new List<Vector2>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.K))
        {
            drawing = !drawing;
            mouselookScript.enabled = !mouselookScript.enabled;
            charControllerScript.enabled = !charControllerScript.enabled;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = !Cursor.visible;
            canvas.SetActive(!canvas.activeSelf);
            scroll.SetActive(!scroll.activeSelf);
            mouseInside = false;
            ZLineCoord = scroll.transform.position.z;
            if (!drawing)
                currentGestureLineRenderer.resetMesh();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            currentGestureLineRenderer.printVertices();
        }

        if (drawing)
        {
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f);
            Ray ray = Camera.main.ScreenPointToRay(mousePos);   
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                mouseInside = hit.collider.gameObject.tag.Equals("Scroll");
            }
            else mouseInside = false;

            if (mouseInside)
            {
                Vector3 camVector = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane + 0.5f));
                if (Input.GetMouseButtonDown(0))
                {
                    points.Add(mousePos);
                    currentGestureLineRenderer.SetPosition(camVector);
                }

                if (Input.GetMouseButton(0))
                {
                    points.Add(mousePos);
                    currentGestureLineRenderer.SetPosition(camVector);
                }
            }
        }

    }

}
