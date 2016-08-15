using UnityEngine;
using System.Collections;

//public class FishHit{
//
//	public int X;
//
//	public int Y;
//
//	public int R;
//		
//	public int RR;
//
//	public static FishHit MakeNewFishHit(int x, int y, int r) {
//	
//		var f = new FishHit ();
//
//		f.X = x;
//
//		f.Y = y;
//
//		f.R = r;
//
//		f.RR = r * r;
//
//		return f;
//	}
//	public static  FishHit MakeNewFishnetHit(Bullet b){
//		
//		var hit = new FishHit ();
//
//		var nowpos = b.NowPos ();
//
//		hit.X = (int)nowpos.x;
//
//		hit.Y = (int)nowpos.y;
//
//		hit.R = b.R;
//
//		hit.RR = b.R * b.R;
//
//		return hit;
//	}
//	public bool isHit(object obj){
//
//		if (obj.GetType () == typeof(Vector3)) {
//
//			Vector3 pos = (Vector3)obj;
//
//			if (pos.Dist (Pos.MakeNewPos (this.X, this.Y)) <= this.RR) {
//
//				return true;
//			
//			}else {
//
//				return false;
//			}
//		}
//
//		if (obj.GetType () == typeof(FishHit)) {
//
//			FishHit hit = (FishHit)obj;
//
//			if (this.Dist(hit) <= (this.R + hit.R)*(this.R+hit.R)){
//				return true;
//			}
//
//			return false;
//		}
//
//		return true;
//	}
//
//	public FishHit NewOXY(float offx, float offy, float cost, float sint){
//	
//		var x = (int)(((float)this.X) * cost - ((float)this.Y) * sint + offx);
//
//		var y = (int)(((float)this.X) * sint - ((float)this.Y) * cost + offy);
//
//		var hit = FishHit.MakeNewFishHit (x, y, this.R);
//
//		return hit;
//	}
//
//
//
//	public int Dist(FishHit other){
//		
//		var a = this.X - other.X;
//
//		var b = this.Y - other.Y;
//
//		return a * a + b * b;
//	}
//
//}
