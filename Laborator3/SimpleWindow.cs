using System;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

class SimpleWindow : GameWindow
{
    // Valorile maxime si minime pentru RGB
    private float redMin = 0.0f;
    private float redMax = 1.0f;
    private float greenMin = 0.0f;
    private float greenMax = 1.0f;
    private float blueMin = 0.0f;
    private float blueMax = 1.0f;

    // Vector pentru stocarea coordonatelor varfurilor
    private float[] triangleVertices;

    // Check pentru buton apasat, pentru afisare in consola
    private bool keyPressed = false;

    public SimpleWindow() : base(800, 600)
    {
        KeyDown += Keyboard_KeyDown;
    }

    void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
    {
        if (e.Key == Key.Escape)
            this.Exit();

        if (e.Key == Key.F11)
            if (this.WindowState == WindowState.Fullscreen)
                this.WindowState = WindowState.Normal;
            else
                this.WindowState = WindowState.Fullscreen;

        // Schimbare gradient de culoare
        if (e.Key == Key.R)
        {
            ChangeColor(ref redMin, ref redMax);
            keyPressed = true;
        }
        if (e.Key == Key.G)
        {
            ChangeColor(ref greenMin, ref greenMax);
            keyPressed = true;
        }
        if (e.Key == Key.B)
        {
            ChangeColor(ref blueMin, ref blueMax);
            keyPressed = true;
        }
    }

    // Schimbare culori la fiecare apasare pe butoane, folosind o valoare mai mare
    void ChangeColor(ref float min, ref float max)
    {
        min += 0.5f;
        if (min > 1.0f)
        {
            min = 0.0f;
            max += 0.5f;
            if (max > 1.0f)
            {
                max = 0.0f;
            }
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        GL.ClearColor(Color.Gray);

        // Citirea coordonatelor triunghiului din fisier
        if (File.Exists("../../triunghi.txt"))
        {
            string[] lines = File.ReadAllLines("../../triunghi.txt");
            if (lines.Length >= 3)
            {
                triangleVertices = new float[6];
                for (int i = 0; i < 3; i++)
                {
                    string[] vertex = lines[i].Split(' ');
                    triangleVertices[i * 2] = float.Parse(vertex[0]);
                    triangleVertices[i * 2 + 1] = float.Parse(vertex[1]);
                }
            }
        }
        else
        {
            // Daca nu exista fisierul, se creaza si se umple cu date default
            string[] defaultVertices = { "0.0 -1.0", "1.0 1.0", "-1.0 1.0" };
            File.WriteAllLines("../../triunghi.txt", defaultVertices);

            triangleVertices = new float[6];
            for (int i = 0; i < 3; i++)
            {
                string[] vertex = defaultVertices[i].Split(' ');
                triangleVertices[i * 2] = float.Parse(vertex[0]);
                triangleVertices[i * 2 + 1] = float.Parse(vertex[1]);
            }
        }
    }

    protected override void OnResize(EventArgs e)
    {
        GL.Viewport(0, 0, Width, Height);

        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();

        // Modul in care obiectele sunt proiectate pe ecran
        GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        // Afisare pozitie mouse
        Console.WriteLine($"Mouse X: {e.X}, Mouse Y: {e.Y}");
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        GL.Begin(PrimitiveType.Triangles);

        for (int i = 0; i < 3; i++)
        {
            float r = redMin + (i * 0.5f);
            float g = greenMin + (i * 0.5f);
            float b = blueMin + (i * 0.5f);

            // Afisare RGB, doar daca este apasata tasta
            if (keyPressed)
            {
                Console.WriteLine($"Vârf {i + 1}: R = {r}, G = {g}, B = {b}");
            }

            GL.Color3(r, g, b);
            GL.Vertex2(triangleVertices[i * 2], triangleVertices[i * 2 + 1]);
        }

        GL.End();

        GL.Begin(PrimitiveType.Lines);

        // Axa X
        GL.Color3(Color.Red);
        GL.Vertex2(-1.0f, 0.0f);
        GL.Vertex2(1.0f, 0.0f);

        // Axa Y
        GL.Color3(Color.Green);
        GL.Vertex2(0.0f, -1.0f);
        GL.Vertex2(0.0f, 1.0f);

        GL.End();

        SwapBuffers();

        keyPressed = false;
    }
}