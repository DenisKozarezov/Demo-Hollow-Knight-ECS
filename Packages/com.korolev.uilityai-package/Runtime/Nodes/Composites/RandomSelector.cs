namespace BehaviourTree.Runtime.Nodes.Composites
{
    public class RandomSelector : Composite
    {
        protected override State OnUpdate()
        {
            System.Random random = new System.Random();
            int rand = random.Next(Children.Count);
            return Children[rand].Update();
        }
    }   
}