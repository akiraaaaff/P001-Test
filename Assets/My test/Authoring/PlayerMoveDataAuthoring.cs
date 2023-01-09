using Lockstep.Math;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace P001.GameView
{
    struct PlayerMoveData : IComponentData
    {
        public LFloat moveSpeed;
        public LVector3 position;
    }
    public class PlayerMoveDataAuthoring : MonoBehaviour
    {
        [Range(0, 10)]public LFloat moveSpeed = new (true,1000);
        public class Baker : Baker<PlayerMoveDataAuthoring>
        {
            public override void Bake(PlayerMoveDataAuthoring authoring)
            {
                var data = new PlayerMoveData
                {
                    moveSpeed = authoring.moveSpeed
                };
                AddComponent(data);
            }
        }
    }
}
