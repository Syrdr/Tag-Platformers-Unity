using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jumpSpeed = 5f;
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    [SerializeField] private GameObject pipe_1;
    [SerializeField] private GameObject pipe_2;
    [SerializeField] private float teleportOffset = 5;
    [SerializeField] private CharacterType characterType;
    [SerializeField] private AudioSource rocketBlastoff;
    [SerializeField] private AudioSource rocketSlowDown;
    private playerControls p1;
    private playerControls p2;
    private Keyboard keyboard;
    private bool isMovingUp;
    private bool isMovingDown;

    // Ground check for Runner
    //[SerializeField] private Transform groundCheck;
    //[SerializeField] private float groundCheckRadius = 0.1f;
    //private bool isGrounded;
    public Vector3 startingPos;
    public Vector3 startingRot;

    [SerializeField] private GameManager gameManager;

    void Start()
    {
        //Startingtransform.SetPositionAndRotation(transform.position, transform.rotation);
        keyboard = Keyboard.current;
        if (keyboard == null) return;

        rb = GetComponent<Rigidbody2D>();

        // Player 1: Arrow keys
        p1 = new playerControls
        {
            upMovement = () => keyboard.upArrowKey.isPressed,
            downMovement = () => keyboard.downArrowKey.isPressed,
            leftMovement = () => keyboard.leftArrowKey.isPressed,
            rightMovement = () => keyboard.rightArrowKey.isPressed
        };

        // Player 2: WASD
        p2 = new playerControls
        {
            upMovement = () => keyboard.wKey.isPressed,
            downMovement = () => keyboard.sKey.isPressed,
            leftMovement = () => keyboard.aKey.isPressed,
            rightMovement = () => keyboard.dKey.isPressed
        };
    }

    void FixedUpdate()
    {
        // Update grounded status if Runner
        /*if (characterType == CharacterType.Runner && groundCheck != null)
        {
            // Check for any collider under feet
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius) != null;
        }*/

        playerControls controls = GetActiveControls();
        if (controls != null)
        {
            HandlePlayerMovement(controls);
        }
    }

    private playerControls GetActiveControls()
    {
        if (GameData.itPlayer == ItPlayer.Player1)
            return (characterType == CharacterType.Rocket) ? p1 : p2;
        else if (GameData.itPlayer == ItPlayer.Player2)
            return (characterType == CharacterType.Rocket) ? p2 : p1;
        return null;
    }

    private void HandlePlayerMovement(playerControls controls)
    {
        if (characterType == CharacterType.Rocket)
        {

            if (controls.rightMovement())
                rb.MoveRotation(rb.rotation - turnSpeed);

            if (controls.leftMovement())
                rb.MoveRotation(rb.rotation + turnSpeed);
            // Rocket: local up movement + rotation
            if (controls.upMovement())
            { rb.linearVelocity += jumpSpeed * (Vector2)transform.up;   if (isMovingUp) { return; } else { isMovingUp = true; rocketBlastoff.Play();}; return; }

            if (controls.downMovement())
            {  rb.linearVelocity -= jumpSpeed * (Vector2)transform.up; if (!isMovingDown) { rocketSlowDown.Play(); } else { isMovingDown = true; }; return; }
            isMovingDown = false;
            isMovingUp = false;
        }
        else if (characterType == CharacterType.Runner)
        {
            // Runner: world-space movement
            Vector2 velocity = rb.linearVelocity;

            // Vertical movement (jump only if grounded)
            if (controls.upMovement())
                velocity.y = jumpSpeed;

            if (controls.downMovement())
                velocity.y = -jumpSpeed;

            // Horizontal movement
            velocity.x = 0f; // reset horizontal velocity
            if (controls.rightMovement())
                velocity.x += moveSpeed;
            if (controls.leftMovement())
                velocity.x -= moveSpeed;

            rb.linearVelocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == pipe_1)
        {
            transform.position = pipe_2.transform.position + new Vector3(0, teleportOffset, 0);
            return;
        }
        else if (collision.gameObject == pipe_2)
        {
            transform.position = pipe_1.transform.position + new Vector3(0, teleportOffset, 0);
            return;
        }
        if (characterType == CharacterType.Runner && (collision.gameObject.CompareTag("Rocket") || collision.gameObject.CompareTag("Lava")) || (gameManager.timeElapsed > GameData.firstPlayerTime))
        {
            if (GameData.roundsLeft == 1)
            {
                GameData.itPlayer = (GameData.itPlayer == ItPlayer.Player1) ? ItPlayer.Player2 : ItPlayer.Player1;
                transform.position = startingPos;
                transform.rotation = Quaternion.Euler(startingRot);
                GameData.firstPlayerTime = gameManager.timeElapsed;
                gameManager.timeElapsed = 0;
                collision.gameObject.transform.SetPositionAndRotation(collision.gameObject.GetComponent<CharacterControl>().startingPos, Quaternion.Euler(collision.gameObject.GetComponent<CharacterControl>().startingRot));
                GameData.TriggerItPlayerChanged();
                GameData.roundsLeft--;
                foreach(AudioSource audio in FindObjectsOfType<AudioSource>())
                {
                    audio.Stop();
                }
                return;
            }
                Debug.Log("Else statement entered");
                GameData.secondPlayerTime = gameManager.timeElapsed;
                if(GameData.firstPlayerTime < GameData.secondPlayerTime)
                {
                    GameData.winner = ItPlayer.Player1;
                }
                else if(GameData.firstPlayerTime > GameData.secondPlayerTime)
                {
                    GameData.winner = ItPlayer.Player2;
                }
                else
                {
                    GameData.winner = ItPlayer.None;
                }
                
                UnityEngine.SceneManagement.SceneManager.LoadScene("EndScene");
        }
    }

    public enum CharacterType
    {
        Runner,
        Rocket
    }

    public class playerControls
    {
        public Func<bool> upMovement;
        public Func<bool> downMovement;
        public Func<bool> leftMovement;
        public Func<bool> rightMovement;
    }
}