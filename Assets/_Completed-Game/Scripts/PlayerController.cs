using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

namespace Geekbrains
{

	public class PlayerController : MonoBehaviour
	{

		// Create public variables for player speed, and for the Text UI game objects
		public float speed;
		public Text countText;
		public Text winText;
		private InteractiveObject[] _interactiveObjects;

		// Create private references to the rigidbody component on the player, and the count of pick up objects picked up so far
		private Rigidbody rb;
		private int count;

        private void Awake()
        {
			_interactiveObjects = FindObjectsOfType<InteractiveObject>();

		}

		// At the start of the game..
		void Start()
		{

			rb = GetComponent<Rigidbody>();
			count = 0;
			SetCountText();
			winText.text = "";
		}

		
		void FixedUpdate()
		{
			
			float moveHorizontal = Input.GetAxis("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
			rb.AddForce(movement * speed);
		}

		private void Update()
		{
			for (var i = 0; i < _interactiveObjects.Length; i++)
			{
				var interactiveObject = _interactiveObjects[i];

				if (interactiveObject == null)
				{
					continue;
				}

				if (interactiveObject is IFly flay)
				{
					flay.Fly();
				}

				if (interactiveObject is IRotation rotation)
				{
					rotation.Rotation();
				}
			}
		}

		
		void OnTriggerEnter(Collider other)
		{
			
			if (other.gameObject.CompareTag("Pick Up"))
			{
				
				other.gameObject.SetActive(false);
				count = count + 1;
				SetCountText();
			}

			if (other.gameObject.CompareTag("Pick Down"))
			{

				other.gameObject.SetActive(false);
				count = count - 1;
				speed = speed + 10;
				SetCountText();
			}
		}

		// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
		void SetCountText()
		{
			countText.text = "Count: " + count.ToString();
			if (count >= 3)
			{
				winText.text = "You Win!";
			}
		}
	}
}