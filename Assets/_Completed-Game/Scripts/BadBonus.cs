using UnityEngine;

namespace Geekbrains
{
    public sealed class BadBonus : InteractiveObject, IFly
    {
        private float _speedRotation;
        private float _lengthFlay;


        private void Awake()
        {
            _lengthFlay = Random.Range(5.0f, 10.0f);
            

        }

        public void Fly()
        {
            transform.localPosition = new Vector3(Mathf.PingPong(Time.time*5, _lengthFlay), transform.localPosition.y, transform.localPosition.z);
        }

    }
}
