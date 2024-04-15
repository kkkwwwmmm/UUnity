using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(StaticMeshGen))]
public class StaticMeshGenEditor : Editor
{
    //버튼만들기 예제
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        StaticMeshGen script = (StaticMeshGen)target;

        if (GUILayout.Button("Generate Mesh"))
        {
            script.GenerateMesh();
        }

    }
}

//메쉬만들기 예제
public class StaticMeshGen : MonoBehaviour
{
    // Start is called before the first frame update
    public void GenerateMesh()
    {
        Mesh mesh = new Mesh();

       Vector3[] vertices = new Vector3[]
        {
             new Vector3 (0.0f, 1.0f, 0.0f), // 꼭지점 1
             new Vector3 (0.1878f, 0.3090f, 0.0f), // 꼭지점 2
             new Vector3 (0.9511f, 0.3090f, 0.0f), // 꼭지점 3
             new Vector3 (0.1511f, -0.3090f, 0.0f), // 꼭지점 4
             new Vector3 (0.5878f, -0.8090f, 0.0f), // 꼭지점 5
             new Vector3 (0.0f, -0.3f, 0.0f), // 꼭지점 6
             new Vector3 (-0.5878f, -0.8090f, 0.0f), // 꼭지점 7
             new Vector3 (-0.1511f, -0.3090f, 0.0f), // 꼭지점 8
             new Vector3 (-0.9511f, 0.3090f, 0.0f), // 꼭지점 9
             new Vector3 (-0.1878f, 0.3090f, 0.0f), // 꼭지점 10
             //별2
             new Vector3 (0.0f, 1.0f, 2.0f), // 꼭지점 1
             new Vector3 (0.1878f, 0.3090f, 2.0f), // 꼭지점 2
             new Vector3 (0.9511f, 0.3090f, 2.0f), // 꼭지점 3
             new Vector3 (0.1511f, -0.3090f, 2.0f), // 꼭지점 4
             new Vector3 (0.5878f, -0.8090f, 2.0f), // 꼭지점 5
             new Vector3 (0.0f, -0.3f, 2.0f), // 꼭지점 6
             new Vector3 (-0.5878f, -0.8090f, 2.0f), // 꼭지점 7
             new Vector3 (-0.1511f, -0.3090f, 2.0f), // 꼭지점 8
             new Vector3 (-0.9511f, 0.3090f, 2.0f), // 꼭지점 9
             new Vector3 (-0.1878f, 0.3090f, 2.0f) // 꼭지점 10
        };

        mesh.vertices = vertices;
int[] triangleIndices = new int[]
{
    0,4,7,
    2,6,9,
    1,4,8,
    0,3,6,
     //반대별
    10,14,17,
    12,16,19,
    11,14,18,
    10,13,16,
    //기둥
    0,10,11,
    11,1,0,
    1,11,12,
    12,2,1,
    2,12,13,
    13,3,2,
    3,13,14,
    14,4,3,
    4,14,15,
    15,5,4,
    5,15,16,
    16,6,5,
    6,16,17,
    17,7,6,
    7,17,18,
    18,8,7,
    8,18,19,
    19,9,8,
    9,19,10,
    10,0,9
};
        mesh.triangles = triangleIndices;
        Vector3[] normals = new Vector3[vertices.Length];

for (int i = 0; i < triangleIndices.Length; i += 3)
{
    int index0 = triangleIndices[i];
    int index1 = triangleIndices[i + 1];
    int index2 = triangleIndices[i + 2];

    Vector3 vertex0 = vertices[index0];
    Vector3 vertex1 = vertices[index1];
    Vector3 vertex2 = vertices[index2];

    // 삼각형의 두 변을 구합니다.
    Vector3 side1 = vertex1 - vertex0;
    Vector3 side2 = vertex2 - vertex0;

    // 삼각형의 법선을 계산합니다.
    Vector3 normal = Vector3.Cross(side1, side2).normalized;

    // 각 정점에 해당 법선을 추가합니다.
    normals[index0] += normal;
    normals[index1] += normal;
    normals[index2] += normal;
}

// 정규화된 법선을 적용합니다.
for (int i = 0; i < normals.Length; i++)
{
    normals[i] = normals[i].normalized;
}
mesh.normals = normals;

        MeshFilter mf = gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>();

        mf.mesh = mesh;
        
        Vector3[] tnormals = mesh.normals;
        Vector4[] triangleNormals = new Vector4[tnormals.Length];
        for (int i = 0; i < tnormals.Length; i++)
        {
            triangleNormals[i] = new Vector4(tnormals[i].x, tnormals[i].y, tnormals[i].z, 0);
        }
        
        mr.sharedMaterial.SetVectorArray("_TriangleNormal", triangleNormals);
}
    

    // Update is called once per frame
    void Update()
    {
        
    }
}

