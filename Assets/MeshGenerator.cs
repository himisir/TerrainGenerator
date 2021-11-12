using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    public int xSize = 20;
    public int zSize = 20;
    public float yNoise;
    public float offset = 2f;
    public float microOffset;
    public float renderSpeed;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;


    }
    private void Update()
    {
        UpdateMesh();
        CreateShape();
    }
    void CreateShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                yNoise = Mathf.PerlinNoise(x + microOffset, z + microOffset) * offset;
                vertices[i] = new Vector3(x, yNoise, z);
                i++;
            }
        }

        triangles = new int[6 * xSize * zSize];
        int vert = 0;
        int tris = 0;
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = 0 + vert;


                triangles[tris + 1] = xSize + 1 + vert;
                triangles[tris + 2] = 1 + vert;
                triangles[tris + 3] = 1 + vert;
                triangles[tris + 4] = xSize + 1 + vert;
                triangles[tris + 5] = xSize + 2 + vert;
                vert++;
                tris += 6;

            }
            vert++;
        }

    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();


    }
    private void OnDrawGizmos()
    {

        if (vertices == null) return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], .1f);
        }
    }
}
