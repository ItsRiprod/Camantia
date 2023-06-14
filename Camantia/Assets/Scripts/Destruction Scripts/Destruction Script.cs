using UnityEngine;

public class MeshDestroy : MonoBehaviour
{
    public float voxelSize = 0.1f;
    public float breakForce = 1.0f;

    private MeshRenderer meshRenderer;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DestroyMesh();
        }
    }

    private void DestroyMesh()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogError("MeshRenderer component not found!");
            return;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter component not found!");
            return;
        }

        Mesh mesh = meshFilter.mesh;
        Vector3[] vertices = mesh.vertices;

        foreach (Vector3 vertex in vertices)
        {
            GameObject voxel = GameObject.CreatePrimitive(PrimitiveType.Cube);
            voxel.transform.localScale = new Vector3(voxelSize, voxelSize, voxelSize);
            voxel.transform.position = transform.TransformPoint(vertex);
            voxel.AddComponent<Rigidbody>().AddForce(Random.insideUnitSphere * breakForce);
        }

        meshRenderer.enabled = false; // Disable the original mesh renderer to hide the intact mesh
    }
}