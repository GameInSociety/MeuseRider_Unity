using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public static PathFollower Instance;


        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public float distanceTravelled;

        public bool move = false;

        private void Awake()
        {
            Instance = this;
        }

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;

            }


        }


        public void StartMovement()
        {
            move = true;
            distanceTravelled = 0f;
        }

        public void UpdateMovement()
        {
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        }

        void Update()
        {
            if (pathCreator != null && move )
            {
                distanceTravelled += speed * Time.deltaTime;
                UpdateMovement();
                //transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}