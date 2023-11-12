using System;

class Program
{
    static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        using (SimpleWindow laborator3 = new SimpleWindow())
        {
            laborator3.Run(30.0, 0.0);
        }
    }
}