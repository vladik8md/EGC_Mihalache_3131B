using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

class SimpleWindow : GameWindow
{
    // Pozitia 0,0
    private float objectX = 0.0f;
    private float objectY = 0.0f;

    // Viteza de miscare la apasarea tastei
    private readonly float objectSpeed = 0.01f;

    // Constructor
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
    }

    protected override void OnLoad(EventArgs e)
    {
        GL.ClearColor(Color.Gray);
    }

    protected override void OnResize(EventArgs e)
    {
        GL.Viewport(0, 0, Width, Height);

        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadIdentity();

        // Modul în care obiectele sunt proiectate pe ecran
        GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Key.W))
            objectY += objectSpeed;
        if (keyboardState.IsKeyDown(Key.S))
            objectY -= objectSpeed;
        if (keyboardState.IsKeyDown(Key.A))
            objectX -= objectSpeed;
        if (keyboardState.IsKeyDown(Key.D))
            objectX += objectSpeed;

        base.OnUpdateFrame(e);
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
        base.OnMouseMove(e);

        objectX = (float)e.X / (float)Width * 2.0f - 1.0f;  // Actualizează poziția obiectului pe axa X în funcție de poziția cursorului.
        objectY = -((float)e.Y / (float)Height * 2.0f - 1.0f);  // Actualizează poziția obiectului pe axa Y în funcție de poziția cursorului.
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Transformare de translație asupra obiectului pentru a-l muta la poziția specificată
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadIdentity();
        GL.Translate(objectX, objectY, 0.0);

        GL.Begin(PrimitiveType.Triangles);

        GL.Color3(Color.Red);
        GL.Vertex2(-1.0f, 1.0f);
        GL.Color3(Color.Green);
        GL.Vertex2(0.0f, -1.0f);
        GL.Color3(Color.Blue);
        GL.Vertex2(1.0f, 1.0f);

        GL.End();

        SwapBuffers();
    }
}