using OpenTK.Input;
using OpenTK;
using System.Drawing;
using System;
using OpenTK.Graphics.OpenGL;

class SimpleWindow : GameWindow
{
    Vector3 povPosition = new Vector3(0, 0, 3); // Poziția inițială a POV
    float povSpeed = 0.1f; // Viteza POV

    Cub cub;

    public SimpleWindow() : base(800, 600)
    {
        cub = new Cub(new Color[] { Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Orange, Color.Purple });
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        GL.ClearColor(Color.MidnightBlue);
        GL.Enable(EnableCap.DepthTest);
        GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);

        GL.Viewport(0, 0, Width, Height);

        double aspect_ratio = Width / (double)Height;

        Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
        GL.MatrixMode(MatrixMode.Projection);
        GL.LoadMatrix(ref perspective);

        Matrix4 lookat = Matrix4.LookAt(30, 30, 30, 0, 0, 0, 0, 1, 0);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref lookat);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Setăm matricea de vizualizare pentru POV
        Matrix4 lookAt = Matrix4.LookAt(povPosition, Vector3.Zero, Vector3.UnitY);
        GL.MatrixMode(MatrixMode.Modelview);
        GL.LoadMatrix(ref lookAt);

        cub.Draw();
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);
        var keyboardState = Keyboard.GetState();

        // Mișcarea POV cu săgețile
        if (keyboardState.IsKeyDown(Key.Left))
        {
            povPosition.X -= povSpeed;
        }
        if (keyboardState.IsKeyDown(Key.Right))
        {
            povPosition.X += povSpeed;
        }
        if (keyboardState.IsKeyDown(Key.Up))
        {
            povPosition.Y += povSpeed;
        }
        if (keyboardState.IsKeyDown(Key.Down))
        {
            povPosition.Y -= povSpeed;
        }

        if (keyboardState.IsKeyDown(Key.Escape))
        {
            Exit();
        }
    }
}