using System;
using System.Collections;
using ACTBase;
using System.Xml;

namespace ACTGame
{
	public struct Position:XmlData
	{
		public double x; //
		public double y; //
		public double z; //z表示深度（厚度）

		public static readonly Position ZERO = new Position();

		public Position(double x = 0.0 , double y = 0.0 , double z = 0.0)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public bool IsZero()
		{
			return this.x == 0.0 && this.y == 0.0 && this.z == 0.0;
		}

		public void SetZero()
		{
			this.x = 0;
			this.y = 0;
			this.z = 0;
		}

		public static Position operator +(Position lhs , Position rhs)
		{
			lhs.x = lhs.x + rhs.x;
			lhs.y = lhs.y + rhs.y;
			lhs.z = lhs.z + rhs.z;
			return lhs;
		}

		public static Position operator -(Position lhs , Position rhs)
		{
			lhs.x = lhs.x - rhs.x;
			lhs.y = lhs.y - rhs.y;
			lhs.z = lhs.z - rhs.z;
			return lhs;
		}

		public static Position operator *(Position lhs , Position rhs)
		{
			lhs.x = lhs.x * rhs.x;
			lhs.y = lhs.y * rhs.y;
			lhs.z = lhs.z * rhs.z;
			return lhs;
		}

		public static bool operator <=(Position lhs , Position rhs)
		{
			return lhs.x <= rhs.x && lhs.y <= rhs.y && lhs.z <= rhs.z;
		}

		public static bool operator >=(Position lhs , Position rhs)
		{
			return lhs.x >= rhs.x && lhs.y >= rhs.y && lhs.z >= rhs.z;
		}

		//当前this表示朝右
		public Position GetFacePos(bool bFaceLeft)
		{
			if (!bFaceLeft)
				return this;
			
			Position pos = new Position ( - this.x , this.y , this.z);
			return pos;
		}

		public Position GetScalePos(double fScale)
		{
			if (fScale == 1.0)
				return this;
			
			this.x = this.x * fScale;
			this.y = this.y * fScale;
			this.z = this.z * fScale;
			return this;
		}
		
		public void Read(XmlElement xml)
		{
			XmlRead.Attr (xml , "x" , ref x);
			XmlRead.Attr (xml , "y" , ref y);
			XmlRead.Attr (xml , "h" , ref z);
		}

		public void Write(XmlElement xml)
		{
			XmlWrite.Attr (xml , "x" , ref x);
			XmlWrite.Attr (xml , "y" , ref y);
			XmlWrite.Attr (xml , "h" , ref z);
		}
	}

	public struct BoundingBox:XmlData
	{
		public Position pMin;
		public Position pMax;

		public static readonly BoundingBox ZERO = new BoundingBox();

		public BoundingBox(Position min, Position max)
		{
			this.pMin = min;
			this.pMax = max;
		}

		public bool IsZero()
		{
			return pMin.IsZero () && pMax.IsZero ();
		}

		public void SetZero()
		{
			pMin.SetZero ();
			pMax.SetZero ();
		}

		public static BoundingBox operator + (BoundingBox box , Position pos)
		{
			box.pMin = box.pMin + pos;
			box.pMax = box.pMax + pos;

			return box;
		}

		public static BoundingBox operator - (BoundingBox box , Position pos)
		{
			box.pMin = box.pMin - pos;
			box.pMax = box.pMax - pos;

			return box;
		}
			
		public BoundingBox GetFaceBox(bool bLeft)
		{
			BoundingBox box = new BoundingBox (pMin , pMax);
			box.pMin.x = - box.pMin.x;
			box.pMax.x = - box.pMax.x;

			return box;
		}

		public BoundingBox GetScaleBox(float fScale)
		{
			if (fScale == 1.0)
				return this;
			
			BoundingBox box = new BoundingBox (pMin , pMax);
			box.pMin *= fScale;
			box.pMax *= fScale;

			return box;
		}

		public bool IsIntersect(BoundingBox box)
		{
			return box.pMax >= pMin && box.pMin <= pMax;
		}

		public bool IsIntersect(Position pos)
		{
			return pos >= pMin && pos <= pMax;
		}

		public void Read(XmlElement xml)
		{
			XmlRead.Node (xml , "min" , pMin);
			XmlRead.Node (xml , "max" , pMax);
		}

		public void Write(XmlElement xml)
		{
			XmlWrite.Node (xml , "min" , pMin);
			XmlWrite.Node (xml , "max" , pMax);
		}

	}
}
