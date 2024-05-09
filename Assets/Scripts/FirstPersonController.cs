using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5.0f;
    public float sensitivity = 2.0f;
    public float jumpSpeed = 5.0f;
    private CharacterController characterController;
    private float rotationX = 0;
    private Vector3 velocity; // 중력과 점프 속도를 처리하기 위한 변수

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 이동
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;

        // 점프
        if (characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpSpeed;
        }
        else if (characterController.isGrounded)
        {
            velocity.y = -2f; // 점프하지 않을 때 캐릭터가 땅에 단단히 붙어 있도록 합니다.
        }

        // 중력 적용
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        characterController.Move(movement * Time.deltaTime); // 이동 로직은 중력 적용 후에 실행

        // 시야 회전
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, -90, 90);
        transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity, 0);
    }
}
