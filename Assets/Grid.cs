using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid  {
	public static int w = 8;
	public static int h = 8;
	public static int currX = 0;
	public static int currY = 0;
	public static Element[,] elements = new Element[w,h];

	public static bool upPressed;
	public static bool downPressed;
	public static bool leftPressed;
	public static bool rightPressed;

	public static void uncoverMines() {
		foreach (Element elem in elements)
			if (elem.mine)
				elem.loadTexture(0);
	}
	public static bool mineAt(int x, int y) {
		// Coordinates in range? Then check for mine.
		if (x >= 0 && y >= 0 && x < w && y < h)
			return elements[x, y].mine;
		return false;
	}
	// Count adjacent mines for an element
	public static int adjacentMines(int x, int y) {
		int count = 0;

		if (mineAt(x,   y+1)) ++count; // top
		if (mineAt(x+1, y+1)) ++count; // top-right
		if (mineAt(x+1, y  )) ++count; // right
		if (mineAt(x+1, y-1)) ++count; // bottom-right
		if (mineAt(x,   y-1)) ++count; // bottom
		if (mineAt(x-1, y-1)) ++count; // bottom-left
		if (mineAt(x-1, y  )) ++count; // left
		if (mineAt(x-1, y+1)) ++count; // top-left

		return count;
	}
//	public static void FFuncover(int x, int y) {
//		// Coordinates in Range?
//		if (x >= 0 && y >= 0 && x < w && y < h) {
//			// visited already?
//			if (mineAt(x, y))
//				return;
//
//			// uncover element
//			elements[x, y].loadTexture(adjacentMines(x, y));
//
//			// close to a mine? then no more work needed here
////			if (adjacentMines(x, y) > 0)
////				return;
//
//			// set visited flag
////			visited[x, y] = true;
//
//			// recursion
////			FFuncover(x-1, y, visited);
////			FFuncover(x+1, y, visited);
////			FFuncover(x, y-1, visited);
////			FFuncover(x, y+1, visited);
//		}
//	}

	public static void FFuncover(int x, int y, bool[,] visited) {
		// Coordinates in Range?
		if (x >= 0 && y >= 0 && x < w && y < h) {
			// visited already?

			if (visited[x, y])
				return;

			// uncover element
			elements[x, y].loadTexture(adjacentMines(x, y));

			// close to a mine? then no more work needed here
			if (adjacentMines(x, y) > 0)
				return;

			// set visited flag
			visited[x, y] = true;

			// recursion
			FFuncover(x-1, y, visited);
			FFuncover(x+1, y, visited);
			FFuncover(x, y-1, visited);
			FFuncover(x, y+1, visited);
		}
	}
	public static void moveCursor() {
		if (mineAt (currX, currY)) {
			uncoverMines ();
			return;
		}
		foreach (Element elem in elements) {
			if (elem.x == currX && elem.y == currY) {
				elem.loadTexture(adjacentMines(currX, currY),true);	
			} 

		}	
	}
	public static void resetGrid() {
		foreach (Element elem in elements) {
			if (elem.x == currX && elem.y == currY) {
				elem.loadTexture(adjacentMines(currX, currY),true);	
			} 
		}	
	}
	public static void newGame() {
		currX = 0;
		currY = 0;
//		FFuncover(currX+1, currY);
//		FFuncover(currX-1, currY);
//		FFuncover(currX, currY+1);
//		FFuncover(currX, currY-1);

		foreach (Element elem in elements) {
			if (elem.x == currX && elem.y == currY) {
				elem.loadTexture(1,true);	
			} else 
				elem.loadTexture(-1);
			elem.init ();
		}
		FFuncover(currX, currY, new bool[Grid.w, Grid.h]);
	}
}
