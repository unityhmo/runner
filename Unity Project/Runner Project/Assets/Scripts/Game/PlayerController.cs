using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterAnimation _anim;
    private CharacterFX _fx;
    private CharacterController _contr;
    private GameController _gameController;

    [SerializeField] private bool _isPlayable = false;
    [SerializeField] private float _runSpeed = 8f;
    private bool _isJumping = false;
    [SerializeField] private float _jumpSpeed = 10f;
    static float _currentJumpSpeed = 0f;
    static float _jumpY;
    [SerializeField] private float _sideJumpDistance = 2f;
    [SerializeField] private float _invincibility = 1f;
    private float _currentInvincibility;
    private bool _isInvincible = false;
    private Vector3 _newLerpPosition = Vector3.zero;
    private bool _lerpInAction = false;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isGameOver = false;

    public bool IsPlayable
    {
        get { return _isPlayable; }
    }

    public bool IsGameOver
    {
        get { return _isGameOver; }
    }

    void Awake()
    {
        _anim = transform.GetComponent<CharacterAnimation>();
        _fx = transform.GetComponent<CharacterFX>();
        _contr = transform.GetComponent<CharacterController>();
        _gameController = GameObject.FindGameObjectWithTag(BaseValues.TAG_GAME_CONTROLLER).transform
            .GetComponent<GameController>();

        _fx.SetInvincibilityTimer(_invincibility);
    }

    void Update()
    {
        // We wont do a thing until the game is playable.
        if (_isPlayable)
        {
            ForwardMovement();
            SideJumpMovement();
        }
    }

    void LateUpdate()
    {
        if (_isPlayable)
        {
            // Here we only check if player isn't falling.
            // Is it cool to have a fixed vertical value to verify this?? /shrugs
            if (transform.position.y < -1.25f)
            {
                _gameController.RegisterDamage(0, true);
                EndGame();

                Invoke("DestroyMe", 2f);
            }

            // After player is hit, hmoman becomes invincible for _invincibility time
            if (_isInvincible)
            {
                if (_currentInvincibility >= _invincibility)
                {
                    _currentInvincibility = 0f;
                    _isInvincible = false;
                }
                else
                {
                    _currentInvincibility += Time.deltaTime;
                }
            }
        }
    }

    // Accesed by GameController, he knows better when the show can start
    public void StartGame()
    {
        _isPlayable = true;
        _anim.SetRunning(true);
    }

    // GameInput actions---------------------INI
    public void JumpLeft()
    {
        if (_isPlayable && _contr.isGrounded && !_lerpInAction)
        {
            _newLerpPosition = transform.position + new Vector3(-_sideJumpDistance, 0, 0);

            _anim.JumpLeft();
            _lerpInAction = true;

            _fx.Jump();
            AudioManager.GetCharFX().Dash();

            _fx.CreateDash(false);
        }
    }

    public void JumpRight()
    {
        if (_isPlayable && _contr.isGrounded && !_lerpInAction)
        {
            _newLerpPosition = transform.position + new Vector3(_sideJumpDistance, 0, 0);

            _anim.JumpRight();
            _lerpInAction = true;

            _fx.Jump();
            AudioManager.GetCharFX().Dash();

            _fx.CreateDash(true);
        }
    }

    public void JumpUp()
    {
        if (_isPlayable && _contr.isGrounded && !_lerpInAction)
        {
            _isJumping = true;
            _anim.JumpUp();

            _fx.Jump();
            AudioManager.GetCharFX().Jump();
        }
    }
    // GameInput actions---------------------END

    // It all ends here, good or bad the outcome.
    private void EndGame(bool isWinner = false)
    {
        _isGameOver = true;
        _isPlayable = false;
        _anim.SetRunning(false);

        if (isWinner)
            ReachedGoal();
        else
            ReachedDeath();

        _fx.SetLowLife(false);
    }

    // Moving forward and jumping up is handled here
    private void ForwardMovement()
    {
        // Forward movement
        _moveDirection = new Vector3(0, 0, 1);
        _moveDirection = transform.TransformDirection(_moveDirection);
        _moveDirection *= _runSpeed;

        // Jumping movement.
        if (_isJumping)
        {
            _jumpY = Mathf.Lerp(_jumpSpeed, 0, _currentJumpSpeed);
            _currentJumpSpeed += 2f * Time.deltaTime;
            _moveDirection.y = _jumpY;

            if (_jumpY < 0.1f)
            {
                _isJumping = false;
                _currentJumpSpeed = 0f;
            }
        }

        // Apply movement to character controller
        _contr.Move(_moveDirection * Time.deltaTime);
    }

    // Side jumping animation is handled here
    private void SideJumpMovement()
    {
        if (_lerpInAction)
        {
            transform.position = Vector3.Lerp(transform.position, _newLerpPosition, Time.deltaTime * _runSpeed);

            if (Mathf.Abs(Mathf.Abs(transform.position.x) - Mathf.Abs(_newLerpPosition.x)) < 0.2f)
            {
                _lerpInAction = false;
                transform.position = new Vector3(_newLerpPosition.x, transform.position.y, transform.position.z);
            }
        }
    }

    // Goal reached. Let gamecontroller know dummy!
    private void ReachedGoal()
    {
        _gameController.Win();
        _anim.Victory();

        Camera.main.GetComponent<CameraFollow>().VictoryZoom();
    }

    // Well, you lose. Lets play you a death animation.
    private void ReachedDeath()
    {
        _gameController.Lose();
        _anim.Death();
    }

    public GameController GetGameController()
    {
        return _gameController;
    }

    /*
     * Collision handler. Here we notify collisioned elements and also apply changes in our player and game controller
     */
    void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.tag == BaseValues.TAG_ORB_PICKUP)
        {
            _gameController.PickUpOrb();
            _fx.PickUpFX();
        }
        else if (go.tag == BaseValues.TAG_OBSTACLE && !_isInvincible)
        {
            Obstacle obstacleComp = go.GetComponent<Obstacle>();

            if (obstacleComp != null)
            {
                _gameController.RegisterDamage(obstacleComp.Damage);
            }
            else
            {
                ObstacleCollider colliderComp = go.GetComponent<ObstacleCollider>();
                _gameController.RegisterDamage(colliderComp.Damage);
            }

            AudioManager.GetCharFX().Damage();
            _fx.Damage();

            if (_gameController.GetCurrentHP() > 0)
            {
                _anim.Damage();

                if (_gameController.GetCurrentHP() == 1)
                    _fx.SetLowLife(true);

                _isInvincible = true;
            }
            else
            {
                _fx.CreateHitsFX();
                EndGame();
            }
        }
        else if (go.tag == BaseValues.TAG_GOAL)
            EndGame(true);

        /*
         * SendMessage without receiver required, this way we wont have errors if no receiver is previously prepared in collisioned object
         */
        go.SendMessage(BaseValues.RECEIVER_COLLISION_DETECTED, SendMessageOptions.DontRequireReceiver);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }
}