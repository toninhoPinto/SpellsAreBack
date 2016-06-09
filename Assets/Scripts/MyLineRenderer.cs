using UnityEngine;
using System.Collections.Generic;

public class MyLineRenderer : MonoBehaviour {

    public float lineWidth = 1f;

    private List<Vector3> points;
    private List<Vector3> newVertices;
    private List<int> newTriangles;
    private List<Vector2> newUV;

    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    void Start()
    {
        resetMesh();
    }

    public void resetMesh()
    {
        mesh = new Mesh();
        mesh.Clear();
        mesh.name = "drawingLine";
        GetComponent<MeshFilter>().mesh = mesh;
        newVertices = new List<Vector3>();
        points = new List<Vector3>();
        newTriangles = new List<int>();
        newUV = new List<Vector2>();
    }

    public void SetPosition(Vector3 newPos)
    {
        if (points.Count > 0 && newPos == points[points.Count - 1])
            return;
        
        points.Add(newPos);

        if (points.Count == 1)
            return;

        Vector3 lastPoint = points[points.Count - 2];
        Vector3 side = Vector3.Cross(newPos-lastPoint, Camera.main.transform.position-lastPoint);
        side.Normalize();

        //will make the first square
        Vector3 a = lastPoint + side * ((lineWidth * 0.05f) / 2); //left top
        Vector3 b = newPos + side * ((lineWidth * 0.05f) / 2); //right top
        Vector3 c = lastPoint + side * ((lineWidth * 0.05f) / -2); //left bottom
        Vector3 d = newPos + side * ((lineWidth * 0.05f) / -2); //right bottom

        newVertices.Add(this.transform.worldToLocalMatrix.MultiplyPoint(a));
        newVertices.Add(this.transform.worldToLocalMatrix.MultiplyPoint(b));
        newVertices.Add(this.transform.worldToLocalMatrix.MultiplyPoint(c));
        newVertices.Add(this.transform.worldToLocalMatrix.MultiplyPoint(d));

        mesh.vertices = newVertices.ToArray();

        //tries
        if (points.Count == 2)
        {
            newTriangles.Add(0);
            newTriangles.Add(2);
            newTriangles.Add(3);
            newTriangles.Add(3);
            newTriangles.Add(1);
            newTriangles.Add(0);
        }
        else
        {
            int t1a = newVertices.Count - 4;
            int t1b = newVertices.Count - 3;
            int t1c = newVertices.Count - 1;

            int t2a = newVertices.Count - 2;
            int t2b = newVertices.Count - 4;
            int t2c = newVertices.Count - 1;

            newTriangles.Add(t1a);
            newTriangles.Add(t1b);
            newTriangles.Add(t1c);
            newTriangles.Add(t2a);
            newTriangles.Add(t2b);
            newTriangles.Add(t2c);
        }
        mesh.triangles = newTriangles.ToArray();

        newUV.Add(new Vector2(0,1));
        newUV.Add(new Vector2(0,0));
        newUV.Add(new Vector2(0,0));
        newUV.Add(new Vector2(1,0));

        mesh.uv = newUV.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
    }

    public void printVertices()
    {
        for (int i = 0; i < points.Count; i++)
            Debug.Log(points[i]);

        Debug.Log("========================================================");

        for (int i = 0; i < newVertices.Count; i++)
            Debug.Log(newVertices[i]);
    }

    /*
    void OnDrawGizmos()
    {
        Vector3 size = new Vector3(0.003f, 0.003f, 0.003f);

        Gizmos.color = Color.red;
        for (int i = 0; i < points.Count; i++) {
            Gizmos.DrawCube(points[i], size);
        }

        Gizmos.color = Color.green;
        for (int i = 0; i < newVertices.Count; i++)
            Gizmos.DrawSphere(newVertices[i], 0.001f);
    }
    */

}
