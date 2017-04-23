using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Element : MonoBehaviour {

	// Is this a mine?
	public bool mine;

	// Different Textures
	public Sprite[] emptyTextures;
	public Sprite[] activeTextures;
	public Sprite mineTexture;
	public Sprite defaultTexture;
	public Sprite flagTexture;
	public int x,y;

	// Use this for initialization
	void Start () {
		// Randomly decide if it's a mine or not
		init();
	}
	public void init() {
		mine = Random.value < 0.15;

		x = (int)transform.position.x;
		y = (int)transform.position.y;
		Grid.elements[x, y] = this;
	}
	public void loadTexture(int adjacentCount, bool activeFlag=false) {
		if (adjacentCount == -1)
			GetComponent<SpriteRenderer>().sprite = defaultTexture;
		else if (adjacentCount == -2)
			GetComponent<SpriteRenderer>().sprite = flagTexture;
		else if (mine)
			GetComponent<SpriteRenderer>().sprite = mineTexture;
		else if (activeFlag)
			GetComponent<SpriteRenderer>().sprite = activeTextures[adjacentCount];
		else
			GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
	}
	void Update() {
		if (Input.GetKeyDown ("up") && Grid.currY < Grid.h - 1 && Grid.upPressed == false) {
//			Grid.resetGrid ();
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
			Grid.currY += 1;
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
//			Grid.FFuncover(Grid.currX+1, Grid.currY);
//			Grid.FFuncover(Grid.currX-1, Grid.currY);
//			Grid.FFuncover(Grid.currX, Grid.currY+1);
//			Grid.FFuncover(Grid.currX, Grid.currY-1);
			Grid.upPressed = true;
		}
		if (Input.GetKeyUp ("up")) {
			Grid.upPressed = false;
		}
		if (Input.GetKeyDown ("down") && Grid.currY > 0 && Grid.downPressed == false) {
//			Grid.resetGrid ();
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
			Grid.currY -= 1;
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
//			Grid.FFuncover(Grid.currX+1, Grid.currY);
//			Grid.FFuncover(Grid.currX-1, Grid.currY);
//			Grid.FFuncover(Grid.currX, Grid.currY+1);
//			Grid.FFuncover(Grid.currX, Grid.currY-1);
			Grid.downPressed = true;
		}
		if (Input.GetKeyUp ("down")) {
			Grid.downPressed = false;
		}


		if (Input.GetKeyDown ("left") && Grid.currX >0 && Grid.leftPressed == false) {
//			Grid.resetGrid ();
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
			Grid.currX -= 1;
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
//			Grid.FFuncover(Grid.currX+1, Grid.currY);
//			Grid.FFuncover(Grid.currX-1, Grid.currY);
//			Grid.FFuncover(Grid.currX, Grid.currY+1);
//			Grid.FFuncover(Grid.currX, Grid.currY-1);
			Grid.leftPressed = true;
		}
		if (Input.GetKeyUp ("left")) {
			Grid.leftPressed = false;
		}
		if (Input.GetKeyDown ("right") && Grid.currX < Grid.w - 1 && Grid.rightPressed == false) {
//			Grid.resetGrid ();
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
			Grid.currX += 1;
			Grid.FFuncover(Grid.currX, Grid.currY, new bool[Grid.w, Grid.h]);
//			Grid.FFuncover(Grid.currX+1, Grid.currY);
//			Grid.FFuncover(Grid.currX-1, Grid.currY);
//			Grid.FFuncover(Grid.currX, Grid.currY+1);
//			Grid.FFuncover(Grid.currX, Grid.currY-1);
			Grid.rightPressed = true;
		}
		if (Input.GetKeyUp ("right")) {
			Grid.rightPressed = false;
		}
		
		Grid.moveCursor ();
	}
	void OnMouseUpAsButton() {
		// It's a mine
		if (mine) {
			// Uncover all mines
			Grid.uncoverMines();

			// game over
			print("you lose");
		}
		// It's not a mine
		else {
			// ToDo show adjacent mine number
			int x = (int)transform.position.x;
			int y = (int)transform.position.y;
			loadTexture(Grid.adjacentMines(x, y));


			// ToDo uncover area without mines
			Grid.FFuncover(x, y, new bool[Grid.w, Grid.h]);


			// ToDo find out if the game was won now
			// ...
		}
	}
	void OnMouseOver () {
		if(Input.GetMouseButtonDown(1)) {
			loadTexture (-2);
		}
	}


}
