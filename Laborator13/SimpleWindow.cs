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
        private float earthRotationAngle = 0f;
        private float moonOrbitAngle = 0f;
        private float sunRotationAngle = 0f;

        private int earthTexture;
        private int moonTexture;
        private int sunTexture;

        private Matrix4 viewMatrix;
        private Matrix4 projectionMatrix;

        public SimpleWindow() : base(1024, 768)
        {
            Title = "Solar System";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.Black);

            earthTexture = LoadTexture("textures/earth_texture.jpg");
            moonTexture = LoadTexture("textures/moon_texture.jpg");
            sunTexture = LoadTexture("textures/sun_texture.jpg");

            viewMatrix = Matrix4.LookAt(new Vector3(0, 0, 20), Vector3.Zero, Vector3.UnitY);

            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Width, Height);

            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 1.0f, 100.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projectionMatrix);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // Earth rotation
            earthRotationAngle += 1f;
            if (earthRotationAngle >= 360f)
                earthRotationAngle = 0f;

            // Sun rotation
            sunRotationAngle += 0.5f;
            if (sunRotationAngle >= 360f)
                sunRotationAngle = 0f;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Enable(EnableCap.DepthTest);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);

            GL.PushMatrix();
            GL.Rotate(sunRotationAngle, Vector3.UnitY);
            DrawTexturedSphere(2.0f, sunTexture);
            GL.PopMatrix();

            GL.PushMatrix();

            double earthOrbitRadius = 6.0;
            double earthOrbitSpeed = 1.0;
            double earthOrbitAngle = earthRotationAngle * earthOrbitSpeed;

            GL.Translate(earthOrbitRadius * Math.Cos(MathHelper.DegreesToRadians(earthOrbitAngle)),
                         0.0,
                         earthOrbitRadius * Math.Sin(MathHelper.DegreesToRadians(earthOrbitAngle)));

            GL.Rotate(earthRotationAngle, Vector3.UnitY);

            DrawTexturedSphere(1.0f, earthTexture);

            GL.PushMatrix();

            double moonOrbitRadius = 2.0;
            double moonOrbitSpeed = 5.0;
            double moonOrbitAngle = earthRotationAngle * moonOrbitSpeed;

            GL.Translate(moonOrbitRadius * Math.Cos(MathHelper.DegreesToRadians(moonOrbitAngle)),
                         0.0,
                         moonOrbitRadius * Math.Sin(MathHelper.DegreesToRadians(moonOrbitAngle)));

            float moonRotationSpeed = 2.0f;
            GL.Rotate(earthRotationAngle * moonRotationSpeed, Vector3.UnitY);

            DrawTexturedSphere(0.5f, moonTexture);

            GL.PopMatrix();
            GL.PopMatrix();

            GL.Disable(EnableCap.DepthTest);

            SwapBuffers();
        }



        private void DrawTexturedSphere(float radius, int texture)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, texture);

            for (int i = 0; i < 360; i += 10)
            {
                for (int j = -90; j < 90; j += 10)
                {
                    GL.Begin(PrimitiveType.Quads);

                    float u1 = i / 360.0f;
                    float u2 = (i + 10) / 360.0f;
                    float v1 = (j + 90) / 180.0f;
                    float v2 = (j + 100) / 180.0f;

                    GL.TexCoord2(u1, v1);
                    GL.Vertex3(radius * Math.Cos(j * Math.PI / 180) * Math.Cos(i * Math.PI / 180),
                               radius * Math.Cos(j * Math.PI / 180) * Math.Sin(i * Math.PI / 180),
                               radius * Math.Sin(j * Math.PI / 180));

                    GL.TexCoord2(u2, v1);
                    GL.Vertex3(radius * Math.Cos(j * Math.PI / 180) * Math.Cos((i + 10) * Math.PI / 180),
                               radius * Math.Cos(j * Math.PI / 180) * Math.Sin((i + 10) * Math.PI / 180),
                               radius * Math.Sin(j * Math.PI / 180));

                    GL.TexCoord2(u2, v2);
                    GL.Vertex3(radius * Math.Cos((j + 10) * Math.PI / 180) * Math.Cos((i + 10) * Math.PI / 180),
                               radius * Math.Cos((j + 10) * Math.PI / 180) * Math.Sin((i + 10) * Math.PI / 180),
                               radius * Math.Sin((j + 10) * Math.PI / 180));

                    GL.TexCoord2(u1, v2);
                    GL.Vertex3(radius * Math.Cos((j + 10) * Math.PI / 180) * Math.Cos(i * Math.PI / 180),
                               radius * Math.Cos((j + 10) * Math.PI / 180) * Math.Sin(i * Math.PI / 180),
                               radius * Math.Sin((j + 10) * Math.PI / 180));

                    GL.End();
                }
            }

            GL.Disable(EnableCap.Texture2D);
        }


        private int LoadTexture(string path)
        {
            int textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);

            Bitmap bitmap = new Bitmap(path);
            System.Drawing.Imaging.BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bitmap.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return textureID;
        }
    }
}
