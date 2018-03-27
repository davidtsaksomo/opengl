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
#include <windows.h>
#include <iostream>
#include <algorithm>
#include <fstream>
#include <GL/glew.h>
#include <GL/freeglut.h>
using namespace std;
#define width 1300
#define height 700
#pragma comment(lib,"glew32.lib")


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
		vertices.vertices[i].x = _x;
		//vertices.vertices[i].x = (_x / width) * 2 - 1;
		//vertices.vertices[i].y = ((_y / height) * 2 - 1)*-1;
		vertices.vertices[i].y = _y;
		i++;
	}
	vertices.neff = i;
	fclose(externalFile);
	return vertices;
}

Vertices garis1;
Vertices garis2;
Vertices garismobil;
Vertices gedung;
Vertices jalan;
Vertices kacabelakang;
Vertices kacadepan;
Vertices lampubelakang;
Vertices lampudepan1;
Vertices lampudepan2;
Vertices sasismobil;
Vertices spion;
Vertices rodadepan;
Vertices rodakecildepan;
Vertices velgdepan;
Vertices rodabelakang;
Vertices rodakecilbelakang;
Vertices velgbelakang;

GLuint garis1ID;
GLuint garis2ID;
GLuint garismobilID;
GLuint gedungID;
GLuint jalanID;
GLuint kacabelakangID;
GLuint kacadepanID;
GLuint lampubelakangID;
GLuint lampudepan1ID;
GLuint lampudepan2ID;
GLuint sasismobilID;
GLuint spionID;
GLuint rodadepanID;
GLuint rodakecildepanID;
GLuint velgdepanID;
GLuint rodabelakangID;
GLuint rodakecilbelakangID;
GLuint velgbelakangID;

void init() {

	//read from file
	garis1 = readVertexFromFIle("resources/garis1.txt");
	garis2 = readVertexFromFIle("resources/garis2.txt");
	garismobil = readVertexFromFIle("resources/garismobil.txt");
	gedung = readVertexFromFIle("resources/gedung.txt");
	jalan = readVertexFromFIle("resources/jalan.txt");
	kacabelakang = readVertexFromFIle("resources/kacabelakang.txt");
	kacadepan = readVertexFromFIle("resources/kacadepan.txt");
	lampubelakang = readVertexFromFIle("resources/lampubelakang.txt");
	lampudepan1 = readVertexFromFIle("resources/lampudepan1.txt");
	lampudepan2 = readVertexFromFIle("resources/lampudepan2.txt");
	sasismobil = readVertexFromFIle("resources/sasismobil.txt");
	spion = readVertexFromFIle("resources/spion.txt");
	rodadepan = readVertexFromFIle("resources/rodadepan.txt");
	rodakecildepan = readVertexFromFIle("resources/rodakecildepan.txt");
	velgdepan = readVertexFromFIle("resources/velgdepan.txt");
	rodabelakang = readVertexFromFIle("resources/rodabelakang.txt");
	rodakecilbelakang = readVertexFromFIle("resources/rodakecilbelakang.txt");
	velgbelakang = readVertexFromFIle("resources/velgbelakang.txt");

	glewInit();
	glClearColor(1.0f, 1.0f, 1.0f, 0.0f);
	glShadeModel(GL_FLAT);
	glEnableClientState(GL_VERTEX_ARRAY);

	
	
	float garis1Data[50][2];
	float garis2Data[50][2];
	float garismobilData[50][2];
	float gedungData[50][2];
	float jalanData[50][2];
	float kacabelakangData[50][2];
	float kacadepanData[50][2];
	float lampubelakangData[50][2];
	float lampudepan1Data[50][2];
	float lampudepan2Data[50][2];
	float sasismobilData[50][2];
	float spionData[50][2];
	float rodadepanData[50][2];
	float rodakecildepanData[50][2];
	float velgdepanData[50][2];
	float rodabelakangData[50][2];
	float rodakecilbelakangData[50][2];
	float velgbelakangData[50][2];

	for (int i = 0; i < spion.neff; i++) {
		spionData[i][0] = spion.vertices[i].x;
		spionData[i][1] = height - spion.vertices[i].y;
	}

	glGenBuffers(1, &spionID);
	glBindBuffer(GL_ARRAY_BUFFER, spionID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(spionData) , spionData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
}

void reshape(int w, int h) {
	glViewport(0, 0, (GLsizei)w, (GLsizei)h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluOrtho2D(0.0f, (GLdouble)w, 0.0f, (GLdouble)h);
}

static GLfloat ANGLE = 0;
static GLfloat XTRANSLATED_JALAN = 0.5;
static GLfloat XTRANSLATED_GEDUNG = 2.3;
static GLfloat RED_LANGIT = 135.0;
static GLfloat GREEN_LANGIT = 206.0;
static GLfloat BLUE_LANGIT = 250.0;
static GLboolean kePagi = false;

void display() {
	glClearColor(RED_LANGIT / 255.0, GREEN_LANGIT / 255.0, BLUE_LANGIT / 255.0, 1);  // sky color
	glClear(GL_COLOR_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
	
	glColor3f(237.0 / 255.0, 2.0 / 255.0, 2.0 / 255.0);
	glBindBuffer(GL_ARRAY_BUFFER, spionID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, spion.neff);

	//glColor3f(113.0 / 255.0, 113.0 / 255.0, 115.0 / 255.0);
	//glBindBuffer(GL_ARRAY_BUFFER, jalanID);
	//glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	//glDrawArrays(GL_POLYGON, 0, jalan.neff);


	glFlush();
}

int main(int argc, char **argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutInitWindowSize(width, height);
	glutInitWindowPosition(0, 0);
	glutCreateWindow("GL Cars");
	init();
	glutDisplayFunc(display);
	glutReshapeFunc(reshape);
	glutMainLoop();
	return 0;
}