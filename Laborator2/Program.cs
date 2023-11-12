using System;

class Program
{
    static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        using (SimpleWindow laborator2 = new SimpleWindow())
        {
            laborator2.Run(30.0, 0.0);
        }
    }
}