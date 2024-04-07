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
             new Vector3 (-0.0878f, 0.3090f, 0.0f), // 꼭지점 10
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
             new Vector3 (-0.0878f, 0.3090f, 2.0f) // 꼭지점 10
        };

        mesh.vertices = vertices;
int[] triangleIndices = new int[]
{
    0, 3, 4,   
    1, 2, 3,
    0, 4, 5,   
    0, 5, 6,   
    0, 6, 7,   
    0, 8, 9,  
    1, 7, 8,
    9, 2, 3,
    2, 8, 9,  
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

        MeshFilter mf = this.AddComponent<MeshFilter>();
        MeshRenderer mr = this.AddComponent<MeshRenderer>();

        mf.mesh = mesh;

        Material yellowMaterial = new Material(Shader.Find("Standard"));
        yellowMaterial.color = Color.yellow;

       
        mr.material = yellowMaterial;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
