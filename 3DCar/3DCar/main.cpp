#include <stdio.h>
#include <stdlib.h>
#include <GL/freeglut.h>
#include <math.h>
#include <string.h>

/* ASCII code for the escape key. */
#define ESCAPE 27

GLint window;
GLint window2;
GLint Xsize = 1300;
GLint Ysize = 700;
float i, theta;
GLint nml = 0, day = 1;

char name3[] = "Mobil Kijang";

GLfloat xt = 0.0, yt = 0.0, zt = 0.0, xw = 0.0;   /* x,y,z translation */
GLfloat tx = 295, ty = 62;
GLfloat xs = 1.0, ys = 1.0, zs = 1.0;

GLfloat xangle = 0.0, yangle = 0.0, zangle = 0.0, angle = 0.0;   /* axis angles */

GLfloat r = 1, g = 0, b = 0;
int count = 1, flg = 1;
int view = 0;
int flag2 = 0, wheelflag = 0;   //to switch fog effect
GLUquadricObj *t;

static void SpecialKeyFunc(int Key, int x, int y);

/* Simple  transformation routine */
GLvoid Transform(GLfloat Width, GLfloat Height)
{
	glViewport(0, 0, Width, Height);              /* Set the viewport */
	glMatrixMode(GL_PROJECTION);                  /* Select the projection matrix */
	glLoadIdentity();				/* Reset The Projection Matrix */
	gluPerspective(45.0, Width / Height, 0.1, 100.0);  /* Calculate The Aspect Ratio Of The Window */
	glMatrixMode(GL_MODELVIEW);                   /* Switch back to the modelview matrix */
}


/* A general OpenGL initialization function.  Sets all of the initial parameters. */
GLvoid InitGL(GLfloat Width, GLfloat Height)
{

	glClearColor(1.0, 1.0, 1.0, 1.0);
	glLineWidth(2.0);              /* Add line width,   ditto */
	Transform(Width, Height); /* Perform the transformation */
							  //newly added
	t = gluNewQuadric();
	gluQuadricDrawStyle(t, GLU_FILL);

}

/* The function called when our window is resized  */
GLvoid ReSizeGLScene(GLint Width, GLint Height)
{
	if (Height == 0)     Height = 1;                   /* Sanity checks */
	if (Width == 0)      Width = 1;
	Transform(Width, Height);                   /* Perform the transformation */
}

void init()
{
	glClearColor(0, 0, 0, 0);
	glPointSize(5.0);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	glOrtho(0.0, 900.0, 0.0, 600.0, 50.0, -50.0);
	glutPostRedisplay(); 		// request redisplay
}


/* The main drawing function

In here we put all the OpenGL and calls to routines which manipulate
the OpenGL state and environment.

This is the function which will be called when a "redisplay" is requested.
*/



GLvoid DrawGLScene()
{


	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);	/* Clear The Screen And The Depth Buffer */

	glClearColor(1, 1, 1, 1);
	glPushMatrix();
	glLoadIdentity();
	glTranslatef(-1.0, -0.2, -3.5);

	
	//puter
	glPushMatrix();
	glTranslatef(1.0, 0, 0.4);
	glRotatef(yangle, 0.0, 1.0, 0.0);
	glTranslatef(-1.0, 0, -0.4);

	glPushMatrix();
	glTranslatef(0.0, 0.4, 0.35);
	glRotatef(xangle, 1.0, 0.0, 0.0);
	glTranslatef(0.0, -0.4, -0.35);
	

	glRotatef(zangle, 0.0, 0.0, 1.0);
	
	
	
	//glRotatef(xangle, 1.0, 0.0, 0.0);
	//glRotatef(yangle, 0.0, 1.0, 0.0);
	//glRotatef(zangle, 0.0, 0.0, 1.0);



	glTranslatef(xt, yt, zt);
	glScalef(xs, ys, zs);
	glEnable(GL_COLOR_MATERIAL);
	glColorMaterial(GL_FRONT, GL_AMBIENT_AND_DIFFUSE);





	glBegin(GL_QUADS);                /* OBJECT MODULE*/

										/* top of cube*/
										//************************FRONT BODY****************************************
	glColor3f(r, g, b);
	glVertex3f(0.2, 0.4, 0.6);
	glVertex3f(0.6, 0.5, 0.6);
	glVertex3f(0.6, 0.5, 0.2);
	glVertex3f(0.2, 0.4, 0.2);

	/* bottom of cube*/
	glVertex3f(0.2, 0.2, 0.6);
	glVertex3f(0.6, 0.2, 0.6);
	glVertex3f(0.6, 0.2, 0.2);
	glVertex3f(0.2, 0.2, 0.2);

	/* front of cube*/
	glVertex3f(0.2, 0.2, 0.6);
	glVertex3f(0.2, 0.4, 0.6);
	glVertex3f(0.2, 0.4, 0.2);
	glVertex3f(0.2, 0.2, 0.2);

	/* back of cube.*/
	glVertex3f(0.6, 0.2, 0.6);
	glVertex3f(0.6, 0.5, 0.6);
	glVertex3f(0.6, 0.5, 0.2);
	glVertex3f(0.6, 0.2, 0.2);

	/* left of cube*/
	glVertex3f(0.2, 0.2, 0.6);
	glVertex3f(0.6, 0.2, 0.6);
	glVertex3f(0.6, 0.5, 0.6);
	glVertex3f(0.2, 0.4, 0.6);

	/* Right of cube */
	glVertex3f(0.2, 0.2, 0.2);
	glVertex3f(0.6, 0.2, 0.2);
	glVertex3f(0.6, 0.5, 0.2);
	glVertex3f(0.2, 0.4, 0.2);
	//****************************************************************************
	glVertex3f(0.7, 0.65, 0.6);
	glVertex3f(0.7, 0.65, 0.2);
	glVertex3f(1.7, 0.65, 0.2);        //top cover
	glVertex3f(1.7, 0.65, 0.6);
	
	//******************MIDDLE BODY************************************
	glVertex3f(0.6, 0.5, 0.6);
	glVertex3f(0.6, 0.2, 0.6);
	glVertex3f(1.8, 0.2, 0.6);
	glVertex3f(1.8, 0.5, 0.6);

	glVertex3f(1.8, 0.2, 0.6);
	glVertex3f(1.8, 0.2, 0.2);
	glVertex3f(1.8, 0.5, 0.2);
	glVertex3f(1.8, 0.5, 0.6);
	/* bottom of cube*/
	glVertex3f(0.6, 0.2, 0.6);
	glVertex3f(0.6, 0.2, 0.2);
	glVertex3f(1.8, 0.2, 0.2);
	glVertex3f(1.8, 0.2, 0.6);

	/* back of cube.*/
	glVertex3f(0.6, 0.5, 0.2);
	glVertex3f(0.6, 0.2, 0.2);
	glVertex3f(1.8, 0.2, 0.2);
	glVertex3f(1.8, 0.5, 0.2);
	//*********************ENTER WINDOW**********************************
	glColor3f(0.3, 0.3, 0.3);
	glVertex3f(0.77, 0.63, 0.2);
	glVertex3f(0.75, 0.5, 0.2);        //quad front window
	glVertex3f(1.2, 0.5, 0.2);
	glVertex3f(1.22, 0.63, 0.2);

	glVertex3f(1.27, 0.63, .2);
	glVertex3f(1.25, 0.5, 0.2);        //quad back window
	glVertex3f(1.65, 0.5, 0.2);
	glVertex3f(1.67, 0.63, 0.2);

	glColor3f(r, g, b);
	glVertex3f(0.7, 0.65, 0.2);
	glVertex3f(0.7, 0.5, .2);       //first separation
	glVertex3f(0.75, 0.5, 0.2);
	glVertex3f(0.77, 0.65, 0.2);

	glVertex3f(1.2, 0.65, 0.2);
	glVertex3f(1.2, 0.5, .2);       //second separation
	glVertex3f(1.25, 0.5, 0.2);
	glVertex3f(1.27, 0.65, 0.2);

	glVertex3f(1.65, 0.65, 0.2);
	glVertex3f(1.65, 0.5, .2);     //3d separation
	glVertex3f(1.7, 0.5, 0.2);
	glVertex3f(1.7, 0.65, 0.2);

	glVertex3f(0.75, 0.65, 0.2);
	glVertex3f(0.75, 0.63, 0.2);        //line strip
	glVertex3f(1.7, 0.63, 0.2);
	glVertex3f(1.7, 0.65, 0.2);

	glVertex3f(0.75, 0.65, 0.6);
	glVertex3f(0.75, 0.63, 0.6);        //line strip
	glVertex3f(1.7, 0.63, 0.6);
	glVertex3f(1.7, 0.65, 0.6);

	glColor3f(0.3, 0.3, 0.3);
	glVertex3f(0.77, 0.63, 0.6);
	glVertex3f(0.75, 0.5, 0.6);        //quad front window
	glVertex3f(1.2, 0.5, 0.6);
	glVertex3f(1.22, 0.63, 0.6);

	glVertex3f(1.27, 0.63, .6);
	glVertex3f(1.25, 0.5, 0.6);        //quad back window
	glVertex3f(1.65, 0.5, 0.6);
	glVertex3f(1.67, 0.63, 0.6);

	glColor3f(r, g, b);
	glVertex3f(0.7, 0.65, 0.6);
	glVertex3f(0.7, 0.5, .6);       //first separation
	glVertex3f(0.75, 0.5, 0.6);
	glVertex3f(0.77, 0.65, 0.6);

	glVertex3f(1.2, 0.65, 0.6);
	glVertex3f(1.2, 0.5, .6);       //second separation
	glVertex3f(1.25, 0.5, 0.6);
	glVertex3f(1.27, 0.65, 0.6);

	glVertex3f(1.65, 0.65, 0.6);
	glVertex3f(1.65, 0.5, .6);
	glVertex3f(1.7, 0.5, 0.6);
	glVertex3f(1.7, 0.65, 0.6);
	glEnd();


	//**************************************************************
	glBegin(GL_QUADS);

	/* top of cube*/
	glColor3f(0.3, 0.3, 0.3);
	glVertex3f(0.6, 0.5, 0.6);
	glVertex3f(0.6, 0.5, 0.2);        //quad front window
	glVertex3f(0.7, 0.65, 0.2);
	glVertex3f(0.7, 0.65, 0.6);

	glVertex3f(1.7, 0.65, .6);
	glVertex3f(1.7, 0.65, 0.2);        //quad back window
	glVertex3f(1.8, 0.5, 0.2);
	glVertex3f(1.8, 0.5, 0.6);

	glEnd();

	//*************************************************************************************************
	glBegin(GL_TRIANGLES);                /* start drawing the cube.*/

											/* top of cube*/
	glColor3f(0.3, 0.3, 0.3);
	glVertex3f(0.6, 0.5, 0.6);
	glVertex3f(0.7, 0.65, 0.6);       //tri front window
	glVertex3f(0.7, 0.5, 0.6);

	glVertex3f(0.6, 0.5, 0.2);
	glVertex3f(0.7, 0.65, 0.2);       //tri front window
	glVertex3f(0.7, 0.5, 0.2);

	glVertex3f(1.7, 0.65, 0.2);
	glVertex3f(1.8, 0.5, 0.2);       //tri back window
	glVertex3f(1.7, 0.5, 0.2);

	glVertex3f(1.7, 0.65, 0.6);
	glVertex3f(1.8, 0.5, 0.6);       //tri back window
	glVertex3f(1.7, 0.5, 0.6);

	glEnd();

	//********************WHEEL*********************************************

	glColor3f(0.7, 0.7, 0.7);
	glPushMatrix();
	glBegin(GL_LINE_STRIP);
	for (theta = 0; theta<360; theta = theta + 20)
	{
		glVertex3f(0.6, 0.2, 0.62);
		glVertex3f(0.6 + (0.08*(cos(((theta + angle)*3.14) / 180))), 0.2 + (0.08*(sin(((theta + angle)*3.14) / 180))), 0.62);
	}
	glEnd();

	glBegin(GL_LINE_STRIP);
	for (theta = 0; theta<360; theta = theta + 20)
	{
		glVertex3f(0.6, 0.2, 0.18);
		glVertex3f(0.6 + (0.08*(cos(((theta + angle)*3.14) / 180))), 0.2 + (0.08*(sin(((theta + angle)*3.14) / 180))), 0.18);
	}
	glEnd();

	glBegin(GL_LINE_STRIP);
	for (theta = 0; theta<360; theta = theta + 20)
	{
		glVertex3f(1.7, 0.2, 0.18);
		glVertex3f(1.7 + (0.08*(cos(((theta + angle)*3.14) / 180))), 0.2 + (0.08*(sin(((theta + angle)*3.14) / 180))), 0.18);
	}
	glEnd();

	glBegin(GL_LINE_STRIP);
	for (theta = 0; theta<360; theta = theta + 20)
	{
		glVertex3f(1.7, 0.2, 0.62);
		glVertex3f(1.7 + (0.08*(cos(((theta + angle)*3.14) / 180))), 0.2 + (0.08*(sin(((theta + angle)*3.14) / 180))), 0.62);
	}
	glEnd();
	glTranslatef(0.6, 0.2, 0.6);
	glColor3f(0, 0, 0);
	glutSolidTorus(0.025, 0.07, 10, 25);

	glTranslatef(0, 0, -0.4);
	glutSolidTorus(0.025, 0.07, 10, 25);

	glTranslatef(1.1, 0, 0);
	glutSolidTorus(0.025, 0.07, 10, 25);

	glTranslatef(0, 0, 0.4);
	glutSolidTorus(0.025, 0.07, 10, 25);
	glPopMatrix();
	//*************************************************************
	glPopMatrix();
	glEnable(GL_DEPTH_TEST);
	glutPostRedisplay();
	glutSwapBuffers();
	
}

/*  The function called whenever a "normal" key is pressed. */
void NormalKey(GLubyte key, GLint x, GLint y)
{
	switch (key) {

		case ESCAPE: printf("escape pressed. exit.\n");
			glutDestroyWindow(window);	/* Kill our window */
			exit(0);
			break;

		case 'W':                          /* Move up */
			yt += 0.2;
			glutPostRedisplay();
			break;

		case 'S':
			yt -= 0.2;                      /* Move down */
			glutPostRedisplay();
			break;

		case 'w':                          /* Move forward */
			zt += 0.2;
			glutPostRedisplay();
			break;

		case 's':
			zt -= 0.2;                      /* Move away */
			glutPostRedisplay();
			break;

		default:
			break;
	}
}

static void SpecialKeyFunc(int Key, int x, int y)
{
	switch (Key) {
		case GLUT_KEY_DOWN:
			xangle += 5.0;
			glutPostRedisplay();
			break;

		case GLUT_KEY_UP:
			xangle -= 5.0;
			glutPostRedisplay();
			break;

		case GLUT_KEY_RIGHT:
			yangle += 5.0;
			glutPostRedisplay();
			break;

		case GLUT_KEY_LEFT:
			yangle -= 5.0;
			glutPostRedisplay();
			break;
	}
}

void myreshape(int w, int h)
{
	glViewport(0, 0, w, h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	if (w <= h)
		glOrtho(-2.0, 2.0, -2.0*(GLfloat)h / (GLfloat)w, 2.0*(GLfloat)h / (GLfloat)w, -10.0, 10.0);
	else
		glOrtho(-2.0*(GLfloat)w / (GLfloat)h, 2.0*(GLfloat)w / (GLfloat)h, -2.0, 2.0, -10.0, 10.0);
	glMatrixMode(GL_MODELVIEW);
	glutPostRedisplay();
}


//*************************** Main ***************************************************************

int main(int argc, char **argv)
{

	/* Initialisation and window creation */

	glutInit(&argc, argv);               /* Initialize GLUT state. */

	glutInitDisplayMode(GLUT_RGBA |      /* RGB and Alpha */
						GLUT_DOUBLE |     /* double buffer */
						GLUT_DEPTH);     /* Z buffer (depth) */

	glutInitWindowSize(Xsize, Ysize);     /* set initial window size. */
	glutInitWindowPosition(0, 0);         /* upper left corner of the screen. */

	glutCreateWindow("Mobil Kijang"); /* Open a window with a title. */

										  /* Now register the various callback functions */

	glutReshapeFunc(myreshape);
	glutDisplayFunc(DrawGLScene);        /* Function to do all our OpenGL drawing. */
	glutReshapeFunc(ReSizeGLScene);
	glutKeyboardFunc(NormalKey);         /*Normal key is pressed */
	glutSpecialFunc(SpecialKeyFunc);
	InitGL(Xsize, Ysize);


	/* Now drop into the event loop from which we never return */

	glutMainLoop();                      /* Start Event Processing Engine. */
	return 1;
}


