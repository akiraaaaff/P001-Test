using System.Collections.Generic;
using Lockstep.PathFinding;
using Lockstep.Math;

public class NavMeshManager {
    static TrianglePointPath path;
    static TriangleNavMesh navMesh;

    public static void DoInit(string data){
        navMesh = new TriangleNavMesh(data);
        path = new TrianglePointPath();
    }

    public static List<LVector3> FindPath(LVector3 fromPoint, LVector3 toPoint){
        var _ret = navMesh.FindPath(fromPoint, toPoint, path);
        return _ret;
    }
}