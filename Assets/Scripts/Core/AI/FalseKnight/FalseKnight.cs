using UnityEngine;

namespace Examples.Example_1.FalseKnight
{
    public class FalseKnight : MonoBehaviour
    {
        public GameObject GameObject;
        public Animator Animator;
        public Rigidbody2D Rigidbody2D;
        public BoxCollider2D BoxCollider2D;

        [HideInInspector]
        public bool Grounded;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == Constants.GroundLayer)
                Grounded = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.layer == Constants.GroundLayer)
                Grounded = false; 
        }
    }
}