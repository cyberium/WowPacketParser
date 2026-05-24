using System;
using System.Globalization;

namespace WowPacketParser.Misc
{
    public struct Vector3
    {
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X;

        public float Y;

        public float Z;

        public static bool operator ==(Vector3 first, Vector3 other)
        {
            return first.Equals(other);
        }

        public static bool operator !=(Vector3 first, Vector3 other)
        {
            return !(first == other);
        }

        public static Vector3 operator +(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X + c2.X, c1.Y + c2.Y, c1.Z + c2.Z);
        }

        public static Vector3 operator -(Vector3 c1, Vector3 c2)
        {
            return new Vector3(c1.X - c2.X, c1.Y - c2.Y, c1.Z - c2.Z);
        }

        public static Vector3 operator *(Vector3 c1, float c2)
        {
            return new Vector3(c1.X * c2, c1.Y * c2, c1.Z * c2);
        }

        public static Vector3 operator /(Vector3 c1, float c2)
        {
            return new Vector3(c1.X / c2, c1.Y / c2, c1.Z / c2);
        }

        public static double GetDistance(Vector3 start, Vector3 end)
        {
            return Math.Sqrt(Math.Pow((start.X - end.X), 2) + Math.Pow((start.Y - end.Y), 2) + Math.Pow((start.Z - end.Z), 2));
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector3)
                return Equals((Vector3)obj);

            return false;
        }

        public override string ToString()
        {
            return string.Format("X: {0} Y: {1} Z: {2}", FormatPrecise(X), FormatPrecise(Y), FormatPrecise(Z));
        }

        // Prints a float with up to 20 characters, without rounding. Non finite values
        // (NaN, infinity) have no such digits and are printed as is.
        public static string FormatPrecise(float value)
        {
            if (!float.IsFinite(value))
                return value.ToString(CultureInfo.InvariantCulture);

            return string.Format(CultureInfo.InvariantCulture, "{0:F20}", value).Substring(0, 20).TrimEnd('0').TrimEnd('.');
        }

        public static string FormatPrecise(double value)
        {
            if (!double.IsFinite(value))
                return value.ToString(CultureInfo.InvariantCulture);

            return string.Format(CultureInfo.InvariantCulture, "{0:F20}", value).Substring(0, 20).TrimEnd('0').TrimEnd('.');
        }

        public bool Equals(Vector3 other)
        {
            return Math.Abs(other.X - X) < float.Epsilon &&
                Math.Abs(other.Y - Y) < float.Epsilon &&
                Math.Abs(other.Z - Z) < float.Epsilon;
        }

        public override int GetHashCode()
        {
            var result = X.GetHashCode();
            unchecked
            {
                result = (result*397) ^ Y.GetHashCode();
                result = (result*397) ^ Z.GetHashCode();
            }
            return result;
        }

        public static implicit operator WowPacketParser.Proto.Vec3(Vector3 vector) =>
            new() {X = vector.X, Y = vector.Y, Z = vector.Z};

        public static implicit operator Vector3(WowPacketParser.Proto.Vec3 vector) =>
            new() {X = vector.X, Y = vector.Y, Z = vector.Z};
    }
}
