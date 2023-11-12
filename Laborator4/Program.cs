using System;

class Program
{
    static void Main(string[] args)
    {
        if (args is null)
        {
            throw new ArgumentNullException(nameof(args));
        }

        using (SimpleWindow laborator4 = new SimpleWindow())
        {
            laborator4.Run(30.0, 0.0);
        }
    }
}