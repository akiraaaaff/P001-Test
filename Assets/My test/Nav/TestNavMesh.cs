using UnityEngine;
using Lockstep.Math;
using Lockstep.PathFinding;
using UnityEngine.Profiling;
using Unity.Transforms;
using System.Collections.Generic;

public class TestNavMesh : MonoBehaviour
{
    public NavMeshAgentMono findTrs1;
    public NavMeshAgentMono findTrs2;
    public NavMeshAgentMono findTrs3;
    public NavMeshAgentMono tarTrs;

    public TriangleNavMesh NavMesh;

    public TextAsset navData;
    void CheckInit()
    {
        if (NavMesh != null)
        {
            return;
        }
        var txt = navData;
        NavMesh = new TriangleNavMesh(txt.text);
        NavMeshManager.DoInit(txt.text);
        findTrs1.DoStart();
        findTrs2.DoStart();
        findTrs3.DoStart();
        tarTrs.DoStart();
    }

    private void Update()
    {
        CheckInit();
        findTrs1.SetDestination(tarTrs.transform.position);
        findTrs2.SetDestination(tarTrs.transform.position);
        findTrs3.SetDestination(tarTrs.transform.position);
    }
}