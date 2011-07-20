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
            float xy = x * y;
            float xz = x * z;
            float yz = y * z;
            float rads = Math.Utils.Deg2Rad(angle);
            float cos = (float)System.Math.Cos(rads);
            float sin = (float)System.Math.Sin(rads);
            float omc = 1 - cos;
            float oms = 1 - sin;

            Matrix rotMat = new Matrix();
            rotMat[0, 0] = cos + x * x * omc;
            rotMat[0, 1] = xy * omc - z * sin;
            rotMat[0, 2] = xz * omc + y * sin;
            rotMat[1, 0] = xy * omc + z * sin;
            rotMat[1, 1] = cos + y * y * omc;
            rotMat[1, 2] = yz * omc - x * sin;
            rotMat[2, 0] = xz * omc - y * sin;
            rotMat[2, 1] = yz * omc + x * sin;
            rotMat[2, 2] = cos + z * z * omc;

            Transformation tempTrans = new Transformation(this);
            tempTrans.Matrix *= rotMat;
            return tempTrans;
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
