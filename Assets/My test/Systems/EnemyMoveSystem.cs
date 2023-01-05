using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;

namespace P001.GameView
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(CreateEntitiesByPrefabSystemGroup))]
    partial struct EnemyMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {

        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }

        public void OnUpdate(ref SystemState state)
        {
            var fixedDeltaTime = SystemAPI.Time.fixedDeltaTime;
            var playerPos = float3.zero;
            foreach (var moveData in SystemAPI.Query<PlayerMoveData>())
            {
                playerPos = moveData.position;
                break;
            }
            foreach (var aspect in SystemAPI.Query<EnemyMoveAspect>())
            {
                aspect.Move(fixedDeltaTime, playerPos);
            }
        }
    }
}
