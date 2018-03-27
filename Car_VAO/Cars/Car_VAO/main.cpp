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

#pragma comment(lib,"glew32.lib")

GLuint ID;

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

	float spionData[9][2];
	for (int i = 0; i < spion.neff; i++) {
		spionData[i][0] = spion.vertices[i].x;
		spionData[i][1] = spion.vertices[i].y;
		cout << spionData[i][0] << " , " << spionData[i][1] << endl;
	}
	
	glGenBuffers(1, &ID);
	glBindBuffer(GL_ARRAY_BUFFER, ID);
	glBufferData(GL_ARRAY_BUFFER, spion.neff, spionData, GL_STATIC_DRAW);
}

void reshape(int w, int h) {
	glViewport(0, 0, (GLsizei)w, (GLsizei)h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluOrtho2D(0.0f, (GLdouble)w, 0.0f, (GLdouble)h);
}

void display() {
	glClear(GL_COLOR_BUFFER_BIT);
	glColor3f(0.0f, 0.0f, 0.0f);
	glBindBuffer(GL_ARRAY_BUFFER, ID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_LINE_LOOP, 0, spion.neff);
	glFlush();
}

int main(int argc, char **argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutInitWindowSize(1366, 768);
	glutInitWindowPosition(0, 0);
	glutCreateWindow("GL Cars");
	init();
	glutDisplayFunc(display);
	glutReshapeFunc(reshape);
	glutMainLoop();
	return 0;
}