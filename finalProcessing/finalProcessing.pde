import processing.serial.*;

Serial myPort;
PShape myModel;
PShape cube;
PImage txtr;
boolean isHolding = false;
float x = 0, y = 0, z = 0;
PVector cubeRelPos;

float armLength = 200; 
float PivotX, PivotY, PivotZ;
float cubeX,cubeY,cubeZ;

boolean useMouseControl = false;
// Toggle Button Dimensions and State
int toggleButtonX = 20;
int toggleButtonY = 20;
int toggleButtonWidth = 60;
int toggleButtonHeight = 30;
boolean toggleState = false; // false for Arduino, true for Mouse
 
void setup() {
  fullScreen(P3D); // Set the size of the window and use the P3D renderer
  
  myModel = loadShape("arm.obj"); // Load the model
  cube = loadShape("cube.obj");
  txtr = loadImage("2.png");
  //myModel.setTexture(txtr);  
  
  String portName = Serial.list()[2];
  myPort = new Serial(this,portName,115200);
  
  // Text Style
  textSize(32);
  fill(0); // Black color for text
  
  cubeRelPos = new PVector(700,200,-200);
  PivotX = width/2;
  PivotY = height/2;
  PivotZ = 0;
  
  cubeX = width/2 + 300;
  cubeY = height/2;
  cubeZ = 0;
  
}

void draw() {
  background(240);
  lights();
  
  statistic_box_draw();
  
  textSize(32);
  fill(0); // Black color for text
   // Display Text
  text("Robotic Stimulation", width/2 - 120, 100); // Position text at (20, 50)
  
  // DRAW 

  
  pushMatrix();
  translate(width/2,height/2); // Adjust object position
  rotateX(radians(x));
  rotateY(radians(y));
  rotateZ(radians(z));
  scale(70);
  shape(myModel);
  popMatrix();
  
  if (isHolding) {
    pushMatrix(); // Save the current transformation state

    PVector armEnd = new PVector(0, 0, -armLength); // Position relative to pivot, along -Z axis
    armEnd = rotateVector(armEnd, radians(x), 'X'); // Rotate around X-axis
    armEnd = rotateVector(armEnd, radians(y), 'Y'); // Rotate around Y-axis
    armEnd = rotateVector(armEnd, radians(z), 'Z'); // Rotate around Z-axis


    cubeX = width/2 + armEnd.x;
    cubeY = height/2 + armEnd.y;
    cubeZ = armEnd.z;
    translate(cubeX, cubeY, cubeZ); 

    scale(5);
    shape(cube); // Draw the cube model
    popMatrix(); // Restore the transformation state
  }

  // If not holding, draw the cube separately
  if (!isHolding) {
    pushMatrix();
    translate(cubeX, cubeY, cubeZ); // Position the cube next to the arm
    scale(5);
    shape(cube); // Draw the cube model
    popMatrix();
  }
}

String val;

void serialEvent(Serial myPort){
  val = myPort.readStringUntil('\n');
  if (val != null){
    val = trim(val);
    //println(val);
    String[] tokens = val.split(",");
    if(tokens.length == 4){
        if (tokens[0].equals("hold")){
          isHolding = true;
        }else {
          isHolding = false;
        }
        x = float(tokens[1]);
        y = float(tokens[2]);
        z = float(tokens[3]);
        
    }
  }
}


PVector rotateVector(PVector vec, float angle, char axis) {
  float sinTheta = sin(angle);
  float cosTheta = cos(angle);
  PVector rotated = new PVector();

  switch(axis) {
    case 'X':
      rotated.x = vec.x;
      rotated.y = cosTheta * vec.y - sinTheta * vec.z;
      rotated.z = sinTheta * vec.y + cosTheta * vec.z;
      break;
    case 'Y':
      rotated.x = cosTheta * vec.x + sinTheta * vec.z;
      rotated.y = vec.y;
      rotated.z = -sinTheta * vec.x + cosTheta * vec.z;
      break;
    case 'Z':
      rotated.x = cosTheta * vec.x - sinTheta * vec.y;
      rotated.y = sinTheta * vec.x + cosTheta * vec.y;
      rotated.z = vec.z;
      break;
  }
  return rotated;
}
