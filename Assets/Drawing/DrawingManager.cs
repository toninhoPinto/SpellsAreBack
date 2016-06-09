using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using PDollarGestureRecognizer;
using System.IO;

public class DrawingManager : MonoBehaviour {
    private MyMouseLook mouselookScript;
    private MyCharController charControllerScript;

    public GameObject canvas;
    public float chalkTotal = 100f;
    public float chalkAmmount = 100f;
    public float chalkSpendRate = 1f;

    public bool drawing;
    public bool mouseInside;
    Vector3 mousePos;
    private List<List<Point>> newGestures;
    private List<Point> currLinePoints;
    private bool newLine;
    private List<Result> resultingGestures;
    private List<Gesture> trainingSet = new List<Gesture>();

    public MyLineRenderer currentGestureLineRenderer;
    public float ZLineCoord;
    public GameObject character;
    public GameObject scroll;
    public Slider chalkSlide;

    // Use this for initialization
    void Start () {
        mouselookScript = character.GetComponent<MyMouseLook>();
        charControllerScript = character.GetComponent<MyCharController>();
        drawing = false;
        mouseInside = false;
        currLinePoints = new List<Point>();
        newGestures = new List<List<Point>>();

        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        //Load user custom gestures
        string[] filePaths = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (string filePath in filePaths)
            trainingSet.Add(GestureIO.ReadGestureFromFile(filePath));
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

            if (Input.GetMouseButtonUp(0))
            {
                newLine = true;
                newGestures.Add(currLinePoints);
                recognizeLatestGesture(currLinePoints);
                currLinePoints = new List<Point>();
            }

            if (mouseInside && chalkAmmount > 0)
            {
                Vector3 camVector = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane + 0.5f));
                if (Input.GetMouseButtonDown(0))
                {
                    currLinePoints.Add(new Point(mousePos.x, mousePos.y));
                    currentGestureLineRenderer.SetPosition(camVector, newLine);
                    newLine = false;
                }

                if (Input.GetMouseButton(0))
                {
                    chalkAmmount -= chalkSpendRate * 15 * Time.deltaTime;
                    chalkSlide.value = chalkAmmount/chalkTotal;
                    currLinePoints.Add(new Point(mousePos.x, mousePos.y));
                    currentGestureLineRenderer.SetPosition(camVector, newLine);
                }
            }
        }

    }

    public void recognizeLatestGesture(List<Point> gesture)
    {
        Gesture candidate = new Gesture(gesture.ToArray());
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
        Debug.Log(gestureResult.GestureClass);
    }

}
