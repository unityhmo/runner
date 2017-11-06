using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private CharacterAnimation anim;
  private CharacterFX fx;
  private CharacterController contr;
  private GameController gameController;

  [SerializeField]
  private bool isPlayable = false;
  [SerializeField]
  private float runSpeed = 8;
  private bool isJumping = false;
  [SerializeField]
  private float jumpSpeed = 10f;
  static float currentJumpSpeed = 0f;
  static float jumpY;
  [SerializeField]
  private float sideJumpDistance = 2f;
  private Vector3 newLerpPosition = Vector3.zero;
  private bool lerpInAction = false;

  private Vector3 moveDirection = Vector3.zero;

  void Awake()
  {
    anim = transform.GetComponent<CharacterAnimation>();
    fx = transform.GetComponent<CharacterFX>();
    contr = transform.GetComponent<CharacterController>();
    gameController = GameObject.FindGameObjectWithTag("GameController").transform.GetComponent<GameController>();
  }

  void Update()
  {
    if (isPlayable)
    {
      forwardMovement();
      sideJumpMovement();
    }
  }
  void LateUpdate()
  {
    if (isPlayable && transform.position.y < -1f)
    {
      gameController.registerDamage(true);
      endGame();
    }
  }

  public void startGame()
  {
    isPlayable = true;
    anim.setRunning(true);
  }

  public void jumpLeft()
  {
    if (isPlayable && contr.isGrounded && !lerpInAction)
    {
      newLerpPosition = transform.position + new Vector3(-sideJumpDistance, 0, 0);

      anim.jumpLeft();
      lerpInAction = true;

      fx.jump();
    }
  }
  public void jumpRight()
  {
    if (isPlayable && contr.isGrounded && !lerpInAction)
    {
      newLerpPosition = transform.position + new Vector3(sideJumpDistance, 0, 0);

      anim.jumpRight();
      lerpInAction = true;

      fx.jump();
    }
  }
  public void jumpUp()
  {
    if (isPlayable && contr.isGrounded && !lerpInAction)
    {
      isJumping = true;
      anim.jumpUp();

      fx.jump();
    }
  }

  private void endGame(bool isWinner = false)
  {
    isPlayable = false;
    anim.setRunning(false);

    if (isWinner)
      reachedGoal();
    else
      reachedDeath();
  }
  private void forwardMovement()
  {
    // Forward movement
    moveDirection = new Vector3(0, 0, 1);
    moveDirection = transform.TransformDirection(moveDirection);
    moveDirection *= runSpeed;

    // Jumping movement.
    if (isJumping)
    {
      jumpY = Mathf.Lerp(jumpSpeed, 0, currentJumpSpeed);
      currentJumpSpeed += 2f * Time.deltaTime;
      moveDirection.y = jumpY;

      if (jumpY < 0.1f)
      {
        isJumping = false;
        currentJumpSpeed = 0f;
      }
    }

    // Apply movement to character controller
    contr.Move(moveDirection * Time.deltaTime);
  }
  private void sideJumpMovement()
  {
    if (lerpInAction)
    {
      transform.position = Vector3.Lerp(transform.position, newLerpPosition, Time.deltaTime * runSpeed);

      if (Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(newLerpPosition.x)) < 0.2f)
      {
        lerpInAction = false;
        transform.position = new Vector3(newLerpPosition.x, transform.position.y, transform.position.z);
      }
    }
  }
  private void reachedGoal()
  {
    gameController.win();
    anim.victory();
  }
  private void reachedDeath()
  {
    gameController.lose();
    anim.death();
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.tag == "Goal")
      endGame(true);

    if (other.gameObject.tag == "Powerup")
      gameController.pickUpPowerUp();

    if (other.gameObject.tag == "Enemy")
    {
      gameController.registerDamage();

      if (gameController.getCurrentHP() > 0)
      {
        anim.damage();
        fx.damage();
      }
      else
        endGame();
    }

    other.gameObject.SendMessage("collisionDetected", SendMessageOptions.DontRequireReceiver);
  }
}
