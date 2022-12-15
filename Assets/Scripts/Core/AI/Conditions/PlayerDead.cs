using Leopotam.Ecs;
using BehaviourTree.Runtime.Nodes;
using BehaviourTree.Runtime.Nodes.Decorators;
using Core.ECS.Components.Units;
using Core.ECS.Components.Player;

namespace Core.AI.Units.Conditions
{
    [Category("Units/Conditions")]
    public class PlayerDead : Condition
    {
        private EcsFilter _filter;
        protected override void OnInit()
        {
            _filter = World.GetFilter(typeof(EcsFilter<PlayerTagComponent, DiedComponent>));
        }
        protected override bool Check() => _filter.GetEntitiesCount() == 0;
    }
}