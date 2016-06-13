using UnityEngine;
using System.Collections.Generic;

public class MyLineRenderer : MonoBehaviour {

    public float lineWidth = 1f;
    public Material drawingMaterial;
    public Material finishedMaterial;

    private List<Vector3> points;
    private List<Vector3> newVertices;
    private List<int> newTriangles;
    private List<int> currTriangles;
    private List<Vector2> newUV;
    private float pointsBeforeNewLine;

    public int subMesh = 0;
    private Mesh mesh;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        resetMesh();
    }

    public void resetMesh()
    {
        currTriangles = new List<int>();
        mesh = new Mesh();
        mesh.Clear();
        mesh.name = "drawingLine";
        GetComponent<MeshFilter>().mesh = mesh;
        newVertices = new List<Vector3>();
        points = new List<Vector3>();
        newTriangles = new List<int>();
        newUV = new List<Vector2>();
        pointsBeforeNewLine = 0;
        subMesh = 0;
        mesh.subMeshCount = 1;
    }

    public void finishCurrLine()
    {
            Material[] newMaterials = meshRenderer.materials;
            newMaterials[newMaterials.Length - 1] = finishedMaterial;
            meshRenderer.materials = newMaterials;
    }

    public void SetPosition(Vector3 newPos, bool newLine)
    {
        if (points.Count > 0 && newPos == points[points.Count - 1])
            return;

        if (newLine)
        {
            subMesh++;
            mesh.subMeshCount = subMesh + 1;
            Material[] newMaterials = new Material[meshRenderer.materials.Length + 1];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                newMaterials[i] = meshRenderer.materials[i];
            }
            newMaterials[newMaterials.Length - 1] = drawingMaterial;
            meshRenderer.materials = newMaterials;
            currTriangles = new List<int>();
            pointsBeforeNewLine = points.Count;
        }

        points.Add(newPos);

        if (points.Count - pointsBeforeNewLine == 1)
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
        if (points.Count - pointsBeforeNewLine == 2)
        {
            currTriangles.Add(0);
            currTriangles.Add(2);
            currTriangles.Add(3);
            currTriangles.Add(3);
            currTriangles.Add(1);
            currTriangles.Add(0);

            newTriangles.AddRange(currTriangles);
        }
        else
        {
            int t1a = newVertices.Count - 4;
            int t1b = newVertices.Count - 3;
            int t1c = newVertices.Count - 1;

            int t2a = newVertices.Count - 2;
            int t2b = newVertices.Count - 4;
            int t2c = newVertices.Count - 1;

            currTriangles.Add(t1a);
            currTriangles.Add(t1b);
            currTriangles.Add(t1c);
            currTriangles.Add(t2a);
            currTriangles.Add(t2b);
            currTriangles.Add(t2c);

            newTriangles.AddRange(currTriangles);
        }
        mesh.SetTriangles(currTriangles.ToArray(), subMesh);

        newUV.Add(new Vector2(0,1));
        newUV.Add(new Vector2(0,0));
        newUV.Add(new Vector2(0,0));
        newUV.Add(new Vector2(1,0));

        mesh.uv = newUV.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();
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
