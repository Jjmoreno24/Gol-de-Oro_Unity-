using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DragAndShoot : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    public AudioSource audioSource; // Asegúrate de asignar esto en el Inspector
    public Animator goalkeeperAnimator;  // Añade esta línea para tener acceso al Animator del portero
    private Rigidbody rb;

    private bool isShoot;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        Vector3 forceInt = (Input.mousePosition - mousePressDownPos);
        Vector3 forceV = (new Vector3(forceInt.x, forceInt.y, forceInt.z)) * forceMultiplier;

        if (!isShoot)
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
       
    }

    private void OnMouseUp()
    {
        DrawTrajectory.Instance.HideLine();
        mouseReleasePos = Input.mousePosition;
        //Shoot(mouseReleasePos - mousePressDownPos);

        // Retrasar el disparo para permitir que la animación comience
        Invoke("DelayedShoot", 0.5f); // Ajusta este tiempo según sea necesario
    }

    [SerializeField]
    private float forceMultiplier = 2; //3
    /*void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;

        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        isShoot = true;

        audioSource.Play(); // Reproduce el sonido de patear la pelota

        DuplicateBall.Instance.NewSpawnRequest();
    }*/

    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;

        rb.AddForce(new Vector3(Force.x, Force.y, Force.y) * forceMultiplier);
        isShoot = true;
        audioSource.Play();

        // Asumiendo que Force.x te da la dirección horizontal del tiro
        if (Mathf.Abs(Force.x) > Mathf.Abs(Force.z))  // Evalúa la dirección predominante del tiro
        {
            if (Force.x > 0)
                goalkeeperAnimator.SetTrigger("DiveRight");  // Tiro a la derecha
            else
                goalkeeperAnimator.SetTrigger("DiveLeft");   // Tiro a la izquierda
        }
        else
        {
            goalkeeperAnimator.SetTrigger("Jump");  // Tiro central o alto
        }

        DuplicateBall.Instance.NewSpawnRequest();  // Solicita una nueva pelota si es necesario
    }

    void DelayedShoot()
    {
        Shoot(mouseReleasePos - mousePressDownPos);
    }
}