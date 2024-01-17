using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace EGC_Mihalache_3131B
{
    class SimpleWindow : GameWindow
    {
        private Oxyz oxyz;

        private float objectYPosition = 5.0f; // Pozitia initiala

        private Matrix4 closeViewMatrix;
        private Matrix4 farViewMatrix;
        private Matrix4 sideViewMatrix;
        private Matrix4 currentViewMatrix;
        private Matrix4 projectionMatrix;

        private int vertexBuffer;

        private bool useFarView = false;

        public SimpleWindow()
        {
            oxyz = new Oxyz();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1.0f); // Culoare background

            // Setare camera din apropiere
            closeViewMatrix = Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.UnitY);

            // Setare camera din departare
            farViewMatrix = Matrix4.LookAt(new Vector3(0, 0, 20), Vector3.Zero, Vector3.UnitY);

            // Setare camera default
            sideViewMatrix = Matrix4.LookAt(new Vector3(10, 10, 10), Vector3.Zero, Vector3.UnitY);

            // Camera default
            currentViewMatrix = closeViewMatrix;

            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);

            // Creare piramida
            float[] vertices = {
                // Baza - rosu
                -1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f, // Vertex 1
                 1.0f, -1.0f,  1.0f, 1.0f, 0.0f, 0.0f, // Vertex 2
                 1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, // Vertex 3
                -1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, // Vertex 4

                // Varful - verde
                 0.0f,  1.0f,  0.0f, 0.0f, 1.0f, 0.0f, // Vertex 5

                // Fata 1 - albastru
                -1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 1.0f, // Vertex 1
                 1.0f, -1.0f,  1.0f, 0.0f, 0.0f, 1.0f, // Vertex 2
                 0.0f,  1.0f,  0.0f, 0.0f, 0.0f, 1.0f, // Vertex 5

                // Fata 2 - galben
                 1.0f, -1.0f,  1.0f, 1.0f, 1.0f, 0.0f, // Vertex 2
                 1.0f, -1.0f, -1.0f, 1.0f, 1.0f, 0.0f, // Vertex 3
                 0.0f,  1.0f,  0.0f, 1.0f, 1.0f, 0.0f, // Vertex 5

                // Fata 3 - rosu
                 1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, // Vertex 3
                -1.0f, -1.0f, -1.0f, 1.0f, 0.0f, 0.0f, // Vertex 4
                 0.0f,  1.0f,  0.0f, 1.0f, 0.0f, 0.0f, // Vertex 5

                // Fata 4 - verde
                -1.0f, -1.0f, -1.0f, 0.0f, 1.0f, 0.0f, // Vertex 4
                -1.0f, -1.0f,  1.0f, 0.0f, 1.0f, 0.0f, // Vertex 1
                 0.0f,  1.0f,  0.0f, 0.0f, 1.0f, 0.0f  // Vertex 5
            };

            // Buffer
            vertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // Jos la click stanga
            if (Mouse.GetState().IsButtonDown(MouseButton.Left))
            {
                objectYPosition -= 0.1f; // Adjust the speed of movement as needed
            }
            else if (Mouse.GetState().IsButtonDown(MouseButton.Right))
            {
                objectYPosition += 0.1f; // Adjust the speed of movement as needed
            }

            // Limitarea de jos
            objectYPosition = Math.Max(objectYPosition, 0.0f);

            // Schimbare camera
            if (Keyboard.GetState().IsKeyDown(Key.F))
            {
                useFarView = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Key.C))
            {
                useFarView = false;
            }

            // Camera curenta
            if (useFarView)
            {
                // Setare camera default de mai departe
                currentViewMatrix = Matrix4.LookAt(new Vector3(15, 15, 15), Vector3.Zero, Vector3.UnitY);
            }
            else
            {
                // Setare camera default
                currentViewMatrix = sideViewMatrix;
            }

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref currentViewMatrix);

            // Transparenta cub
            GL.Disable(EnableCap.DepthTest);

            oxyz.DrawMe();

            // Transparenta cub
            GL.Enable(EnableCap.DepthTest);

            GL.Translate(0.0, objectYPosition, 0.0); // Setare pozitie cub mai sus

            // Desenare cub
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.VertexPointer(3, VertexPointerType.Float, 6 * sizeof(float), 0);
            GL.ColorPointer(3, ColorPointerType.Float, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.ColorArray);

            GL.DrawArrays(PrimitiveType.Quads, 0, 24);

            GL.DisableClientState(ArrayCap.VertexArray);
            GL.DisableClientState(ArrayCap.ColorArray);

            SwapBuffers();
        }
    }
}