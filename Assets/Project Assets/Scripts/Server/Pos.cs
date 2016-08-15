//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Pos  {
//
//    public float X;
//    public float Y;
//	public float Angle;
//
//	public static Pos MakeNewPos(int x, int y, float a = 0.0f){
//		var pos = new Pos ();
//		pos.X = x;
//		pos.Y = y;
//		pos.Angle = a;
//
//		return pos;
//    }
//
//
//	public Pos Clone(){
//		
//		var pos = new Pos ();
//
//		pos.X = this.X;
//
//		pos.Y = this.Y;
//
//		pos.Angle = this.Angle;
//
//
//		return pos;
//	}
//
//	public bool CheckPosBegin(Scene s){
//		return true;
//	}
//
//	public bool CheckPosEnd(int r, Scene s){
//		return true;
//	}
//
//	public int Dist(Pos other){
//		var a = this.X - other.X;
//		var b = this.Y - other.Y;
//		return (int)(a * a + b * b);
//	} 
//
//}
//
