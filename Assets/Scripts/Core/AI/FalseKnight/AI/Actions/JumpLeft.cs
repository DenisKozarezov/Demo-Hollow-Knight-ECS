using AI.BehaviorTree.Nodes;
using UnityEngine;

namespace Examples.Example_1.FalseKnight.AI.Actions
{
    public class JumpLeft : ActionNode
    {
        public override void OnInit() { }

        public override void OnStart() { }

        public override void OnStop() { }

        public override State OnUpdate()
        {
            if (this.BehaviorTreeRef.GameObjectRef == null)
                return State.Failure;
            if (this.BehaviorTreeRef.GameObjectRef.GetComponent<FalseKnight>() == null)
                return State.Failure;

            if (this.BehaviorTreeRef.GameObjectRef.GetComponent<FalseKnight>().Grounded)
            {
                if (this.BehaviorTreeRef.GameObjectRef.transform.localScale.x > 0)
                    this.BehaviorTreeRef.GameObjectRef.transform.localScale
                        = new Vector3(this.BehaviorTreeRef.GameObjectRef.transform.localScale.x * -1,
                            this.BehaviorTreeRef.GameObjectRef.transform.localScale.y, this.BehaviorTreeRef.GameObjectRef.transform.localScale.z);

                this.BehaviorTreeRef.GameObjectRef.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 10);
                this.BehaviorTreeRef.GameObjectRef.GetComponent<Animator>().SetTrigger("Jump");
                this.BehaviorTreeRef.GameObjectRef.GetComponent<FalseKnight>().Grounded = false;
                
                return State.Success;
            }
            return State.Failure;
        }
    }
}