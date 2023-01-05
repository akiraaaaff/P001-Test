using Unity.Entities;
namespace P001.GameView
{
    public class SystemGroup : ComponentSystemGroup { }
    
    [UpdateInGroup(typeof(SystemGroup))]
    public class CreateEntitiesByPrefabSystemGroup : AuthoringSceneSystemGroup
    {
        protected override string AuthoringSceneName => "Example Scene";
    }
}