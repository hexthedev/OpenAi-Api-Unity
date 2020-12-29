using UnityEngine;

namespace OpenAi.Api.Test
{
    public class EmptyMono : MonoBehaviour
    {
        public void DestroySelf() => Destroy(gameObject);
    }
}