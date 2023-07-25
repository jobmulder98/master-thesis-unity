using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        public float delay = 0;
        public bool startAtEnd = false;
        public bool destroyAtStop = false;


        float distanceTravelled;
        private float delayTimer = 0f;
        private bool isDelayed = true;
        private bool isReversing = false;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }

            if (startAtEnd)
            {
                distanceTravelled = pathCreator.path.length; // Move to the end of the path
                isReversing = true;
            }
        }

        private void Update()
        {
            if (pathCreator != null)
            {
                if (isDelayed)
                {
                    delayTimer += Time.deltaTime;
                    if (delayTimer >= delay)
                    {
                        isDelayed = false;
                        delayTimer = 0f;
                    }
                }
                else
                {
                    if (startAtEnd)
                    {
                        distanceTravelled -= speed * Time.deltaTime;

                        if ((distanceTravelled <= 0f) && (endOfPathInstruction == EndOfPathInstruction.Stop) && (destroyAtStop))
                        {
                            Destroy(gameObject); 
                        }
                    }
                    else
                    {
                        distanceTravelled += speed * Time.deltaTime;

                        if ((distanceTravelled >= pathCreator.path.length) && (endOfPathInstruction == EndOfPathInstruction.Stop) && (destroyAtStop))
                        {
                            Destroy(gameObject);
                        }
                    }

                    transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                }
            }
        }

        private void OnPathChanged()
        {
            // Update the distance travelled to match the new path
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}