using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer; // LineRenderer for drawing the trajectory

    [SerializeField]
    [Range(3, 100)]
    private int lineSegmentCount = 28; // Number of segments for the line

    private List<Vector3> linePoints = new List<Vector3>(); // List to store the trajectory points

    #region Singleton
    public static DrawTrajectory Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion
 
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        // Calculate initial velocity
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

        // Calculate flight duration using the formula (2 * initial velocity in y) / gravity
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        // Determine the time step for each segment
        float stepTime = FlightDuration / lineSegmentCount;

        // Clear previous points
        linePoints.Clear();

        for (int i = 0; i < lineSegmentCount; i++)
        {
            // Calculate the time passed at this segment
            float stepTimePassed = stepTime * i;

            // Calculate movement vector
            Vector3 movementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed
            );

            // Calculate the new position
            Vector3 newPosition = -movementVector + startingPoint;

            // Check for collisions using a raycast
            if (Physics.Raycast(origin: startingPoint, direction: -movementVector.normalized, out RaycastHit hit, movementVector.magnitude))
            {
                // If a collision is detected, stop the calculation
                break;
            }

            // Add the new position to the list of points
            linePoints.Add(newPosition);
        }

        // Update the line renderer with the calculated points
        _lineRenderer.positionCount = linePoints.Count;
        _lineRenderer.SetPositions(linePoints.ToArray());
    }
    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}


    



















/*public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer; // LineRenderer for drawing the trajectory

    [SerializeField]
    [Range(3, 30)]
    private int lineSegmentCount = 28; // Number of segments for the line

    private List<Vector3> linePoints = new List<Vector3>(); // List to store the trajectory points

    
    #region Singleton
    
    public static DrawTrajectory Instance;

    
    private void Awake()
    {
        Instance = this;
    }
    #endregion
    
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        // Calculate initial velocity
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

        // Calculate flight duration using the formula (2 * initial velocity in y) / gravity
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        // Determine the time step for each segment
        float stepTime = FlightDuration / _lineSegmentCount;

        // Clear previous points
        _linePoints.Clear();

        for (int i = 0; i < _lineSegmentCount; i++)
        {
            // Calculate the time passed at this segment
            float stepTimePassed = stepTime * i;

            // Calculate movement vector
            Vector3 MovementVector = new Vector3(
                x: velocity.x * stepTimePassed,
                y: velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                z: velocity.z * stepTimePassed
            );

            // Calculate the new position
            Vector3 newPosition = MovementVector + startingPoint;

            // Check for collisions using a raycast
            RaycastHit hit;
            if (Physics.Raycast(origin: startingPoint, direction: MovementVector.normalized, out hit, MovementVector.magnitude))
            {
                // If a collision is detected, stop the calculation
                break;
            }

            // Add the new position to the list of points
            _linePoints.Add(newPosition);
        }

        // Update the line renderer with the calculated points
        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}*/
/*    public class DrawTrajectory : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _lineRenderer; // LineRenderer para dibujar la trayectoria

        [SerializeField]
        [Range(3, 30)]
        private int _lineSegmentCount = 28; // Número de segmentos para la línea

        private List<Vector3> _linePoints = new List<Vector3>(); // Lista para almacenar los puntos de la trayectoria

        #region Singleton
        public static DrawTrajectory Instance;
        private void Awake()
        {
            Instance = this;
        }
        #endregion

        public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
        {
            // Ajustar el vector de fuerza para que apunte hacia adelante y hacia arriba
            Vector3 adjustedForceVector = new Vector3(forceVector.x, Mathf.Abs(forceVector.y), Mathf.Abs(forceVector.z));

            // Calcular la velocidad inicial basada en el vector de fuerza ajustado
            Vector3 velocity = (adjustedForceVector / rigidBody.mass) * Time.fixedDeltaTime;

            // Otros cálculos permanecen igual
            float flightDuration = (2 * velocity.y) / Physics.gravity.y;
            float stepTime = flightDuration / _lineSegmentCount;
            _linePoints.Clear();

            for (int i = 0; i < _lineSegmentCount; i++)
            {
                float stepTimePassed = stepTime * i;
                Vector3 movementVector = new Vector3(
                    velocity.x * stepTimePassed,
                    velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                    velocity.z * stepTimePassed  // Asegúrate de que esto se calcula correctamente
                );

                Vector3 newPosition = startingPoint + movementVector;
                _linePoints.Add(newPosition);
            }

            _lineRenderer.positionCount = _linePoints.Count;
            _lineRenderer.SetPositions(_linePoints.ToArray());
        }

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}


/*public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
{
    // Ajustar el vector de fuerza para que apunte hacia adelante y hacia arriba
    Vector3 adjustedForceVector = new Vector3(forceVector.x, Mathf.Abs(forceVector.y), Mathf.Abs(forceVector.z));

    // Calcular la velocidad inicial basada en el vector de fuerza ajustado
    Vector3 velocity = (adjustedForceVector / rigidBody.mass) * Time.fixedDeltaTime;

    // Otros cálculos permanecen igual
    float flightDuration = (2 * velocity.y) / Physics.gravity.y;
    float stepTime = flightDuration / _lineSegmentCount;
    _linePoints.Clear();

    for (int i = 0; i < _lineSegmentCount; i++)
    {
        float stepTimePassed = stepTime * i;
        Vector3 movementVector = new Vector3(
            velocity.x * stepTimePassed,
            velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
            velocity.z * stepTimePassed  // Asegúrate de que esto se calcula correctamente
        );

        Vector3 newPosition = startingPoint + movementVector;
        _linePoints.Add(newPosition);
    }

    _lineRenderer.positionCount = _linePoints.Count;
    _lineRenderer.SetPositions(_linePoints.ToArray());
}*/