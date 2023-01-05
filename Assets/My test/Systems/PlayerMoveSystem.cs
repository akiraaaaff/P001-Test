using Unity.Burst;
using Unity.Entities;

namespace P001.GameView
{
    [BurstCompile]
    [RequireMatchingQueriesForUpdate]
    [UpdateInGroup(typeof(CreateEntitiesByPrefabSystemGroup))]
    partial struct PlayerMoveSystem : ISystem
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
            var joystickDirection = Joystick.input;
            foreach (var aspect in SystemAPI.Query<PlayerMoveAspect>())
            {
                aspect.Move(fixedDeltaTime, joystickDirection);
            }
        }
    }
}
