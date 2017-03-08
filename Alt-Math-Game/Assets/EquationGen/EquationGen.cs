using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mehroz;

/// <summary>
/// This class generates an equation based on inputs.
/// </summary>
public class EquationGen
{


    public string equation; //Stores an equation
    public string solution; //and a solution to that equation.

    /// <summary>
    /// Blank constructor: Generates a totally random equation.
    /// </summary>
    public EquationGen() //Note: This class generates prototype equations. This means that it provides very basic equations. To be modified.
    { //This constructor is a 'free' constructor, allows us to generate any type of equation we want.
        SeededGeneration(0);
    }

    /// <summary>
    /// Creates an equation from the specified generator.
    /// </summary>
    /// <param name="genSeed">The generator number with which to generate an equation.</param>
    public EquationGen(int genSeed) //This generator generates based on an initial value given. This will likely be used for a single-topic mode.
    {
        SeededGeneration(genSeed);
    }

    /// <summary>
    /// Creates an equation from any of the generators within the range.
    /// </summary>
    /// <param name="startSeed">Beginning of generation</param>
    /// <param name="endSeed">End of generation</param>
    public EquationGen(int startSeed, int endSeed) //This generates an equation based on the range of ints. Will be used in a story-type mode.
    {
        int genSeed = UnityEngine.Random.Range(startSeed, endSeed);
        SeededGeneration(genSeed);
    }

    private void SeededGeneration(int genSeed) //Helper method that creates the equation.
    {
        //Number types: Whole numbers, Negative Numbers, Fractions, Mixed Numbers, Decimals
        //Operations: Addition, Subtraction, Multiplication, Division
        //Hmm... Maybe we should arrange levels by topic.
        if(genSeed == 0)
        {
            genSeed = UnityEngine.Random.Range(1, 9);
        }
        if (genSeed == 1)
        {
            BasicAddSub();
        }
        else if (genSeed == 2)
        {
            HundredsAddSub();
        }
        else if (genSeed == 3)
        {
            ThousandsAddSub();
        }
        else if (genSeed == 4)
        {
            BasicMultiDiv();
        }
        else if (genSeed == 5)
        {
            HundredsMultiDiv();
        }
        
        else if(genSeed == 6)
        {
            FractionsAddSub();
        }
        else if(genSeed == 7)
        {
            FractionsMultiDiv();
        }
        //else if(genSeed == 8)
        //{
        //    DecimalAddSub();
        //}
        //else if(genSeed == 9)
        //{
        //    DecimalMultiDiv();
        //}
        else
        {
            throw new NotImplementedException();
        }
        //int oper = Random.Range(1, 5); //And generates an operation.
        //if (oper == 1)
        //{ //Addition
        //    int a = Random.Range(0, 10);
        //    int b = Random.Range(0, 10);

        //    equation = a.ToString() + " + " + b.ToString();
        //    solution = (a + b).ToString();
        //}

        //else if (oper == 2)
        //{ //Subtraction
        //    int a = Random.Range(0, 10);
        //    int b = Random.Range(0, a);
        //    equation = a.ToString() + " - " + b.ToString();
        //    solution = (a - b).ToString();
        //}

        //else if (oper == 3)
        //{ //Multiplication
        //    int a = Random.Range(0, 5);
        //    int b = Random.Range(0, 5);
        //    equation = a.ToString() + " * " + b.ToString();
        //    solution = (a * b).ToString();
        //}

        //else if (oper == 4)
        //{ //Division
        //    string[] hardCodedDivList = { "1 / 1", "2 / 1", "3 / 1", "4 / 1", "5 / 1", "6 / 1", "7 / 1", "8 / 1", "9 / 1", "10 / 1", "2 / 2", "4 / 2", "6 / 2", "8 / 2", "10 / 2", "3 / 3", "6 / 3", "9 / 3", "4 / 4", "8 / 4", "5 / 5", "10 / 5" }; //To be removed
        //    string[] hardCodedSolList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "1", "2", "3", "4", "5", "1", "2", "3", "1", "2", "1", "2" };
        //    int equaSolPair = Random.Range(0, hardCodedDivList.Length);
        //    equation = hardCodedDivList[equaSolPair];
        //    solution = hardCodedSolList[equaSolPair];
        //}

    }

    

    private void FractionsMultiDiv()
    {
        Fraction sol;
        int a = UnityEngine.Random.Range(0, 10);
        int b = UnityEngine.Random.Range(0, 10);
        int c = UnityEngine.Random.Range(0, 10);
        int d = UnityEngine.Random.Range(0, 10);

        Fraction alpha = new Fraction(a, b);
        Fraction beta = new Fraction(c, d);

        int oper = UnityEngine.Random.Range(0, 1);

        if (oper == 0)
        {
            sol = alpha * beta;
            equation = alpha.ToString() + " * " + beta.ToString();
            solution = sol.ToString();
        }
        else
        {
            sol = alpha / beta;
            equation = alpha.ToString() + " / " + beta.ToString();
            solution = sol.ToString();
        }
    }

    private void FractionsAddSub()
    {
        Fraction sol;
        int a = UnityEngine.Random.Range(0, 10);
        int b = UnityEngine.Random.Range(0, 10);
        int c = UnityEngine.Random.Range(0, 10);
        int d = UnityEngine.Random.Range(0, 10);

        Fraction alpha = new Fraction(a, b);
        Fraction beta = new Fraction(c, d);

        int oper = UnityEngine.Random.Range(0, 1);

        if (oper == 0)
        {
            sol = alpha + beta;
            equation = alpha.ToString() + " + " + beta.ToString();
            solution = sol.ToString();
        }
        else
        {
            if (alpha > beta)
            {
                equation = alpha.ToString() + " - " + beta.ToString();
                solution = (alpha - beta).ToString();
            }
            else
            {
                equation = beta.ToString() + " - " + alpha.ToString();
                solution = (beta - alpha).ToString();
            }
        }
    }

    private void HundredsMultiDiv()
    {
        int sol;
        int a;
        int b;
        int oper = UnityEngine.Random.Range(0, 1);

        if (oper == 0)
        {
            a = UnityEngine.Random.Range(0, 100);
            b = UnityEngine.Random.Range(0, 100);

            equation = a.ToString() + " * " + b.ToString();
            solution = (a * b).ToString();
        }
        else
        {
            sol = UnityEngine.Random.Range(0, 1000);
            do
            {
                a = UnityEngine.Random.Range(0, sol);
            } while (sol % a != 0);

            b = sol / a;

            equation = a.ToString() + " / " + b.ToString();
            solution = sol.ToString();
        }
    }

    
    private void BasicMultiDiv()
    {
        int sol;
        int a;
        int b;
        int oper = UnityEngine.Random.Range(0, 1);

        if (oper == 0)
        {
            a = UnityEngine.Random.Range(0, 10);
            b = UnityEngine.Random.Range(0, 10);

            equation = a.ToString() + " * " + b.ToString();
            solution = (a * b).ToString();
        }
        else
        {
            sol = UnityEngine.Random.Range(0, 100);
            do
            {
                a = UnityEngine.Random.Range(0, sol);
            } while (sol % a != 0);

            b = sol / a;

            equation = a.ToString() + " / " + b.ToString();
            solution = sol.ToString();
        }
    }

    private void BasicAddSub()
    {
        int sol = UnityEngine.Random.Range(0, 99);
        solution = sol.ToString();

        int a = sol - UnityEngine.Random.Range(0, sol);
        int b = sol - a;

        int oper = UnityEngine.Random.Range(0, 1);

        if(oper == 0)
        {
            equation = a.ToString() + " + " + b.ToString();
        }
        else
        {
            if(a > b)
            {
                equation = a.ToString() + " - " + b.ToString();
            }
            else
            {
                equation = b.ToString() + " - " + a.ToString();
            }
        }
    }

    private void HundredsAddSub()
    {
        int sol = UnityEngine.Random.Range(0, 999);
        solution = sol.ToString();

        int a = sol - UnityEngine.Random.Range(0, sol);
        int b = sol - a;

        int oper = UnityEngine.Random.Range(0, 1);

        if (oper == 0)
        {
            equation = a.ToString() + " + " + b.ToString();
            solution = (a + b).ToString();
        }
        else
        {
            if (a > b)
            {
                equation = a.ToString() + " - " + b.ToString();
                solution = (a - b).ToString();
            }
            else
            {
                equation = b.ToString() + " - " + a.ToString();
                solution = (b - a).ToString();
            }
        }
    }

    private void ThousandsAddSub()
    {
        int sol = UnityEngine.Random.Range(0, 9999);
        solution = sol.ToString();

        int a = sol - UnityEngine.Random.Range(0, sol);
        int b = sol - a;

        int oper = UnityEngine.Random.Range(0, 1);

        if (oper == 0)
        {
            equation = a.ToString() + " + " + b.ToString();
            solution = (a + b).ToString();
        }
        else
        {
            if (a > b)
            {
                equation = a.ToString() + " - " + b.ToString();
                solution = (a - b).ToString();
            }
            else
            {
                equation = b.ToString() + " - " + a.ToString();
                solution = (b - a).ToString();
            }
        }
    }
}
