using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 유니티가 기본 제공 함수
    private void Awake()
    {
        ChangeState(eState.IDLE);
    }

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        LayerMask layerMask = 1 << LayerMask.NameToLayer("Ground");

        // Raycast : 선하나를 쐈을 때 붙이 치는것들을 검출한다. 타겟팅 , 적과의 거리 
        // 현재위치에서 부터 아래( 2.0f 거리 만큼, 레이어마스크인 그라운드를 넣는다)
        RaycastHit2D distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down,2.0f,layerMask);

        if(null != distanceFromGround.transform)
        {
            _isGround = true;
            gameObject.GetComponent<Animator>().SetBool("isGround", _isGround);
        }
        else
        {
            _isGround = false;
            gameObject.GetComponent<Animator>().SetBool("isGround", _isGround);
        }

        //
        if(eState.RUN == _state || eState.FEVER == _state)
        {
            if(_velocity.x <_maxSpeed)
            {
                _velocity.x += _addSpeed;
            }
            else
            {
                _velocity.x = _maxSpeed;
            }
            gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);

            // UpdateHP
            UpdateHP();

            // UpdateWeight
            //UpdateWeight();
            AddWeight(-_decreaseWeight);

            //Update Distance
            float deltaDistance = _velocity.x * Time.deltaTime;
            _distance += deltaDistance;

            //Debug.Log(_distance);
            if(_maxDistance < _distance)
            {
                ChangeState(eState.COMPLETE);
            }


        }
    }
  
    

    public enum eState
    {
        IDLE,
        RUN,
        DEAD,
        COMPLETE,
        FEVER,
    }

    eState _state = eState.IDLE;
    

    

   
    public void ChangeState(eState state)
    {
        _state = state;
        switch (state)
        {
            case eState.IDLE:
                // IDLE 애니메이션 실행 : 애니메이터에 isGround 패러미터를 true로 만들어 주면 된다.
                _velocity.x = 0.0f;
                _velocity.y = 0.0f;
                //gameObject.GetComponent<Animator>().SetBool("isGround", true);
                _isFever = false;

                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;
            case eState.RUN:
                // RUN 애니메이션 실행
                _velocity.x = 0.0f;
                _velocity.y = 0.0f;
                //gameObject.GetComponent<Animator>().SetBool("isGround", true);
                _isFever = false;
                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;
            case eState.DEAD:
                _velocity = Vector2.zero;
                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
               // IsDead();
                break;
            case eState.COMPLETE:
                _velocity = Vector2.zero;
                _isFever = false;

                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;
            case eState.FEVER:
                FeverTime();
                _isFever = true;
                gameObject.GetComponent<Animator>().SetFloat("Horizontal", _velocity.x);
                break;

        }

    }


    // Move

    Vector2 _velocity = Vector2.zero;

    bool _isGround = false;
    bool _canDoubleJump = false;

    public Vector2 GetVelocity()
    {
        return _velocity;
    }

    // JUMP

    void Jump()
    {
        if (_isGround)
        {
            if (_currentWeight < 70.0f)
            {
                _canDoubleJump = true;
            }
            else
            {
                _canDoubleJump = false;
            }
           
            JumpAction();
        }
        else if (true == _canDoubleJump)
        {
            _canDoubleJump = false;
            JumpAction();
        }
      
    }

    void JumpAction()
    {
        //float jumpPower = 500.0f;
        //gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        float jumpSpeed = 30.0f;
        Vector2 velocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        velocity.y = jumpSpeed;
        gameObject.GetComponent<Rigidbody2D>().velocity = velocity;
        gameObject.GetComponent<Animator>().SetTrigger("Jump");
    }

    //
    bool _isBlockSmash = false;
    void SetBlockSmash(bool isSmash)
    {
        _isBlockSmash = isSmash;
    }

    //
    void ResetSpeed()
    {
        if(false == _isFever)
        {
            _velocity.x = 3.0f;
        }
    }

    public bool IsDead()
    {
        return (eState.DEAD == _state);
    }

    // HP
    float _currentHP = 100.0f;
    float _decreaseHP = 0.1f;
    float _maxHP = 100.0f;

    public float GetMaxHP()
    {
        return _maxHP;
    }
    public float GetCurrentHp()
    {
        return _currentHP;
    }

    void UpdateHP()
    {
        _currentHP -= _decreaseHP;
        if (0 > _currentHP)
        {
            _currentHP = 0;
            ChangeState(eState.DEAD);
        }
    }

    //
    public void IncreaseHP(float addHP)
    {

        if (false == _isBlockSmash)
        {
            _currentHP += addHP;
         
            if (_maxHP < _currentHP)
            {
                _currentHP = _maxHP;
            }
        }
        else 
        {
            addHP = 0;
            if (_maxHP < _currentHP)
            {
                _currentHP = _maxHP;
            }
        }
        
    }

    // Weight
    float _currentWeight = 80.0f;
    float _decreaseWeight = 0.1f;

    float _maxWeight = 120.0f;
    float _minWeight = 40.0f;

    public float GetMaxWeight()
    {
        return _maxWeight;
    }

    public float GetCurrentWeight()
    {
        return _currentWeight;
    }

    public float GetMinWeight()
    {
        return _minWeight;
    }
  
    void AddWeight(float addWeight)
    {
        _currentWeight += addWeight;

        if (false == _isBlockSmash)
        {
            if (_currentWeight < _minWeight)
            {
                _currentWeight = _minWeight;
            }
            if (_maxWeight < _currentWeight)
            {
                _currentWeight = _maxWeight;
            }

            UpdateMaxSpeed();
        }
        
        // UpdateJumpLevel
    }

    // Speed
    float _maxSpeed = 15.0f;
    float _addSpeed = 0.05f;

    void UpdateMaxSpeed()
    {
        if (110.0f < _currentWeight)
        {
            _maxSpeed = 3.0f;
        }
        else if (100.0f < _currentWeight)
        {
            _maxSpeed = 6.0f;
        }
        else if (90.0f < _currentWeight)
        {
            _maxSpeed = 9.0f;
        }
        else if (80.0f < _currentWeight)
        {
            _maxSpeed = 11.0f;
        }
        else if (70.0f < _currentWeight)
        {
            _maxSpeed = 15.0f;
        }
        else if (60.0f < _currentWeight)
        {
            _maxSpeed = 18.0f;
        }
        else if (50.0f < _currentWeight)
        {
            _maxSpeed = 21.0f;
        }
        else if (40.0f < _currentWeight)
        {
            _maxSpeed = 24.0f;
        }
    
    }
    // Distance
    float _distance = 0.0f;
    float _maxDistance = 1000.0f;
           
    public bool IsComplete()
    {
        return (eState.COMPLETE == _state);
    }

    public float GetMaxDistance()
    {
        return _maxDistance;
    }

    public float GetDistance()
    {
        return _distance;
    }

    //
    float _goalWeight = 60.0f;

    public float GetGoalWeight()
    {
        return _goalWeight;
    }

    // fever
    bool _isFever = false;
    public void IsFever(bool isFever)
    {
        _isFever = isFever;
        ChangeState(eState.FEVER);

    }

    float _currentFeverTime =3.0f;
    void FeverTime()
    {
        _velocity.x = 50.0f;
        _currentFeverTime -= Time.deltaTime;
        if(0 >= _currentFeverTime)
        {
            Debug.Log("Test");
            _currentFeverTime = 3.0f;
            ChangeState(eState.RUN);
        }
    }

}


