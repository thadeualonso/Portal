using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float MouseSensitivity { get; } = 200f;
    private float MoveSpeed { get; } = 300f;
    private float RotateSpeed { get; } = 70f;

    private Rigidbody Rigidbody { get; set; }

    [SerializeField]
    private GameObject portalprefab; // preguiça
    private Vector3 rotation;

    private void FixedUpdate()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        Vector3 velocityDirection = Vector3.zero;
        velocityDirection += transform.forward * verticalAxis;
        if (Input.GetKey(KeyCode.Q))
        {
            velocityDirection += -transform.right;
        }
        if (Input.GetKey(KeyCode.E))
        {
            velocityDirection += transform.right;
        }
        Rigidbody.velocity = velocityDirection.normalized * Time.deltaTime * MoveSpeed;
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        rotation = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        rotation.y += horizontalAxis * Time.deltaTime * RotateSpeed;
        rotation.y += Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity;
        rotation.x += -Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity;
        rotation.x = Mathf.Clamp(rotation.x, -30f, 30f);
        transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        Camera.main.transform.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool hit = Physics.Raycast(ray, out RaycastHit raycastHit);
            if (hit && raycastHit.collider.gameObject.CompareTag("PortalPlace"))
            {
                Instantiate(portalprefab, raycastHit.collider.transform.position, raycastHit.collider.transform.rotation);
            }
        }
        if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.X))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            bool hit = Physics.Raycast(ray, out RaycastHit raycastHit);
            if (hit && raycastHit.collider.gameObject.CompareTag("PortalPlace"))
            {
                Debug.Log("Portal 2");
            }
        }
    }
}
