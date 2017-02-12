using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquationGen
{


    public string equation; //Stores an equation
    public string solution; //and a solution to that equation.

    public EquationGen() //Note: This class generates prototype equations. This means that it provides very basic equations. To be modified.
    {
        int oper = Random.Range(1, 5); //And generates an operation.
        if (oper == 1)
        { //Addition
            int a = Random.Range(0, 10);
            int b = Random.Range(0, 10);

            equation = a.ToString() + " + " + b.ToString();
            solution = (a + b).ToString();
        }

        else if (oper == 2)
        { //Subtraction
            int a = Random.Range(0, 10);
            int b = Random.Range(0, a);
            equation = a.ToString() + " - " + b.ToString();
            solution = (a - b).ToString();
        }

        else if (oper == 3)
        { //Multiplication
            int a = Random.Range(0, 5);
            int b = Random.Range(0, 5);
            equation = a.ToString() + " * " + b.ToString();
            solution = (a * b).ToString();
        }

        else if (oper == 4)
        { //Division
            string[] hardCodedDivList = { "1 / 1", "2 / 1", "3 / 1", "4 / 1", "5 / 1", "6 / 1", "7 / 1", "8 / 1", "9 / 1", "10 / 1", "2 / 2", "4 / 2", "6 / 2", "8 / 2", "10 / 2", "3 / 3", "6 / 3", "9 / 3", "4 / 4", "8 / 4", "5 / 5", "10 / 5" }; //To be removed
            string[] hardCodedSolList = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "1", "2", "3", "4", "5", "1", "2", "3", "1", "2", "1", "2" };
            int equaSolPair = Random.Range(0, hardCodedDivList.Length);
            equation = hardCodedDivList[equaSolPair];
            solution = hardCodedSolList[equaSolPair];
        }
    }

}
