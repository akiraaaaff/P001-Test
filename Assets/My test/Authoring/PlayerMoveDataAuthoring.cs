using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace P001.GameView
{
    struct PlayerMoveData : IComponentData
    {
        public float moveSpeed;
        public float3 position;
    }
    public class PlayerMoveDataAuthoring : MonoBehaviour
    {
        [Range(0, 10)]public float moveSpeed = 1.0f;
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
