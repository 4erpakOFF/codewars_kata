using System;
using System.Numerics;

public class Fibonacci
{
    private class FibMatrix
    {
        public BigInteger a00, a01, a10, a11;
        public static readonly FibMatrix defaultMatrix = new FibMatrix (0, 1, 1, 1);

        public FibMatrix(BigInteger a00, BigInteger a01, BigInteger a10, BigInteger a11)
        {
            this.a00 = a00;
            this.a01 = a01;
            this.a10 = a10;
            this.a11 = a11;
        }

        public static FibMatrix operator *(FibMatrix m1, FibMatrix m2)
        {
            return new FibMatrix(m1.a00 * m2.a00 + m1.a01 * m2.a10,
                                 m1.a00 * m2.a01 + m1.a01 * m2.a11,
                                 m1.a10 * m2.a00 + m1.a11 * m2.a10,
                                 m1.a10 * m2.a01 + m1.a11 * m2.a11);
        }
        public static FibMatrix operator *(BigInteger val, FibMatrix m)
        {
            return new FibMatrix(m.a00 * val, m.a01 * val, m.a10 * val, m.a11 * val);
        }
    }

    private static FibMatrix BinPow(FibMatrix val, int pow)
    {
        FibMatrix res = new FibMatrix(1, 0, 0, 1);
        if (pow == 0) return res;
        bool isNegative = pow > 0 ? false : true;
        var exp = Math.Abs(pow);
        while (exp > 0)
        {
            if (exp % 2 == 1)
                res *= val;
            val *= val;
            exp >>= 1;
        }

        if (isNegative)
            return 1 / (res.a00 * res.a11 - res.a01 * res.a10) 
                   * (new FibMatrix(res.a11, (-1) * res.a01, (-1) * res.a10, res.a00));
        else
            return res;
    }

    public static BigInteger fib(int n)
    {
        var resMatrix = BinPow(FibMatrix.defaultMatrix, n);
        return resMatrix.a10;
    }
}