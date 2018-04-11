/*
*  As a first example of using OpenGL in C, this program draws the
*  classic red/green/blue triangle.  It uses the default OpenGL
*  coordinate system, in which x, y, and z are limited to the range
*  -1 to 1, and the positive z-axis points into the screen.  Note
*  that this coordinate system is hardly ever used in practice.
*
*  When compiling this program, you must link it to the OpenGL library
*  and to the glut library. For example, in Linux using the gcc compiler,
*  it can be compiled with the command:
*
*          gcc -o first-triangle first-triangle.c -lGL -lglut
*/
#include <GL/freeglut.h>   // freeglut.h might be a better alternative, if available.
#include <stdio.h>
#include <algorithm>
#include <fstream>
#define width 1300
#define height 700
class Point {
public:
	float x;
	float y;

	Point() {
		x = 0;
		y = 0;
	}
	Point(float _x, float _y) {
		x = _x;
		y = _y;
	}
};
class Vertices {
public:
	Point * vertices;
	int neff;
	Vertices() {
		neff = 0;
	}
	Vertices(int count) {
		neff = 0;
		vertices = new Point[count];
	}
};

static GLfloat ANGLE = 0;


Vertices readVertexFromFIle(const char* fileName) {
	FILE* externalFile;
	fopen_s(&externalFile, fileName, "r");

	if (externalFile == NULL) {
		printf("[VectorPath.c => createVectorPathFromFile()] Failed to open external file\n");
		return Vertices();
	}
	std::ifstream inFile(fileName);
	int count = std::count(std::istreambuf_iterator<char>(inFile),
		std::istreambuf_iterator<char>(), ',');

	Vertices vertices = Vertices(count + 1);
	float _x, _y;


	int i = 0;
	while (fscanf_s(externalFile, "%f,%f\n", &_x, &_y) == 2) {
		vertices.vertices[i].x = (_x / width) * 2 - 1;
		vertices.vertices[i].y = ((_y / height) * 2 - 1)*-1;
		i++;
	}
	vertices.neff = i;
	fclose(externalFile);
	return vertices;
}

void drawVector(Vertices * vertices) {
	glBegin(GL_POLYGON);
	int i = 0;
	while (i < (vertices->neff)) {
		glVertex2f(vertices->vertices[i].x, vertices->vertices[i].y);
		i++;
	}
	glEnd();
}

void drawLine(Vertices * vertices) {
	glBegin(GL_LINE_LOOP);
	int i = 0;
	while (i < (vertices->neff)) {
		glVertex2f(vertices->vertices[i].x, vertices->vertices[i].y);
		i++;
	}
	glEnd();
}

void display() {  // Display function will draw the image.	

				  //read from file
	Vertices garis1 = readVertexFromFIle("resources/garis1.txt");
	Vertices garis2 = readVertexFromFIle("resources/garis2.txt");
	Vertices garismobil = readVertexFromFIle("resources/garismobil.txt");
	Vertices gedung = readVertexFromFIle("resources/gedung.txt");
	Vertices jalan = readVertexFromFIle("resources/jalan.txt");
	Vertices kacabelakang = readVertexFromFIle("resources/kacabelakang.txt");
	Vertices kacadepan = readVertexFromFIle("resources/kacadepan.txt");
	Vertices lampubelakang = readVertexFromFIle("resources/lampubelakang.txt");
	Vertices lampudepan1 = readVertexFromFIle("resources/lampudepan1.txt");
	Vertices lampudepan2 = readVertexFromFIle("resources/lampudepan2.txt");
	Vertices sasismobil = readVertexFromFIle("resources/sasismobil.txt");
	Vertices spion = readVertexFromFIle("resources/spion.txt");
	Vertices rodadepan = readVertexFromFIle("resources/rodadepan.txt");
	Vertices rodakecildepan = readVertexFromFIle("resources/rodakecildepan.txt");
	Vertices velgdepan = readVertexFromFIle("resources/velgdepan.txt");
	Vertices rodabelakang = readVertexFromFIle("resources/rodabelakang.txt");
	Vertices rodakecilbelakang = readVertexFromFIle("resources/rodakecilbelakang.txt");
	Vertices velgbelakang = readVertexFromFIle("resources/velgbelakang.txt");

	//Langit
	glClearColor(135.0 / 255.0, 206.0 / 255.0, 250.0 / 255.0, 1);  // sky color
	glClear(GL_COLOR_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);

	//Draw Gedung
	glEnable(GL_STENCIL_TEST);
	glColorMask(GL_FALSE, GL_FALSE, GL_FALSE, GL_FALSE);
	glStencilFunc(GL_ALWAYS, 0, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_INVERT);
	glStencilMask(1);
	drawVector(&gedung);
	glColorMask(GL_TRUE, GL_TRUE, GL_TRUE, GL_TRUE);
	glStencilFunc(GL_EQUAL, 1, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_KEEP);
	glColor3f(11.0 / 255.0, 61.0 / 255.0, 170.0 / 255.0);
	drawVector(&gedung);
	glDisable(GL_STENCIL_TEST);

	//Draw Jalan
	glColor3f(113.0 / 255.0, 113.0 / 255.0, 115.0 / 255.0);
	drawVector(&jalan);
	glColor3f(1, 1, 1);//putih
	drawVector(&garis1);
	drawVector(&garis2);

	//Draw Mobil
	glEnable(GL_STENCIL_TEST);
	glColorMask(GL_FALSE, GL_FALSE, GL_FALSE, GL_FALSE);
	glStencilFunc(GL_ALWAYS, 0, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_INVERT);
	glStencilMask(1);
	drawVector(&sasismobil);
	glColorMask(GL_TRUE, GL_TRUE, GL_TRUE, GL_TRUE);
	glStencilFunc(GL_EQUAL, 1, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_KEEP);
	glColor3f(237.0 / 255.0, 2.0 / 255.0, 2.0 / 255.0);
	drawVector(&sasismobil);
	glDisable(GL_STENCIL_TEST);

	glColor3f(255.0 / 255.0, 145.0 / 255.0, 12.0 / 255.0);
	drawVector(&lampubelakang);
	glColor3f(20.0 / 255.0, 2.0 / 255.0, 20.0 / 255.0);
	drawVector(&kacabelakang);
	drawVector(&kacadepan);
	glColor3f(199.0 / 255.0, 202.0 / 255.0, 207.0 / 255.0);
	drawVector(&lampudepan1);
	glColor3f(0, 0, 0);
	drawVector(&lampudepan2);
	drawVector(&rodabelakang);
	drawVector(&rodadepan);
	glColor3f(0.9, 0.9, 0.9);
	drawVector(&rodakecilbelakang);
	drawVector(&rodakecildepan);

	glColor3f(0.3, 0.3, 0.3);

	glMatrixMode(GL_MODELVIEW);
	glPushMatrix();
	glTranslatef(-0.29, -0.34, 0);
	glRotatef(ANGLE, 0.0, 0.0, 1.0);
	glTranslatef(0.29, 0.34, 0);
	drawVector(&velgbelakang);
	glPopMatrix();

	glPushMatrix();
	glTranslatef(0.29, -0.34, 0);
	glRotatef(ANGLE, 0.0, 0.0, 1.0);
	glTranslatef(-0.29, 0.34, 0);
	drawVector(&velgdepan);
	glPopMatrix();

	glColor3f(237.0 / 255.0, 2.0 / 255.0, 2.0 / 255.0);
	drawVector(&spion);


	glutSwapBuffers(); // Required to copy color buffer onto the screen.
}

void managerIdle(void)
{
	Sleep(5.0);
	ANGLE -= 10;
	glutPostRedisplay();
}

int main(int argc, char** argv) {  // Initialize GLUT

	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE);    // Use single color buffer and no depth buffer.
	glutInitWindowSize(1300, 700);         // Size of display area, in pixels.
	glutInitWindowPosition(0, 0);     // Location of window in screen coordinates.
	glutCreateWindow("GL Cars"); // Parameter is window title.
	glutDisplayFunc(display);            // Called when the window needs to be redrawn.
	glutIdleFunc(managerIdle);
	glutMainLoop(); // Run the event loop!  This function does not return.
					// Program ends when user closes the window.
	return 0;

}
