using System;
using System.Collections.Generic;
using Tao.FreeGlut;
using OpenGL;

namespace CarParticle
{
    public class Smoke
    {
        public static int width = 1280, height = 720;
        public static bool fullscreen = false, rainbow = false;
        public static System.Diagnostics.Stopwatch watch;

        public static ShaderProgram program;
        public static Texture particleTexture;
        public static VBO<Vector3> particleVertices;
        public static VBO<Vector3> particleColors;
        public static VBO<int> particlePoints;

        public static List<Particle> particles = new List<Particle>();
        public static int particleCount = 100;
        public static Vector3[] particlePositions = new Vector3[particleCount];
        public static Random generator = new Random();



        public Smoke()
        {
            // create our shader program
            particles.Clear();
            program = new ShaderProgram(VertexShader, FragmentShader);

            // set up the projection and view matrix
            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 6), new Vector3(1.2f, 0.2f, 0f), new Vector3(0, 1, 0)));

            program["model_matrix"].SetValue(Matrix4.Identity);
            program["static_colors"].SetValue(true);

            // load the particle texture
            particleTexture = new Texture("smoke.bmp");

            // set up the particlePoints VBO, which will stay constant
            int[] points = new int[particleCount];
            for (int i = 0; i < points.Length; i++) points[i] = i;
            particlePoints = new VBO<int>(points, BufferTarget.ElementArrayBuffer);

            // set up the particleColors, which we'll just keep static
            Vector3[] colors = new Vector3[particleCount];
            //for (int i = 0; i < colors.Length; i++) colors[i] = new Vector3((float)generator.NextDouble(), (float)generator.NextDouble(), (float)generator.NextDouble());
            for (int i = 0; i < colors.Length; i++) colors[i] = new Vector3(0.5f, 0.5f, 0.5f);
            particleColors = new VBO<Vector3>(colors);

            // build up our first batch of 1000 particles and 1000 static colors
            for (int i = 0; i < particleCount; i++) particles.Add(new Particle(new Vector3(100f, 0.5f, 0.6f), 0));



            watch = System.Diagnostics.Stopwatch.StartNew();

        }

        public static void OnClose()
        {
            particleVertices.Dispose();
            particleColors.Dispose();
            particlePoints.Dispose();
            program.DisposeChildren = true;
            program.Dispose();
        }

        public class Particle
        {
            public float Life;
            public Vector3 Position;
            public Vector3 Direction;

            public Particle(Vector3 origin, float life = 2)
            {
                Position = origin;
                Direction = new Vector3(3 + (float)generator.NextDouble(), (float)generator.NextDouble() * 2 - 1, (float)generator.NextDouble() * 2 - 1);
                Life = life + (float)generator.NextDouble() * 2;
            }

            public void Update(float delta)
            {
                Direction += delta * new Vector3(0, 3, 0);
                Position += Direction * delta;
                Life -= delta;
            }
        }



        public static string VertexShader = @"
#version 130

in vec3 vertexPosition;
in vec3 vertexColor;

out vec3 color;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;
uniform bool static_colors;
uniform float mixer;

void main(void)
{
    color = (static_colors ? vertexColor : vec3(1 - (1*clamp(mixer,0,1)), 1*clamp(mixer-1,0,1), 1* clamp(mixer,0,1)));
   
    if (vertexPosition.y > 1) {
        gl_PointSize = 0;
    } else if (vertexPosition.y > 0.5) {
        gl_PointSize = 25;
    } else {
        gl_PointSize = 50;
    } 

    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition.xyz, 1);
}
";

        public static string FragmentShader = @"
#version 130

uniform sampler2D texture;

in vec3 color;

out vec4 fragment;

void main(void)
{
    fragment = vec4(color * texture2D(texture, gl_PointCoord).xyz, 1);
}
";
    }
}
