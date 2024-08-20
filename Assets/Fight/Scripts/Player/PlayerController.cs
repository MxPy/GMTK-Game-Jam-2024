using System;
using UnityEngine;

namespace TarodevController
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats;
        private Rigidbody2D _rb;
        public static PlayerController Instance { get; private set; }
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private VariableTimer timer;
        private bool _cachedQueryStartInColliders;
        private bool isFacingRight = true;

        public GameObject lastTarget;

        bool disableIdle = false;

        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;
        public event Action Dashed;
        //public GameObject SideDmgR, SideDmgL;

        #endregion

        private float _time;

        private void Awake()
        {
            timer = gameObject.AddComponent(typeof(VariableTimer)) as VariableTimer;
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();
            Instance = this;

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        public void Death(){
            Debug.Log("player died");
        }
        

        private void Update()
        {
            _time += Time.deltaTime;
            GatherInput();
            HandleAttack();
            if (_frameInput.Move.x == 0)
            {
                if(!disableIdle){
                    GetComponent<Animator>().Play("idle");
                }
                
            }
            else
            {
                GetComponent<Animator>().Play("playerMovment");
            }
            

        }

        public void PreHid(){
            disableIdle = true;
        }

        public void Hid(){
                Debug.Log("fdghfgh");
                GetComponent<Animator>().Play("idle");
                lastTarget.GetComponent<change>().exchange(0);
                disableIdle = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<PlayerController>().enabled = false;
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                JumpHeld = Input.GetButton("Jump") || Input.GetKeyDown(KeyCode.C),
                Move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")),
                DashDown = Input.GetButtonDown("Fire3") || Input.GetKeyDown(KeyCode.LeftShift),
                DashHeld = Input.GetButton("Fire3") || Input.GetKeyDown(KeyCode.LeftShift),
                AttackDown = Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Mouse0),
                AttackHeld = Input.GetButton("Fire1") || Input.GetKeyDown(KeyCode.Mouse0)
            };

            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            if (_frameInput.JumpDown)
            {
                _jumpToConsume = true;
                _timeJumpWasPressed = _time;
            }

            if (_frameInput.DashDown)
            {
                _dashToConsume = true;
                _timeDashWasPressed = _time;
            }
            
        }

        private void flipSprite()
        {
            if(isFacingRight){
                GetComponent<SpriteRenderer>().flipX = false;
            }else{
                GetComponent<SpriteRenderer>().flipX = true;
            }
            isFacingRight = !isFacingRight;
        }

        private void FixedUpdate()
        {
            CheckCollisions();

            HandleJump();
            HandleDash();
            HandleDirection();
            HandleGravity();
            
            
            ApplyMovement();
        }

        #region Collisions
        
        private float _frameLeftGrounded = float.MinValue;
        public bool _grounded;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

           // Tworzenie maski warstw, która wyklucza warstwę "rtfthrfth"
            int layerMask = ~(_stats.PlayerLayer | (1 << LayerMask.NameToLayer("rtfhrfth")));

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, layerMask);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, layerMask);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0) _endedJumpEarly = true;

            if (!_jumpToConsume && !HasBufferedJump) return;

            if (_grounded || CanUseCoyote) ExecuteJump();

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        #region Attack
        private void HandleAttack(){
            if (_frameInput.AttackDown && timer.started == false){
                timer.StartTimer(0.5f);
                // if(isFacingRight){
                //     SideDmgR.SetActive(true);
                // }else{
                //     SideDmgL.SetActive(true);
                // }
                
            }
            if(timer.finished){
                // SideDmgR.SetActive(false);
                // SideDmgL.SetActive(false);
                timer.ResetTimer();
            }
        }

        #endregion

        #region Dash

        private bool _dashToConsume;
        private bool _bufferedDashUsable;
        private bool _endedDashEarly;
        private float _timeDashWasPressed;

        private bool HasBufferedDash => _bufferedDashUsable && _time < _timeDashWasPressed + _stats.JumpBuffer;

        private void HandleDash()
        {
            if (!_endedDashEarly &&  _rb.velocity.y > 0) _endedDashEarly = true;

            if (!_dashToConsume && !HasBufferedDash) return;

            ExecuteDash();

            _dashToConsume = false;
        }

        private void ExecuteDash()
        {
            //GetComponent<Animator>().Play("dash");
            _endedDashEarly = false;
            _timeDashWasPressed = 0;
            _bufferedDashUsable = false;
            if(isFacingRight){
                _frameVelocity.x = _stats.JumpPower;
            }else{
                _frameVelocity.x = -_stats.JumpPower;
            }
            
            Dashed?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
                
            }
            else
            {
                
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }

            if (_frameInput.Move.x > 0 && !isFacingRight)    flipSprite();
            else if(_frameInput.Move.x < 0 && isFacingRight) flipSprite();

        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }

    public struct FrameInput
    {
        public bool JumpDown;
        public bool JumpHeld;
        public Vector2 Move;
        public bool DashDown;
        public bool DashHeld;
        public bool AttackDown;
        public bool AttackHeld;
    }

    public interface IPlayerController
    {
        public event Action<bool, float> GroundedChanged;

        public event Action Jumped;
        public event Action Dashed;
        public Vector2 FrameInput { get; }
    }

    
}