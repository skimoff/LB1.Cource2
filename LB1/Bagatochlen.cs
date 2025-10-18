namespace LB1;

public class Polynomial
{
    public int Degree { get; set; }
    public double[] Coefficients { get; set; }

    public Polynomial()
    {
        Degree = 0;
        Coefficients = new double[0];
    }

    public Polynomial(int degree, double[] coefficients)
    {
        Degree = degree;
        Coefficients = coefficients;
    }
    public override string ToString()
    {
        string result = "";
        for (int i = Degree; i >= 0; i--)
        {
            double coef = Coefficients[i];
            if (coef == 0) continue;

            if (result.Length > 0 && coef > 0)
                result += " + ";

            if (i == 0)
                result += coef;
            else if (i == 1)
                result += coef + "x";
            else
                result += coef + "x^" + i;
        }
        return result == "" ? "0" : result;
    }
}