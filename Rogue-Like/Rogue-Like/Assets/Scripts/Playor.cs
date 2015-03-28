using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Playor : MovingObjects {
	
	public int wallDamage = 1;
	public int pointsPerFood = 10;
	public int pointsPerSoda = 20;
	public float restartLevelDelay = 1f;
	public Text foodText;
	public AudioClip moveSound1;
	public AudioClip moveSound2;
	public AudioClip eatSound1;
	public AudioClip eatSound2;
	public AudioClip drinkSound1;
	public AudioClip drinkound2;
	public AudioClip gameOverSound;

	private Animator animator;
	private int food;
	
	protected override void  Start () {
		animator = GetComponent<Animator> ();	
		food = GameManager.instance.playerFoodPoints;
		foodText.text = "Food: " + food;
		base.Start ();
		
	}
	
	private void onDisable(){
		GameManager.instance.playerFoodPoints = food;
	}
	// Update is called once per frame
	void Update () {
		if(!GameManager.instance.playerTurn) return;
		
		int horizontal = 0;  	//Used to store the horizontal move direction.
		int vertical = 0;		//Used to store the vertical move direction.
		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		vertical = (int) (Input.GetAxisRaw ("Vertical"));
		if(horizontal != 0)
		{
			vertical = 0;
		}
		if(horizontal != 0 || vertical != 0)
		{
			AttemptMove<Wall> (horizontal, vertical);
		}
	}
	
	protected override void AttemptMove <T> (int xDir, int yDir){
		food--;
		foodText.text = "Food: " + food;
		base.AttemptMove <T> (xDir, yDir);
		RaycastHit2D hit;
		if (Move (xDir, yDir, out hit)) {
			SoundManager.instance.RandomizeSfx(moveSound1,moveSound2);
		}
		CheckIfGameOver ();
		GameManager.instance.playerTurn = false;
	}
	
	private void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Exit") {
			Invoke ("Restart", restartLevelDelay);
			enabled = false;
		} else if (other.tag == "Food") {
			food += pointsPerFood;
			foodText.text = "+" + pointsPerFood + " Food: "+food;
			SoundManager.instance.RandomizeSfx(eatSound1,eatSound2);
			other.gameObject.SetActive(false);
		} else if (other.tag == "Soda") {
			food  += pointsPerSoda;
			foodText.text = "+" + pointsPerSoda + " Food: "+food;
			SoundManager.instance.RandomizeSfx(drinkSound1, drinkound2);
			other.gameObject.SetActive(false);
		}
	}

	protected override void OnCantMove <T> (T component)
	{
	    Wall hitWall = component as Wall;
		hitWall.DamageWall (wallDamage);
		animator.SetTrigger ("playerChop");
	}
	
	private void Restart(){
		Application.LoadLevel (Application.loadedLevel);
	}
	
	public void LoseFood(int loss){
		animator.SetTrigger ("playerHit");
		food -= loss;
		foodText.text = "-" + loss + " Food: "+food;
		CheckIfGameOver ();
	}
	
	private void CheckIfGameOver(){
		if (food <= 0) {
			SoundManager.instance.RandomizeSfx(gameOverSound);
			GameManager.instance.GameOver();
		}
	}
}
