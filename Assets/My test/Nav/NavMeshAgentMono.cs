using Lockstep.Logic;
using Lockstep.Collision2D;
using Lockstep.Math;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;
using Unity.Transforms;

public class NavMeshAgentMono : UnityEngine.MonoBehaviour {
    private CNavMesh agent = new CNavMesh();
    public CTransform2D Transform;

    public void DoStart(){
        agent.Speed = 2.ToLFloat();
        agent.Radius = 2.ToLFloat();
        agent.StoppingDistance = 2.ToLFloat();
        agent.FuncOnReachTargetPos += OnReachTargetPos;
        Transform = new CTransform2D();
        agent.transform2D = Transform;
        Transform.Pos3 = transform.position.ToLVector3();
    }

    public void Update(){
        Transform.Pos3 = transform.position.ToLVector3();
        agent.DoUpdate(Time.deltaTime.ToLFloat());
        transform.position = Transform.Pos3.ToVector3();
        transform.LookAt(transform.position +  agent.curNormal.ToVector3());
    }

    public void SetDestination(Vector3 targetPos)
    {
        SetDestination(targetPos.ToLVector3());
    }

    public void SetDestination(LVector3 targetPos){
        if (isDebug) { 
            int i = 0;
        }
        Transform.Pos3 = transform.position.ToLVector3();
        agent.SetDestination(targetPos);
    }

    void OnReachTargetPos(){ }

    public bool isDebug = false;
    void OnDrawGizmos(){
        if(!isDebug) return;
        agent.OnDrawGizmos();
    }
}