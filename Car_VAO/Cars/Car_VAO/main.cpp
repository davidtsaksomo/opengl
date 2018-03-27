#include <windows.h>
#include <iostream>
#include <algorithm>
#include <fstream>
#include <GL/glew.h>
#include <GL/freeglut.h>

using namespace std;

#define width 1300
#define height 700
#define LANGIT_SPEED 4000
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

static GLfloat ANGLE = 0;
static GLfloat XTRANSLATED_JALAN = 0.5 * width / 2;
static GLfloat XTRANSLATED_GEDUNG = 2.3 * width / 2;
static GLfloat RED_LANGIT = 135.0;
static GLfloat GREEN_LANGIT = 206.0;
static GLfloat BLUE_LANGIT = 250.0;
static GLboolean kePagi = false;

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

	for (int i = 0; i < gedung.neff; i++) {
		gedungData[i][0] = gedung.vertices[i].x;
		gedungData[i][1] = height - gedung.vertices[i].y;
	}
	for (int i = 0; i < jalan.neff; i++) {
		jalanData[i][0] = jalan.vertices[i].x;
		jalanData[i][1] = height - jalan.vertices[i].y;
	}
	for (int i = 0; i < garis1.neff; i++) {
		garis1Data[i][0] = garis1.vertices[i].x;
		garis1Data[i][1] = height - garis1.vertices[i].y;
	}
	for (int i = 0; i < garis2.neff; i++) {
		garis2Data[i][0] = garis2.vertices[i].x;
		garis2Data[i][1] = height - garis2.vertices[i].y;
	}
	for (int i = 0; i < sasismobil.neff; i++) {
		sasismobilData[i][0] = sasismobil.vertices[i].x;
		sasismobilData[i][1] = height - sasismobil.vertices[i].y;
	}
	for (int i = 0; i < lampubelakang.neff; i++) {
		lampubelakangData[i][0] = lampubelakang.vertices[i].x;
		lampubelakangData[i][1] = height - lampubelakang.vertices[i].y;
	}
	for (int i = 0; i < kacabelakang.neff; i++) {
		kacabelakangData[i][0] = kacabelakang.vertices[i].x;
		kacabelakangData[i][1] = height - kacabelakang.vertices[i].y;
	}
	for (int i = 0; i < kacadepan.neff; i++) {
		kacadepanData[i][0] = kacadepan.vertices[i].x;
		kacadepanData[i][1] = height - kacadepan.vertices[i].y;
	}
	for (int i = 0; i < lampudepan1.neff; i++) {
		lampudepan1Data[i][0] = lampudepan1.vertices[i].x;
		lampudepan1Data[i][1] = height - lampudepan1.vertices[i].y;
	}
	for (int i = 0; i < lampudepan2.neff; i++) {
		lampudepan2Data[i][0] = lampudepan2.vertices[i].x;
		lampudepan2Data[i][1] = height - lampudepan2.vertices[i].y;
	}
	for (int i = 0; i < rodadepan.neff; i++) {
		rodadepanData[i][0] = rodadepan.vertices[i].x;
		rodadepanData[i][1] = height - rodadepan.vertices[i].y;
	}
	for (int i = 0; i < rodabelakang.neff; i++) {
		rodabelakangData[i][0] = rodabelakang.vertices[i].x;
		rodabelakangData[i][1] = height - rodabelakang.vertices[i].y;
	}
	for (int i = 0; i < rodakecildepan.neff; i++) {
		rodakecildepanData[i][0] = rodakecildepan.vertices[i].x;
		rodakecildepanData[i][1] = height - rodakecildepan.vertices[i].y;
	}
	for (int i = 0; i < rodakecilbelakang.neff; i++) {
		rodakecilbelakangData[i][0] = rodakecilbelakang.vertices[i].x;
		rodakecilbelakangData[i][1] = height - rodakecilbelakang.vertices[i].y;
	}
	for (int i = 0; i < velgdepan.neff; i++) {
		velgdepanData[i][0] = velgdepan.vertices[i].x;
		velgdepanData[i][1] = height - velgdepan.vertices[i].y;
	}
	for (int i = 0; i < velgbelakang.neff; i++) {
		velgbelakangData[i][0] = velgbelakang.vertices[i].x;
		velgbelakangData[i][1] = height - velgbelakang.vertices[i].y;
	}
	for (int i = 0; i < spion.neff; i++) {
		spionData[i][0] = spion.vertices[i].x;
		spionData[i][1] = height - spion.vertices[i].y;
	}
	for (int i = 0; i < garismobil.neff; i++) {
		garismobilData[i][0] = garismobil.vertices[i].x;
		garismobilData[i][1] = height - garismobil.vertices[i].y;
	}

	glGenBuffers(1, &gedungID);
	glBindBuffer(GL_ARRAY_BUFFER, gedungID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(gedungData), gedungData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &jalanID);
	glBindBuffer(GL_ARRAY_BUFFER, jalanID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(jalanData), jalanData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &garis1ID);
	glBindBuffer(GL_ARRAY_BUFFER, garis1ID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(garis1Data), garis1Data, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &garis2ID);
	glBindBuffer(GL_ARRAY_BUFFER, garis2ID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(garis2Data), garis2Data, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &sasismobilID);
	glBindBuffer(GL_ARRAY_BUFFER, sasismobilID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(sasismobilData), sasismobilData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &lampubelakangID);
	glBindBuffer(GL_ARRAY_BUFFER, lampubelakangID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(lampubelakangData), lampubelakangData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &kacabelakangID);
	glBindBuffer(GL_ARRAY_BUFFER, kacabelakangID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(kacabelakangData), kacabelakangData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &kacadepanID);
	glBindBuffer(GL_ARRAY_BUFFER, kacadepanID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(kacadepanData), kacadepanData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &lampudepan1ID);
	glBindBuffer(GL_ARRAY_BUFFER, lampudepan1ID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(lampudepan1Data), lampudepan1Data, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &lampudepan2ID);
	glBindBuffer(GL_ARRAY_BUFFER, lampudepan2ID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(lampudepan2Data), lampudepan2Data, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &rodadepanID);
	glBindBuffer(GL_ARRAY_BUFFER, rodadepanID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(rodadepanData), rodadepanData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &rodabelakangID);
	glBindBuffer(GL_ARRAY_BUFFER, rodabelakangID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(rodabelakangData), rodabelakangData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &rodakecildepanID);
	glBindBuffer(GL_ARRAY_BUFFER, rodakecildepanID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(rodakecildepanData), rodakecildepanData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &rodakecilbelakangID);
	glBindBuffer(GL_ARRAY_BUFFER, rodakecilbelakangID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(rodakecilbelakangData), rodakecilbelakangData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &velgdepanID);
	glBindBuffer(GL_ARRAY_BUFFER, velgdepanID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(velgdepanData), velgdepanData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &velgbelakangID);
	glBindBuffer(GL_ARRAY_BUFFER, velgbelakangID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(velgbelakangData), velgbelakangData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &spionID);
	glBindBuffer(GL_ARRAY_BUFFER, spionID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(spionData) , spionData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);

	glGenBuffers(1, &garismobilID);
	glBindBuffer(GL_ARRAY_BUFFER, garismobilID);
	glBufferData(GL_ARRAY_BUFFER, sizeof(garismobilData), garismobilData, GL_STATIC_DRAW);
	glBindBuffer(GL_ARRAY_BUFFER, 0);
}

void reshape(int w, int h) {
	glViewport(0, 0, (GLsizei)w, (GLsizei)h);
	glMatrixMode(GL_PROJECTION);
	glLoadIdentity();
	gluOrtho2D(0.0f, (GLdouble)w, 0.0f, (GLdouble)h);
}



void display() {

	//langit
	glClearColor(RED_LANGIT / 255.0, GREEN_LANGIT / 255.0, BLUE_LANGIT / 255.0, 1);  // sky color
	glClear(GL_COLOR_BUFFER_BIT | GL_STENCIL_BUFFER_BIT);
	
	//gedung
	glBindBuffer(GL_ARRAY_BUFFER, gedungID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);

	glPushMatrix();
	glTranslatef(XTRANSLATED_GEDUNG, 0, 0);
	glEnable(GL_STENCIL_TEST);
	glColorMask(GL_FALSE, GL_FALSE, GL_FALSE, GL_FALSE);
	glStencilFunc(GL_ALWAYS, 0, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_INVERT);
	glStencilMask(1);
	glDrawArrays(GL_POLYGON, 0, gedung.neff);
	glColorMask(GL_TRUE, GL_TRUE, GL_TRUE, GL_TRUE);
	glStencilFunc(GL_EQUAL, 1, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_KEEP);
	glColor3f(11.0 / 255.0, 61.0 / 255.0, 170.0 / 255.0);
	glDrawArrays(GL_POLYGON, 0, gedung.neff);
	glDisable(GL_STENCIL_TEST);
	glPopMatrix();

	//jalan
	glColor3f(113.0 / 255.0, 113.0 / 255.0, 115.0 / 255.0);
	glBindBuffer(GL_ARRAY_BUFFER, jalanID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, jalan.neff);

	glColor3f(1, 1, 1);//putih
	
    //garis_1
	glBindBuffer(GL_ARRAY_BUFFER, garis1ID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);

	glPushMatrix();
	glTranslatef(XTRANSLATED_JALAN, 0, 0);
	glDrawArrays(GL_POLYGON, 0, garis1.neff);
	glPopMatrix();

	//garis_2
	glBindBuffer(GL_ARRAY_BUFFER, garis2ID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	
	glPushMatrix();
	glTranslatef(XTRANSLATED_JALAN, 0, 0);
	glDrawArrays(GL_POLYGON, 0, garis2.neff);
	glPopMatrix();

	//mobil
	glColor3f(237.0 / 255.0, 2.0 / 255.0, 2.0 / 255.0);

	glBindBuffer(GL_ARRAY_BUFFER, sasismobilID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	
	glEnable(GL_STENCIL_TEST);
	glColorMask(GL_FALSE, GL_FALSE, GL_FALSE, GL_FALSE);
	glStencilFunc(GL_ALWAYS, 0, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_INVERT);
	glStencilMask(1);
	glDrawArrays(GL_POLYGON, 0, sasismobil.neff);
	glColorMask(GL_TRUE, GL_TRUE, GL_TRUE, GL_TRUE);
	glStencilFunc(GL_EQUAL, 1, 1);
	glStencilOp(GL_KEEP, GL_KEEP, GL_KEEP);
	glColor3f(237.0 / 255.0, 2.0 / 255.0, 2.0 / 255.0);
	glDrawArrays(GL_POLYGON, 0, sasismobil.neff);
	glDisable(GL_STENCIL_TEST);

	//lampu_belakang
	glColor3f(255.0 / 255.0, 145.0 / 255.0, 12.0 / 255.0);
	glBindBuffer(GL_ARRAY_BUFFER, lampubelakangID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, lampubelakang.neff);

	glColor3f(20.0 / 255.0, 2.0 / 255.0, 20.0 / 255.0);
	//kaca_belakang
	glBindBuffer(GL_ARRAY_BUFFER, kacabelakangID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, kacabelakang.neff);
	
	//kaca_depan
	glBindBuffer(GL_ARRAY_BUFFER, kacadepanID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, kacadepan.neff);

	glColor3f(199.0 / 255.0, 202.0 / 255.0, 207.0 / 255.0);
	//lampu_depan_1
	glBindBuffer(GL_ARRAY_BUFFER, lampudepan1ID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, lampudepan1.neff);
	
	//lampu_depan_2
	glColor3f(0, 0, 0);
	glBindBuffer(GL_ARRAY_BUFFER, lampudepan2ID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, lampudepan2.neff);

	//roda_belakang
	glBindBuffer(GL_ARRAY_BUFFER, rodabelakangID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, rodabelakang.neff);

	//roda_depan
	glBindBuffer(GL_ARRAY_BUFFER, rodadepanID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, rodadepan.neff);

	glColor3f(0.9, 0.9, 0.9);
	//rodakecilbelakang
	glBindBuffer(GL_ARRAY_BUFFER, rodakecilbelakangID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, rodakecilbelakang.neff);

	//rodakecildepan
	glBindBuffer(GL_ARRAY_BUFFER, rodakecildepanID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, rodakecildepan.neff);

	glColor3f(0.3, 0.3, 0.3);
	//velgbelakang
	glBindBuffer(GL_ARRAY_BUFFER, velgbelakangID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);

	glMatrixMode(GL_MODELVIEW);
	glPushMatrix();
	glTranslatef(width/2 - (width/2*0.29), height/2 - (height/2*0.34), 0);
	glRotatef(ANGLE, 0.0, 0.0, 1.0);
	glTranslatef(- (width / 2 - (width / 2 * 0.29)), - (height / 2 - (height / 2 * 0.34)), 0);
	glDrawArrays(GL_POLYGON, 0, velgbelakang.neff);
	glPopMatrix();
	
	//velgdepan
	glBindBuffer(GL_ARRAY_BUFFER, velgdepanID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glPushMatrix();
	glTranslatef(width / 2 + (width / 2 * 0.2935), height / 2 - (height / 2 * 0.34), 0);
	glRotatef(ANGLE, 0.0, 0.0, 1.0);
	glTranslatef(-(width / 2 + (width / 2 * 0.2935)), -(height / 2 - (height / 2 * 0.34)), 0);
	glDrawArrays(GL_POLYGON, 0, velgdepan.neff);
	glPopMatrix();

	//spion
	glColor3f(237.0 / 255.0, 2.0 / 255.0, 2.0 / 255.0);
	glBindBuffer(GL_ARRAY_BUFFER, spionID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_POLYGON, 0, spion.neff);

	//garis_mobil
	glColor3f(99.0 / 255.0, 0, 0);
	glBindBuffer(GL_ARRAY_BUFFER, garismobilID);
	glVertexPointer(2, GL_FLOAT, 2 * sizeof(float), 0);
	glDrawArrays(GL_LINE_LOOP, 0, garismobil.neff);

	glFlush();
}

void managerIdle(void)
{
	Sleep(5.0);
	ANGLE -= 10;

	if (XTRANSLATED_JALAN < -0.5 * width/2) {
		XTRANSLATED_JALAN = 0.5 * width/2;
	}
	else {
		XTRANSLATED_JALAN -= 0.025 * width/2;
	}

	if (XTRANSLATED_GEDUNG < -2.3 * width / 2) {
		XTRANSLATED_GEDUNG = 2.3 * width / 2;
	}
	else {
		XTRANSLATED_GEDUNG -= 0.0005 * width / 2;
	}

	if (kePagi) {
		if (RED_LANGIT >= 135.0 && GREEN_LANGIT >= 206.0 && BLUE_LANGIT >= 250.0) {
			kePagi = false;
		}
		else {
			RED_LANGIT += 135.0 / LANGIT_SPEED;
			GREEN_LANGIT += 206.0 / LANGIT_SPEED;
			BLUE_LANGIT += 250.0 / LANGIT_SPEED;
		}
	}
	else {
		if (RED_LANGIT <= 0 && GREEN_LANGIT <= 0 && BLUE_LANGIT <= 0) {
			kePagi = true;
		}
		else {
			RED_LANGIT -= 135.0 / LANGIT_SPEED;
			GREEN_LANGIT -= 206.0 / LANGIT_SPEED;
			BLUE_LANGIT -= 250.0 / LANGIT_SPEED;
		}
	}

	glutPostRedisplay();
}

int main(int argc, char **argv) {
	glutInit(&argc, argv);
	glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB);
	glutInitWindowSize(width, height);
	glutInitWindowPosition(0, 0);
	glutCreateWindow("GL Cars");
	init();
	glutDisplayFunc(display);
	glutIdleFunc(managerIdle);
	glutReshapeFunc(reshape);
	glutMainLoop();
	return 0;
}