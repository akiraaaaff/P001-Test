using Lockstep.Math;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.AnimatedValues;

namespace P001.GameView
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(CreateEntitiesByPrefabSystemGroup))]
    partial struct PlayerGenerateByPrefabSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerGeneratorByPrefab>();
        }
    
        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
    
        }
    
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var generator = SystemAPI.GetSingleton<PlayerGeneratorByPrefab>();
            var cubes = CollectionHelper.CreateNativeArray<Entity>(generator.cubeCount, Allocator.Temp);
            state.EntityManager.Instantiate(generator.cubeEntityProtoType, cubes);
            int count = 0;
            foreach (var cube in cubes)
            {
                var position = new LVector3(new LFloat(true, (count - generator.cubeCount / 2) * 1200), LFloat.zero, LFloat.zero);
                state.EntityManager.AddComponentData<PlayerMoveData>(cube, new PlayerMoveData
                {
                    moveSpeed = new LFloat(true,3000),
                    position= position,
                });
                var transform = SystemAPI.GetAspectRW<TransformAspect>(cube);
                transform.LocalPosition = position.ToVector3();
                count++;
            }
    
            cubes.Dispose();
            // 此System只在启动时运行一次，所以在第一次更新后关闭它。
            state.Enabled = false;
        }
    }
}
