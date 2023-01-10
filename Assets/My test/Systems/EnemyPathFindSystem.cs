using Lockstep.Math;
using Unity.Burst;
using Unity.Entities;

namespace P001.GameView
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(CreateEntitiesByPrefabSystemGroup))]
    partial struct EnemyPathFindSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            //state.RequireForUpdate<EnemyGenerateByPrefabSystem>();
        }
    
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
    
        }
    
        //[BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var playerPos = LVector3.zero;
            foreach (var moveData in SystemAPI.Query<PlayerMoveData>())
            {
                playerPos = moveData.position;
                break;
            }
            foreach (var moveData in SystemAPI.Query<EnemyMoveData>())
            {
                UnityEngine.Debug.LogWarning(playerPos);
                UnityEngine.Debug.LogWarning(moveData.position);
                var aa=NavMeshManager.FindPath(playerPos, moveData.position);
                if (aa != null)
                {
                    UnityEngine.Debug.LogWarning(aa.Count);
                    UnityEngine.Debug.LogWarning(string.Join(",",aa));
                }
            }
        }
    }
}
