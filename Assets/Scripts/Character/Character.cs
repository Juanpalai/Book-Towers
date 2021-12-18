using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
	//派閥
	public enum CAMPS
	{
		PLAYER,
		ENEMY,
	};
	public CAMPS _camp;

	public Vector3 _move_target_dir;
	private float _move_speed = 1.0f;
	private int _end_dir;
	private int END_DIR = 16; // 移動上限
							  //次の点までの距離
	private float _move_distance = -1.0f;
	//向き
	private float _orientation = 90.0f;
	//HP
	public float _life;
	private float _max_life;
	private int _damage;
	//判定
	private bool _is_moving;
	[SerializeField]
	protected bool _is_effective_unit;
	//HPState
	private Transform _blood_obj;
	private Slider _blood_slider;
	public List<Character> _enemy_in_scope = new List<Character>();

	[SerializeField]
	private Animator _anim;

	//攻撃
	public int _attack_power;
	private bool _is_attacking;
	private bool _finish_attack;
	//攻撃の間
	private const int ATTACK_SPEED = 3;


	private void Awake()
	{
		_is_effective_unit = true;
	}
	// Start is called before the first frame update
	public void Start()
	{
		_max_life = _life;
		_damage = 0;
		_end_dir = 0;
		_is_moving = false;
		_is_attacking = false;
		_finish_attack = true;
		_anim = GetComponent<Animator>();
		rotationInit();
		loadBlood();
		if (_camp == CAMPS.ENEMY)
		{
			setUnitEffective();
		}
		setMoveTarget();
	}

	// Update is called once per frame
	public void Update()
	{
		if (!_is_effective_unit)
		{
			return;
		}
		//moveTargetDirUpdate( );
		move();
		animUdp();
		autoAttack();
		damageProcess();
		dead();
	}

	public void move()
	{
		if (this.transform.position == _move_target_dir ||
			!isEnemyListEmpty() ||
			isEndDir() ||
			_is_attacking ||
			!_finish_attack)
		{
			_is_moving = false;
			return;
		}
		_is_moving = true;
		this.transform.position = Vector3.MoveTowards(this.transform.position, _move_target_dir, _move_speed * Time.deltaTime);
	}

	public void moveTargetDirUpdate()
	{
		if (isMoving() ||
			isEndDir() ||
			!isEnemyListEmpty())
		{
			return;
		}
		Vector3 target_dir = this.transform.position;
		float dis = _move_distance;
		if (_camp == CAMPS.ENEMY)
		{
			dis *= -1;
		}
		target_dir.x += dis;
		_end_dir++;
		_move_target_dir = target_dir;
	}

	public bool isMoving()
	{
		return _is_moving;
	}

	public CAMPS getCamp()
	{
		return _camp;
	}

	public void setDamage(int damage)
	{
		_damage = damage;
	}

	public void setAttackPower(int power)
	{
		_attack_power = power;
	}

	private void damageProcess()
	{
		if (_damage == 0)
		{
			return;
		}
		_life -= _damage;
		_damage = 0;
	}


	public void setCamp(CAMPS camp)
	{
		_camp = camp;
	}

	public void setLife(float life)
	{
		_life = life;
	}

	private void rotationInit()
	{
		if (_camp == CAMPS.PLAYER)
		{
			_orientation *= -1;
		}
		this.transform.rotation = Quaternion.Euler(0.0f, _orientation, 0.0f);
	}

	private void autoAttack()
	{
		if (isEnemyListEmpty() &&
			!isEndDir() ||
			!_finish_attack)
		{
			_is_attacking = false;
			return;
		}
		_is_attacking = true;
		if (_finish_attack) { _finish_attack = false; }
		StartCoroutine(waitTimeToAttack());
	}

	private void attack()
	{
		foreach (Character character in _enemy_in_scope)
		{
			character.setDamage(_attack_power);
		}

		for (int i = 0; i < _enemy_in_scope.Count; i++)
		{
			if (_enemy_in_scope[i].isdead())
			{
				_enemy_in_scope.Remove(_enemy_in_scope[i]);
			}
		}
	}

	public bool isEnemyListEmpty()
	{
		return _enemy_in_scope.Count == 0;
	}

	private void loadBlood()
	{
		GameObject prefab = (GameObject)Resources.Load("Canvas_Of_Blood");
		_blood_obj = Instantiate(prefab, Vector3.zero, Camera.main.transform.rotation, this.transform).transform;
		if (_camp == CAMPS.ENEMY)
		{
			_blood_obj.transform.GetChild(0).gameObject.SetActive(true);
		}
		else {
			_blood_obj.transform.GetChild(1).gameObject.SetActive(true);
		}
		//キャラクターの頭の上に描画する
		_blood_obj.localPosition = new Vector3(0, 2.0f, 0.13f);
		//大きさ調整
		_blood_obj.localScale = new Vector3(0.05f, 0.05f, 0.05f);
		_blood_slider = _blood_obj.GetComponentInChildren<Slider>();
		StartCoroutine(bloodUpdate());
	}

	private IEnumerator bloodUpdate()
	{
		_blood_slider.value = _life / _max_life;
		yield return 0;
		StartCoroutine(bloodUpdate());
	}

	private IEnumerator waitTimeToAttack()
	{
		yield return new WaitForSeconds(ATTACK_SPEED);
		_anim.SetBool( "Attack", true );
		yield return new WaitForSeconds(_anim.GetCurrentAnimatorStateInfo(0).length * 0.5f);
		_anim.SetBool("Attack", false);
		if (!isEnemyListEmpty()) { attack(); } //Unitにダメージ
		if ( isEndDir() && isEnemyListEmpty( ) ) { damageToHome(); }  //Homeにダメージ
		_finish_attack = true;
	
		yield return 0;

	}

	private void animUdp()
	{
		string str = "Walk";

		if (_is_moving &&
			!_is_attacking &&
			_finish_attack)
		{
			_anim.SetBool(str, _is_moving);
		}
		else
		{
			_anim.SetBool(str, false);
		}
	}
	public void setUnitEffective()
	{
		_is_effective_unit = true;
	}

	public void setUnitDimmed( ) {
		_is_effective_unit = false;
	}

	public bool isdead()
	{
		return _life <= 0;
	}

	private void dead()
	{
		if (_life > 0)
		{
			return;
		}

		Destroy(this.gameObject);
	}

	private bool isEndDir()
	{
		return this.transform.position == _move_target_dir;
	}

	private void damageToHome()
	{
		if (this._camp == CAMPS.PLAYER)
		{
			GameManager.setDamageToEnemy(_attack_power);
		}
		else
		{
			GameManager.setDamageToPlayer(_attack_power);
		}
	}

	private void setMoveTarget()
	{
		float tar_x = 20.0f;
		if (this._camp == CAMPS.PLAYER)
		{
			tar_x *= -1;
		}
		_move_target_dir = new Vector3(this.transform.position.x + tar_x,
										this.transform.position.y,
										this.transform.position.z);
	}
}
