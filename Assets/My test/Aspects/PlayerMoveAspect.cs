using Lockstep.Math;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace P001.GameView
{
    readonly partial struct PlayerMoveAspect : IAspect
    {
        readonly RefRW<LocalTransform> localTransform;
        readonly RefRW<PlayerMoveData> moveData;

        public void Move(LFloat fixedDeltaTime, LVector2 variableJoystick)
        {
            var direction = LVector3.forward * variableJoystick.y + LVector3.right * variableJoystick.x;
            moveData.ValueRW.position += direction * moveData.ValueRO.moveSpeed * fixedDeltaTime;
            localTransform.ValueRW.Position = moveData.ValueRO.position.ToVector3();
        }
    }
}
