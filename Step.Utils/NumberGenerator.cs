using System;
using System.Collections.Generic;
using System.Text;

namespace Step.Utils.RandomNumber;

public class NumberGenerator
{
    public static string GenerateRandomNumbers(int length, string type)
    {
        string index = type;
        Random random = new Random();
        StringBuilder output = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            output.Append(random.Next(0, 10));
        }
        return $"{index}{output.ToString()}";
    }
}