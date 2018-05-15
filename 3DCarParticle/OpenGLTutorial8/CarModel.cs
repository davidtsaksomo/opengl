using System;
using Tao.FreeGlut;
using OpenGL;

namespace CarParticle
{
    public class CarModel
    {
        public static VBO<Vector3> cube, cubeNormals, window, windowNormals, wheel1, wheel1Normals, wheel2Normals, wheel2, wheel3, wheel4, velg1, velg2, velg3, velg4;
        public static VBO<Vector2> cubeUV, windowUV, wheel1UV;
        public static VBO<int> cubeQuads, windowQuads, wheel1Quads;
        public static Texture bodyTexture, kacaTexture, rodaTexture;

        public CarModel()
        {
            bodyTexture = new Texture("body.bmp");
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

            cubeQuads = new VBO<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 61, 63, 64, 65, 66, 67, 68, 69, 70, 71, 72, 73, 74, 75, 76, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 106, 107 }, BufferTarget.ElementArrayBuffer);

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

            windowQuads = new VBO<int>(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35 }, BufferTarget.ElementArrayBuffer);


            //wheeel-----------------------------------------------------------------------------------------------
            // Number of segments the circle is divided into.
            const int DIV_COUNT = 32;

            // Will use a triangle fan rooted at the origin to draw the circle. So one additional
            // point is needed for the origin, and another one because the first point is repeated
            // as the last one to close the circle.
            Vector3[] coordA = new Vector3[(DIV_COUNT + 2) * 2];

            // Origin.
            int coordIdx = 0;

            // Calculate angle increment from point to point, and its cos/sin.
            float angInc = DIV_COUNT * 3.1415926535897932384626433832795f / 180;
            float cosInc = (float)Math.Cos(angInc);
            float sinInc = (float)Math.Sin(angInc);
            float radius = 0.15f;
            float depth = 0.1f;
            Vector3 center = new Vector3(0.6f, 0.25f, 0.55f);
            // Start with vector (1.0f, 0.0f), ...

            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            float xc = radius;
            float yc = 0.0f;
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

            wheel1 = new VBO<Vector3>(coordA);      // right

            // Origin.
            coordIdx = 0;
            center = new Vector3(1.5f, 0.25f, 0.55f);
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

            wheel2 = new VBO<Vector3>(coordA);      // right

            // Origin.
            coordIdx = 0;
            center = new Vector3(0.6f, 0.25f, 0.15f);
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
            center = new Vector3(1.5f, 0.25f, 0.15f);
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

            //-------------------------------VELGGGGGG--------------------------------------------------------------------
            // Origin.
            coordIdx = 0;
            center = new Vector3(0.6f, 0.25f, 0.55f);
            // Start with vector (1.0f, 0.0f), ...

            //coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            xc = radius;
            yc = 0.0f;
            for (int iDiv = 0; iDiv < (DIV_COUNT * 2) + 2; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                // coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z);
                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z + depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            //  coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            velg1 = new VBO<Vector3>(coordA);      // right

            // Origin.
            coordIdx = 0;
            center = new Vector3(1.5f, 0.25f, 0.55f);
            // Start with vector (1.0f, 0.0f), ...

            //coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            xc = radius;
            yc = 0.0f;
            for (int iDiv = 0; iDiv < (DIV_COUNT * 2) + 2; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                // coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z);
                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z + depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            //  coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            velg2 = new VBO<Vector3>(coordA);      // right

            // Origin.
            coordIdx = 0;
            center = new Vector3(0.6f, 0.25f, 0.15f);
            // Start with vector (1.0f, 0.0f), ...

            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            //coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            xc = radius;
            yc = 0.0f;
            for (int iDiv = 0; iDiv < (DIV_COUNT * 2) + 2; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z);
                //  coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z + depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            //coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            velg3 = new VBO<Vector3>(coordA);      // right
                                                   // Origin.
            coordIdx = 0;
            center = new Vector3(1.5f, 0.25f, 0.15f);
            // Start with vector (1.0f, 0.0f), ...

            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            //coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            // ... and then rotate it by angInc for each point.
            xc = radius;
            yc = 0.0f;
            for (int iDiv = 0; iDiv < (DIV_COUNT * 2) + 2; ++iDiv)
            {
                float xcNew = cosInc * xc - sinInc * yc;
                yc = sinInc * xc + cosInc * yc;
                xc = xcNew;

                coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z);
                //   coordA[coordIdx++] = new Vector3(xc + center.X, yc + center.Y, center.Z + depth);
            }
            // Start with vector (1.0f, 0.0f), ...
            coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z);
            // coordA[coordIdx++] = new Vector3(center.X + radius, center.Y, center.Z + depth);

            velg4 = new VBO<Vector3>(coordA);      // right

            //---------------------------------------------------------------------------------------------------


            int[] quads = new int[(DIV_COUNT + 2) * 2];
            Vector3[] leftnormals = new Vector3[(DIV_COUNT + 2) * 2];
            Vector3[] rightnormals = new Vector3[(DIV_COUNT + 2) * 2];
            for (int i = 0; i < (DIV_COUNT + 2) * 2; i++)
            {
                leftnormals[i] = new Vector3(0, 0, 1);
                rightnormals[i] = new Vector3(0, 0, -1);
                quads[i] = i;
            }

            Vector2[] UVs = new Vector2[(DIV_COUNT + 2) * 2];
            for (int i = 0; i < (DIV_COUNT + 2) * 2 - 3; i = i + 4)
            {
                UVs[i] = new Vector2(0, 0);
                UVs[i + 1] = new Vector2(1, 0);
                UVs[i + 2] = new Vector2(1, 1);
                UVs[i + 3] = new Vector2(0, 1);
            }
            wheel1Normals = new VBO<Vector3>(leftnormals);
            wheel2Normals = new VBO<Vector3>(rightnormals);
            wheel1UV = new VBO<Vector2>(UVs);
            wheel1Quads = new VBO<int>(quads, BufferTarget.ElementArrayBuffer);
        }
    }
}