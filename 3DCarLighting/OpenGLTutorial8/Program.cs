using System;
using Tao.FreeGlut;
using OpenGL;

namespace OpenGLTutorial8
{
    class Program
    {
        private static int width = 1280, height = 720;
        private static ShaderProgram program;
        private static VBO<Vector3> cube, cubeNormals, window, windowNormals, wheel1, wheel1Normals, wheel2, wheel3, wheel4;
        private static VBO<Vector2> cubeUV, windowUV,wheel1UV;
        private static VBO<int> cubeQuads, windowQuads,wheel1Quads;
        private static Texture glassTexture, kacaTexture, rodaTexture;
        private static System.Diagnostics.Stopwatch watch;
        private static float xangle, yangle;
        private static bool autoRotate, lighting = true, fullscreen = false, alpha = true;
        private static bool left, right, up, down;
        private static float ambient = 0.3f;
        private static float maxdiffuse = 1f;


        static void Main(string[] args)
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
            Glut.glutInitWindowSize(width, height);
            Glut.glutCreateWindow("Texture 3D Car");

            Glut.glutIdleFunc(OnRenderFrame);
            Glut.glutDisplayFunc(OnDisplay);

            Glut.glutKeyboardFunc(OnKeyboardDown);
            Glut.glutKeyboardUpFunc(OnKeyboardUp);

            Glut.glutCloseFunc(OnClose);
            Glut.glutReshapeFunc(OnReshape);

            Gl.Disable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            program = new ShaderProgram(VertexShader, FragmentShader);

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, new Vector3(0, 1, 0)));

            program["light_direction"].SetValue(new Vector3(0, 0, 1));
            program["enable_lighting"].SetValue(lighting);

            glassTexture = new Texture("glass.bmp");
            kacaTexture = new Texture("kaca.bmp");
            rodaTexture = new Texture("tire.bmp");

            cube = new VBO<Vector3>(new Vector3[] {

	        /* top of cube.*/
	        new Vector3 (0.2f, 0.4f, 0.6f),
            new Vector3 (0.6f, 0.5f, 0.6f),
            new Vector3 (0.6f, 0.5f, 0.2f),
            new Vector3 (0.2f, 0.4f, 0.2f),

	        /* bottom of, cube*/
	        new Vector3 (0.2f, 0.2f, 0.6f),
            new Vector3 (0.6f, 0.2f, 0.6f),
            new Vector3 (0.6f, 0.2f, 0.2f),
            new Vector3 (0.2f, 0.2f, 0.2f),

	        /* front of cube.*/
            new Vector3 (0.2f, 0.2f, 0.6f),
            new Vector3 (0.2f, 0.4f, 0.6f),
            new Vector3 (0.2f, 0.4f, 0.2f),
            new Vector3 (0.2f, 0.2f, 0.2f),

	        /* back of cube.*/
	        new Vector3 (0.6f, 0.2f, 0.6f),
            new Vector3 (0.6f, 0.5f, 0.6f),
            new Vector3 (0.6f, 0.5f, 0.2f),
            new Vector3 (0.6f, 0.2f, 0.2f),

	        /* left of cube*/
	        new Vector3 (0.2f, 0.2f, 0.6f),
            new Vector3 (0.6f, 0.2f, 0.6f),
            new Vector3 (0.6f, 0.5f, 0.6f),
            new Vector3 (0.2f, 0.4f, 0.6f),

	        /* Right of cube */
	        new Vector3 (0.2f, 0.2f, 0.2f),
            new Vector3 (0.6f, 0.2f, 0.2f),
            new Vector3 (0.6f, 0.5f, 0.2f),
            new Vector3 (0.2f, 0.4f, 0.2f),
	        //****************************************************************************
	        new Vector3 (0.7f, 0.65f, 0.6f),
            new Vector3 (0.7f, 0.65f, 0.2f),
            new Vector3 (1.7f, 0.65f, 0.2f),        //top cover
	        new Vector3 (1.7f, 0.65f, 0.6f),
	
	        //******************MIDDLE BODY************************************
	        new Vector3 (0.6f, 0.5f, 0.6f),
            new Vector3 (0.6f, 0.2f, 0.6f),
            new Vector3 (1.8f, 0.2f, 0.6f),
            new Vector3 (1.8f, 0.5f, 0.6f),

            new Vector3 (1.8f, 0.2f, 0.6f),
            new Vector3 (1.8f, 0.2f, 0.2f),
            new Vector3 (1.8f, 0.5f, 0.2f),
            new Vector3 (1.8f, 0.5f, 0.6f),
	        /* bottom of cube*/
	        new Vector3 (0.6f, 0.2f, 0.6f),
            new Vector3 (0.6f, 0.2f, 0.2f),
            new Vector3 (1.8f, 0.2f, 0.2f),
            new Vector3 (1.8f, 0.2f, 0.6f),

	        /* back of cube.*/
	        new Vector3 (0.6f, 0.5f, 0.2f),
            new Vector3 (0.6f, 0.2f, 0.2f),
            new Vector3 (1.8f, 0.2f, 0.2f),
            new Vector3 (1.8f, 0.5f, 0.2f),

	        new Vector3 (0.7f, 0.65f, 0.2f),
            new Vector3 (0.7f, 0.5f, .2f),       //first separation
	        new Vector3 (0.75f, 0.5f, 0.2f),
            new Vector3 (0.77f, 0.65f, 0.2f),

            new Vector3 (1.2f, 0.65f, 0.2f),
            new Vector3 (1.2f, 0.5f, .2f),       //second separation
	        new Vector3 (1.25f, 0.5f, 0.2f),
            new Vector3 (1.27f, 0.65f, 0.2f),

            new Vector3 (1.65f, 0.65f, 0.2f),
            new Vector3 (1.65f, 0.5f, .2f),     //3d separation
	        new Vector3 (1.7f, 0.5f, 0.2f),
            new Vector3 (1.7f, 0.65f, 0.2f),

            new Vector3 (0.75f, 0.65f, 0.2f),
            new Vector3 (0.75f, 0.63f, 0.2f),        //line strip
	        new Vector3 (1.7f, 0.63f, 0.2f),
            new Vector3 (1.7f, 0.65f, 0.2f),

            new Vector3 (0.75f, 0.65f, 0.6f),
            new Vector3 (0.75f, 0.63f, 0.6f),        //line strip
	        new Vector3 (1.7f, 0.63f, 0.6f),
            new Vector3 (1.7f, 0.65f, 0.6f),


	        new Vector3 (0.7f, 0.65f, 0.6f),
            new Vector3 (0.7f, 0.5f, .6f),       //first separation
	        new Vector3 (0.75f, 0.5f, 0.6f),
            new Vector3 (0.77f, 0.65f, 0.6f),

            new Vector3 (1.2f, 0.65f, 0.6f),
            new Vector3 (1.2f, 0.5f, .6f),       //second separation
	        new Vector3 (1.25f, 0.5f, 0.6f),
            new Vector3 (1.27f, 0.65f, 0.6f),

            new Vector3 (1.65f, 0.65f, 0.6f),
            new Vector3 (1.65f, 0.5f, .6f),
            new Vector3 (1.7f, 0.5f, 0.6f), //3d separation
            new Vector3 (1.7f, 0.65f, 0.6f),



            });      // right


            cubeNormals = new VBO<Vector3>(new Vector3[] {
                new Vector3(-0.2f, 1, 0), new Vector3(-0.2f, 1, 0), new Vector3(-0.2f, 1, 0), new Vector3(-0.2f, 1, 0),
                new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0),
                new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0),
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),

                new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0),
                new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0),
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),

                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),

                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),

                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1)
            });
            cubeUV = new VBO<Vector2>(new Vector2[] {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) });

            cubeQuads = new VBO<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27,28,29,30,31,32,33,34,35,36,37,38,39,40,41,42,43,44,45,46,47,48,49,50,51,52,53,54,55,56,57,58,59,60,61,61,63,64,65,66,67,68,69,70,71,72,73,74,75,76,78,79,80,81,82,83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107}, BufferTarget.ElementArrayBuffer);

            window = new VBO<Vector3>(new Vector3[] {
            new Vector3 (0.77f, 0.63f, 0.2f),
            new Vector3 (0.75f, 0.5f, 0.2f),        //quad front window
	        new Vector3 (1.2f, 0.5f, 0.2f),
            new Vector3 (1.22f, 0.63f, 0.2f),

            new Vector3 (1.27f, 0.63f, .2f),
            new Vector3 (1.25f, 0.5f, 0.2f),        //quad back window
	        new Vector3 (1.65f, 0.5f, 0.2f),
            new Vector3 (1.67f, 0.63f, 0.2f),


            new Vector3 (0.77f, 0.63f, 0.6f),
            new Vector3 (0.75f, 0.5f, 0.6f),        //quad front window
	        new Vector3 (1.2f, 0.5f, 0.6f),
            new Vector3 (1.22f, 0.63f, 0.6f),

            new Vector3 (1.27f, 0.63f, 0.6f),
            new Vector3 (1.25f, 0.5f, 0.6f),        //quad back window
	        new Vector3 (1.65f, 0.5f, 0.6f),
            new Vector3 (1.67f, 0.63f, 0.6f),


            new Vector3 (0.6f, 0.5f, 0.6f),
            new Vector3 (0.6f, 0.5f, 0.2f),        //quad f,ront window
	        new Vector3 (0.7f, 0.65f, 0.2f),
            new Vector3 (0.7f, 0.65f, 0.6f),

            new Vector3 (1.7f, 0.65f, .6f),
            new Vector3 (1.7f, 0.65f, 0.2f),        //quad back window
	        new Vector3 (1.8f, 0.5f, 0.2f),
            new Vector3 (1.8f, 0.5f, 0.6f),

            new Vector3 (0.6f, 0.5f, 0.6f),
            new Vector3 (0.7f, 0.65f, 0.6f),       //tri f,ront window
	        new Vector3 (0.7f, 0.5f, 0.6f),
	        new Vector3 (0.7f, 0.5f, 0.6f),

            new Vector3 (0.6f, 0.5f, 0.2f),
            new Vector3 (0.7f, 0.65f, 0.2f),       //tri f,ront window
	        new Vector3 (0.7f, 0.5f, 0.2f),
            new Vector3 (0.7f, 0.5f, 0.2f),

            new Vector3 (1.7f, 0.65f, 0.2f),
            new Vector3 (1.8f, 0.5f, 0.2f),       //tri back window
	        new Vector3 (1.7f, 0.5f, 0.2f),
            new Vector3 (1.7f, 0.5f, 0.2f),

            new Vector3 (1.7f, 0.65f, 0.6f),
            new Vector3 (1.8f, 0.5f, 0.6f),       //tri back window
	        new Vector3 (1.7f, 0.5f, 0.6f),
            new Vector3 (1.7f, 0.5f, 0.6f)

            });      // right


            windowNormals = new VBO<Vector3>(new Vector3[] {
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
               new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
               new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(-1, 0.2f, 0), new Vector3(-1, 0.2f, 0), new Vector3(-1, 0.2f, 0), new Vector3(-1, 0.2f, 0),
                new Vector3(1, 0.2f, 0), new Vector3(1, 0.2f, 0), new Vector3(1, 0.2f, 0), new Vector3(1, 0.2f, 0),
                  new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1),
                new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
               new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1),
                new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1)
            });
            windowUV = new VBO<Vector2>(new Vector2[] {
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) });

            windowQuads = new VBO<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35}, BufferTarget.ElementArrayBuffer);


            //wheeel-----------------------------------------------------------------------------------------------
            // Number of segments the circle is divided into.
            const int DIV_COUNT = 32;

            // Will use a triangle fan rooted at the origin to draw the circle. So one additional
            // point is needed for the origin, and another one because the first point is repeated
            // as the last one to close the circle.
            Vector3[] coordA = new Vector3[(DIV_COUNT + 2)*2];

            // Origin.
            int coordIdx = 0;

            // Calculate angle increment from point to point, and its cos/sin.
            float angInc = DIV_COUNT * 3.1415926535897932384626433832795f / 180;
            float cosInc = (float) Math.Cos(angInc);
            float sinInc = (float )Math.Sin(angInc);
            float radius = 0.1f;
            float depth = 0.1f;
            Vector3 center = new Vector3(0.6f,0.2f,0.55f);
            // Start with vector (1.0f, 0.0f), ...

            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z+ depth);

            // ... and then rotate it by angInc for each point.
            float xc = radius;
            float yc = 0.0f;
            for (int iDiv = 0; iDiv < DIV_COUNT; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                coordA[coordIdx++] = new Vector3(xc+center.X, yc+center.Y, center.Z);
                coordA[coordIdx++] = new Vector3(xc+center.X, yc+center.Y, center.Z+depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            wheel1 = new VBO<Vector3>(coordA);      // right


            // Origin.
            coordIdx = 0;
            center = new Vector3(1.5f, 0.2f, 0.55f);
            // Start with vector (1.0f, 0.0f), ...

            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            xc = radius;
            yc = 0.0f;
            for (int iDiv = 0; iDiv < DIV_COUNT; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z);
                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z + depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            wheel2= new VBO<Vector3>(coordA);      // right

            // Origin.
            coordIdx = 0;
            center = new Vector3(0.6f, 0.2f, 0.15f);
            // Start with vector (1.0f, 0.0f), ...

            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            xc = radius;
            yc = 0.0f;
            for (int iDiv = 0; iDiv < DIV_COUNT; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z);
                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z + depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            wheel3 = new VBO<Vector3>(coordA);      // right
                                                    // Origin.
            coordIdx = 0;
            center = new Vector3(1.5f, 0.2f, 0.15f);
            // Start with vector (1.0f, 0.0f), ...

            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            xc = radius;
            yc = 0.0f;
            for (int iDiv = 0; iDiv < DIV_COUNT; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z);
                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z + depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            wheel4 = new VBO<Vector3>(coordA);      // right

            int[] quads = new int[(DIV_COUNT + 2) * 2];
            Vector3[] normals = new Vector3[(DIV_COUNT + 2) * 2];
            for (int i = 0; i < (DIV_COUNT + 2) * 2; i++)
            {
                normals[i] = new Vector3(0, 1, 0);
                quads[i] = i;
            }

            Vector2[] UVs = new Vector2[(DIV_COUNT + 2) * 2];
            for (int i = 0; i < (DIV_COUNT + 2)*2-3; i = i + 4)
            {
                UVs[i] = new Vector2(0, 0);
                UVs[i + 1] = new Vector2(1, 0);
                UVs[i + 2] = new Vector2(1, 1);
                UVs[i + 3] = new Vector2(0, 1);
            }
            wheel1Normals = new VBO<Vector3>(normals);
            wheel1UV = new VBO<Vector2>(UVs);
            wheel1Quads = new VBO<int>(quads, BufferTarget.ElementArrayBuffer);

            watch = System.Diagnostics.Stopwatch.StartNew();

            Glut.glutMainLoop();
        }

        private static void OnClose()
        {
            cube.Dispose();
            cubeNormals.Dispose();
            cubeUV.Dispose();
            cubeQuads.Dispose();
            glassTexture.Dispose();
            kacaTexture.Dispose();
            rodaTexture.Dispose();
            program.DisposeChildren = true;
            program.Dispose();
        }

        private static void OnDisplay()
        {

        }

        private static void OnRenderFrame()
        {
            watch.Stop();
            float deltaTime = (float)watch.ElapsedTicks / System.Diagnostics.Stopwatch.Frequency;
            watch.Restart();

            // perform rotation of the cube depending on the keyboard state
            if (autoRotate)
            {
                xangle += deltaTime / 2;
                yangle += deltaTime;
            }
            if (right) yangle += deltaTime;
            if (left) yangle -= deltaTime;
            if (up) xangle -= deltaTime;
            if (down) xangle += deltaTime;

            // set up the viewport and clear the previous depth and color buffers
            Gl.Viewport(0, 0, width, height);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // make sure the shader program and texture are being used
            Gl.UseProgram(program);
            Gl.BindTexture(glassTexture);

            // set up the model matrix and draw the cube
            program["model_matrix"].SetValue(Matrix4.CreateRotationY(yangle) * Matrix4.CreateRotationX(xangle));
            program["enable_lighting"].SetValue(lighting);

            Gl.BindBufferToShaderAttribute(cube, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(cubeNormals, program, "vertexNormal");
            Gl.BindBufferToShaderAttribute(cubeUV, program, "vertexUV");
            Gl.BindBuffer(cubeQuads);
            
            Gl.DrawElements(BeginMode.Quads, cubeQuads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);


            //Draw window
            Gl.BindTexture(kacaTexture);

            // set up the model matrix and draw the cube
            program["model_matrix"].SetValue(Matrix4.CreateRotationY(yangle) * Matrix4.CreateRotationX(xangle));
            program["enable_lighting"].SetValue(lighting);
            program["max_diffuse"].SetValue(maxdiffuse);
            program["ambient"].SetValue(ambient);

            Gl.BindBufferToShaderAttribute(window, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(windowNormals, program, "vertexNormal");
            Gl.BindBufferToShaderAttribute(windowUV, program, "vertexUV");
            Gl.BindBuffer(windowQuads);

            Gl.DrawElements(BeginMode.Quads, windowQuads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            //drawwheel
            Gl.BindTexture(rodaTexture);

            Gl.BindBufferToShaderAttribute(wheel1, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(wheel1Normals, program, "vertexNormal");
            Gl.BindBufferToShaderAttribute(wheel1UV, program, "vertexUV");
            Gl.BindBuffer(wheel1Quads);
            Gl.DrawElements(BeginMode.TriangleStrip, wheel1Quads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.BindBufferToShaderAttribute(wheel2, program, "vertexPosition");
            Gl.DrawElements(BeginMode.TriangleStrip, wheel1Quads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.BindBufferToShaderAttribute(wheel3, program, "vertexPosition");
            Gl.DrawElements(BeginMode.TriangleStrip, wheel1Quads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            Gl.BindBufferToShaderAttribute(wheel4, program, "vertexPosition");
            Gl.DrawElements(BeginMode.TriangleStrip, wheel1Quads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);

            Glut.glutSwapBuffers();
        }

        private static void OnReshape(int width, int height)
        {
            Program.width = width;
            Program.height = height;

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)width / height, 0.1f, 1000f));
        }

        private static void OnKeyboardDown(byte key, int x, int y)
        {
            if (key == 'w') up = true;
            else if (key == 's') down = true;
            else if (key == 'd') right = true;
            else if (key == 'a') left = true;
            else if (key == 27) Glut.glutLeaveMainLoop();
        }

        private static void OnKeyboardUp(byte key, int x, int y)
        {
            if (key == 'w') up = false;
            else if (key == 's') down = false;
            else if (key == 'd') right = false;
            else if (key == 'a') left = false;
            else if (key == ' ') autoRotate = !autoRotate;
            else if (key == 'l') lighting = !lighting;
            else if (key == 'f')
            {
                fullscreen = !fullscreen;
                if (fullscreen) Glut.glutFullScreen();
                else
                {
                    Glut.glutPositionWindow(0, 0);
                    Glut.glutReshapeWindow(1280, 720);
                }
            }
            else if (key == 'b')
            {
                alpha = !alpha;
                if (alpha)
                {
                    Gl.Enable(EnableCap.Blend);
                    Gl.Disable(EnableCap.DepthTest);
                }
                else
                {
                    Gl.Disable(EnableCap.Blend);
                    Gl.Enable(EnableCap.DepthTest);
                }
            }
        }

        public static string VertexShader = @"
#version 130

in vec3 vertexPosition;
in vec3 vertexNormal;
in vec2 vertexUV;

out vec3 normal;
out vec2 uv;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

void main(void)
{
    normal = normalize((model_matrix * vec4(floor(vertexNormal), 0)).xyz);
    uv = vertexUV;

    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}
";

        public static string FragmentShader = @"
#version 130

uniform sampler2D texture;
uniform vec3 light_direction;
uniform bool enable_lighting;
uniform float ambient;
uniform float max_diffuse;

in vec3 normal;
in vec2 uv;

out vec4 fragment;

void main(void)
{
    float diffuse = max(dot(normal, light_direction), 0) * max_diffuse;
    float lighting = (enable_lighting ? max(diffuse, ambient) : 1);

    // add in some blending for tutorial 8 by setting the alpha to 0.5
    fragment = vec4(lighting * texture2D(texture, uv).xyz, 0.5);
}
";
    }
}
