using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float jumpSpeed = 5.0f;
    private CharacterController characterController;
    private float rotationX = 0;
    private Vector3 velocity; // �߷°� ���� �ӵ��� ó���ϱ� ���� ����

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // �̵�
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        // ����
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpSpeed;
        }
        else if (characterController.isGrounded)
        {
            velocity.y = -2f; // �������� ���� �� ĳ���Ͱ� ���� �ܴ��� �پ� �ֵ��� �մϴ�.
        }

        // �߷� ����
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        characterController.Move(movement * Time.deltaTime); // �̵� ������ �߷� ���� �Ŀ� ����

        // �þ� ȸ��
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, 0);
    }
}
