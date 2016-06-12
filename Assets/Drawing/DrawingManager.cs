using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using PDollarGestureRecognizer;
using System.IO;

public class DrawingManager : MonoBehaviour {

    //Drawing related
    public float chalkTotal = 100f;
    public float chalkAmmount = 100f;
    public float chalkSpendRate = 1f;

    private bool drawing;
    private bool mouseInside;
    private Vector3 mousePos;
    private bool newLine;

    //Recognition related
    private List<List<Point>> newGestures;
    private List<Point> currLinePoints;
    private List<Gesture> inputGestures;
    private List<Result> resultingGestures;
    private List<Gesture> trainingSet = new List<Gesture>();

    //Objects
    public GameObject canvas;
    public MyLineRenderer currentGestureLineRenderer;
    public GameObject character;
    public GameObject scroll;
    public Slider chalkSlide;
    private MyMouseLook mouselookScript;
    private MyCharController charControllerScript;

    // Use this for initialization
    void Start () {
        mouselookScript = character.GetComponent<MyMouseLook>();
        charControllerScript = character.GetComponent<MyCharController>();
        drawing = false;
        mouseInside = false;
        currLinePoints = new List<Point>();
        newGestures = new List<List<Point>>();
        inputGestures = new List<Gesture>();
        resultingGestures = new List<Result>();

        //Load pre-made gestures
        TextAsset[] gesturesXml = Resources.LoadAll<TextAsset>("GestureSet/10-stylus-MEDIUM/");
        foreach (TextAsset gestureXml in gesturesXml)
            trainingSet.Add(GestureIO.ReadGestureFromXML(gestureXml.text));

        Debug.Log(Application.persistentDataPath);
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

            if (Input.GetMouseButtonUp(0) && currLinePoints.Count>0)
            {
                currentGestureLineRenderer.finishCurrLine();
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
                    currLinePoints.Add(new Point(mousePos.x, -mousePos.y));
                    currentGestureLineRenderer.SetPosition(camVector, newLine);
                    newLine = false;
                }

                if (Input.GetMouseButton(0))
                {
                    chalkAmmount -= chalkSpendRate * 15 * Time.deltaTime;
                    chalkSlide.value = chalkAmmount/chalkTotal;
                    currLinePoints.Add(new Point(mousePos.x, -mousePos.y));
                    currentGestureLineRenderer.SetPosition(camVector, newLine);
                }
            }
        }

    }

    public void recognizeLatestGesture(List<Point> gesture)
    {
        Gesture candidate = new Gesture(gesture.ToArray());
        inputGestures.Add(candidate);
        Result gestureResult = PointCloudRecognizer.Classify(candidate, trainingSet.ToArray());
        if (gestureResult.Score >= 0.5) {
            resultingGestures.Add(gestureResult);
            candidate.Name = gestureResult.GestureClass;
            Debug.Log(candidate.Name);
            Debug.Log(gestureResult.Score);
        }
    }

    public void recognizeFullSpell()
    {

    }

    public void saveGesture()
    {
        //GestureIO.WriteGesture(currLinePoints.ToArray(), newGestureName, fileName);
    }

}
