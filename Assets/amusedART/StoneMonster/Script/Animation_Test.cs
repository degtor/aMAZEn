using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Test : MonoBehaviour {

	public const string IDLE	= "Anim_Idle";
	public const string RUN		= "Anim_Run";
	public const string ATTACK	= "Anim_Attack";
	public const string DAMAGE	= "Anim_Damage";
	public const string DEATH	= "Anim_Death";

	public float Speed=0.1f;
	public float ToMove;
	public Transform target1;
	public Transform target2;
	private float direction=1.0f;
	private float currentPosition=0.0f;

	Animation anim;

	void Start () {

		anim = GetComponent<Animation>();

		IdleAni ();
	}

	void Update ()
	{
		currentPosition = Mathf.Clamp01(currentPosition + Speed * Time.deltaTime * direction);
		if(direction == 1.0f & currentPosition > 0.99f) direction=-1.0f;
		if(direction ==-1.0f & currentPosition < 0.01f) direction= 1.0f;

		transform.position = Vector3.Lerp(target1.position, target2.position, currentPosition);

		IdleAni ();
	}
	
	public void IdleAni (){
		anim.CrossFade (IDLE);
	}

	public void RunAni (){
		anim.CrossFade (RUN);
	}

	public void AttackAni (){
		anim.CrossFade (ATTACK);
	}

	public void DamageAni (){
		anim.CrossFade (DAMAGE);
	}

	public void DeathAni (){
		anim.CrossFade (DEATH);
	}

}
