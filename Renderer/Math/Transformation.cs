using System;

namespace Renderer.Math
{
    public class Transformation
    {
        public Matrix Matrix { get; private set; }
        public Vector Vector { get; private set; }

        public Transformation()
        {
            this.Matrix = Matrix.Identity();
            this.Vector = new Vector();
        }

        public Transformation(Transformation copy)
        {
            this.Matrix = new Matrix(copy.Matrix);
            this.Vector = new Vector(copy.Vector);
        }

        public Transformation Transform(Transformation trans)
        {
            Transformation tempTrans = new Transformation();
            tempTrans.Matrix = this.Matrix * trans.Matrix;
            tempTrans.Vector = this.Matrix * trans.Vector + this.Vector;
            return tempTrans;
        }

        public Transformation Translate(float x, float y, float z)
        {
            Vector translate = new Vector(x, y, z);
            Transformation tempTrans = new Transformation(this);
            tempTrans.Vector += tempTrans.Matrix * translate;
            return tempTrans;
        }

        public Transformation Scale(float x, float y, float z)
        {
            Matrix scaleMat = Matrix.Identity();
            scaleMat[0, 0] = x;
            scaleMat[1, 1] = y;
            scaleMat[2, 2] = z;
            Transformation tempTrans = new Transformation(this);
            tempTrans.Matrix *= scaleMat;
            return tempTrans;
            
        }

        public Transformation Rotate(float angle, float x, float y, float z)
        {
            throw new NotImplementedException("Transform.Rotate");
        }

        public Transformation Inverse()
        {
            Transformation tempTrans = new Transformation(this);
            tempTrans.Matrix = this.Matrix.Inverse();
            tempTrans.Vector = tempTrans.Matrix * -this.Vector;
            return tempTrans;
        }
    }
}
