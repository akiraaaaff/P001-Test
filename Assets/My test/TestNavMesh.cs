using UnityEngine;


public class TestNavMesh : MonoBehaviour {
    public TextAsset navData;

    private void Awake()
    {
        NavMeshManager.DoInit(navData.text);
    }
}