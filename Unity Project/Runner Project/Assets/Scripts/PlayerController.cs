using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private CharacterAnimation anim;
  private CharacterFX fx;
  private CharacterController contr;
  private GameController gameController;

  [SerializeField] private bool isPlayable = false;
  [SerializeField] private float runSpeed = 8f;
  private bool isJumping = false;
  [SerializeField] private float jumpSpeed = 10f;
  static float currentJumpSpeed = 0f;
  static float jumpY;
  [SerializeField] private float sideJumpDistance = 2f;
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
    // We wont do a thing until the game is playable.
    if (isPlayable)
    {
      forwardMovement();
      sideJumpMovement();
    }
  }

  void LateUpdate()
  {
    // Here we only check if player isn't falling.
    // Is it cool to have a fixed vertical value to verify this?? /shrugs
    if (isPlayable && transform.position.y < -1.25f)
    {
      gameController.registerDamage(true);
      endGame();
    }
  }

  // Accesed by GameController, he knows better when the show can start
  public void startGame()
  {
    isPlayable = true;
    anim.setRunning(true);
  }

  // GameInput actions---------------------INI
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
  // GameInput actions---------------------END

  // It all ends here, good or bad the outcome.
  private void endGame(bool isWinner = false)
  {
    isPlayable = false;
    anim.setRunning(false);

    if (isWinner)
    {
      fx.setLowLife(false);
      reachedGoal();
    }
    else
      reachedDeath();
  }

  // Moving forward and jumping up is handled here
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

  // Side jumping animation is handled here
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

  // Goal reached. Let gamecontroller know dummy!
  private void reachedGoal()
  {
    gameController.win();
    anim.victory();
  }

  // Well, you lose. Lets play you a death animation.
  private void reachedDeath()
  {
    gameController.lose();
    anim.death();
  }

  /*
   * Collision handler. Here we notify collisioned elements and also apply changes in our player and game controller
   */
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

        if (gameController.getCurrentHP() == 1)
          fx.setLowLife(true);
      }
      else
        endGame();
    }

    /*
     * SendMessage without receiver required, this way we wont have errors if no receiver is previously prepared in collisioned object
     */
    other.gameObject.SendMessage("collisionDetected", SendMessageOptions.DontRequireReceiver);
  }
}
