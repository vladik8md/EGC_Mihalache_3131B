using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace EGC_Mihalache_3131B
{
    internal class Cub
    {
        private int intVertex;

        private Vector3 objVertex;
        
        // Varfurile cubului
        private Vector3[] vectorVertex = new Vector3[]
        {
            new Vector3(-0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, -0.5f, -0.5f),
            new Vector3(0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, 0.5f, -0.5f),
            new Vector3(-0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, -0.5f, 0.5f),
            new Vector3(0.5f, 0.5f, 0.5f),
            new Vector3(-0.5f, 0.5f, 0.5f)
        };

        // Culorile fetelor
        private Color4[] faceColor = new Color4[]
         {
            new Color4(0.5f, 0.2f, 0.8f, 0.7f),
            new Color4(0.9f, 0.4f, 0.1f, 0.3f),
            new Color4(0.1f, 0.7f, 0.6f, 0.2f),
            new Color4(0.3f, 0.5f, 0.8f, 0.9f),
            new Color4(0.6f, 0.3f, 0.4f, 0.1f),
            new Color4(0.8f, 0.1f, 0.4f, 0.6f)
         };

        private int[][] face = new int[][]
        {
            new int[] { 0, 1, 2, 7 }, 
            new int[] { 4, 5, 6, 7 }, 
            new int[] { 0, 4, 7, 3 }, 
            new int[] { 1, 5, 6, 2 }, 
            new int[] { 0, 1, 5, 4 }, 
            new int[] { 2, 3, 7, 6 } 
        };
        public Cub()
        {

            this.intVertex = 0;
            this.objVertex = vectorVertex[intVertex];
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);

            for (int i = 0; i < face.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    intVertex = face[i][j];
                    objVertex = vectorVertex[intVertex];
                    GL.Color4(faceColor[i]);
                    GL.Vertex3(objVertex);
                }
            }

            GL.End();
        }
    }
}