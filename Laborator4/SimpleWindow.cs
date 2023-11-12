using OpenTK;
using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using OpenTK.Input;

namespace EGC_Mihalache_3131B
{
    internal class SimpleWindow : GameWindow
    {
        private Cub cub;
        private Oxyz oxyz;

        public SimpleWindow()
        {
            cub = new Cub();
            oxyz = new Oxyz();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Alpha
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc((BlendingFactor)BlendingFactorSrc.SrcAlpha, (BlendingFactor)BlendingFactorDest.OneMinusSrcAlpha);

            GL.Hint(HintTarget.PointSmoothHint, HintMode.Nicest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        protected override void OnResize(EventArgs e)
        {

            base.OnResize(e);

            GL.ClearColor(Color.Gray);


            // POV
            GL.Viewport(0, 0, Width, Height);
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, this.Width / this.Height, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // Camera
            Matrix4 camera = Matrix4.LookAt(3, 3, 9, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState currentKey = Keyboard.GetState();
            if (currentKey[Key.Escape])
            {
                Exit();
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            cub.Draw();

            oxyz.DrawMe();

            SwapBuffers();
        }

    }
}