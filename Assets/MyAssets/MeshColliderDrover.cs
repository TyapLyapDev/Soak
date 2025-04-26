using UnityEngine;

public class MeshColliderDrover : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        if (GetComponent<MeshCollider>() != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireMesh(GetComponent<MeshCollider>().sharedMesh, transform.position, transform.rotation, transform.lossyScale);
        }
    }
}
