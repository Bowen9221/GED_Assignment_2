using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class WallMovement : MonoBehaviour
{
        public Vector3 direction;
        public float distance = 2f;
        public float extendTime = 0.3f;   // fast push
        public float retractTime = 1f;    // slow return
        public float minInterval = 1f;
        public float maxInterval = 4f;

        Rigidbody rb;
        Vector3 start;
        float progress;                   // 0 = retracted, 1 = extended
        float wait;
        int phase;                        // 0 = waiting, 1 = extending, 2 = retracting

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            start = rb.position;
            wait = Random.Range(minInterval, maxInterval);
            direction = gameObject.transform.forward;
        }

        void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;

            if (phase == 0)
            {
                wait -= dt;
                if (wait <= 0f) phase = 1;
                return;
            }

            if (phase == 1)
            {
                progress += dt / extendTime;
                if (progress >= 1f) { progress = 1f; phase = 2; }
            }
            else
            {
                progress -= dt / retractTime;
                if (progress <= 0f)
                {
                    progress = 0f;
                    phase = 0;
                    wait = Random.Range(minInterval, maxInterval);
                }
            }

            rb.MovePosition(start + direction.normalized * distance * progress);
        }
    }
