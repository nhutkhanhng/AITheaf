using UnityEngine;

namespace CoverShooter
{
    /// <summary>
    /// Base parent class for some AI components.
    /// </summary>
    public class AIBase : MonoBehaviour, kINetworkUpdate
    {
        public virtual void FixedUpdateNetwork()
        {
            
        }

        public virtual void kRender()
        {
            
        }

        /// <summary>
        /// Sends a message to other components.
        /// </summary>
        public void Message(string name)
        {
            SendMessage(name, SendMessageOptions.DontRequireReceiver);
        }

        /// <summary>
        /// Sends a message to other components.
        /// </summary>
        public void Message(string name, object value)
        {
            SendMessage(name, value, SendMessageOptions.DontRequireReceiver);
        }

        public virtual void Update()
        {
            FixedUpdateNetwork();
        }
    }
}
