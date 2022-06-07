using UnityEngine;

namespace Examples.Example_1.FalseKnight
{
    public class FalseKnight : MonoBehaviour
    {
        public bool Grounded { get; private set; }

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