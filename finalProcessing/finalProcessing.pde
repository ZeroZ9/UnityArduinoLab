import processing.serial.*;

Serial myPort;
PShape myModel;
PShape cube;
PImage txtr;

float x = 0, y = 0, z = 0;
 
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
}

void draw() {
  background(200);
  lights();
  
   // Display Text
  text("My project", 20, 50); // Position text at (20, 50)
  
  pushMatrix();
  translate(width/2,height/2); // Adjust object position
  rotateX(radians(x));
  rotateY(radians(y));
  rotateZ(radians(z));
  scale(70);
  shape(myModel);
  popMatrix();
  
  translate(width/2 - 300,height/2); // Adjust object position
  scale(5);
  translate(100,0);
  shape(cube);
}

String val;

void serialEvent(Serial myPort){
  val = myPort.readStringUntil('\n');
  if (val != null){
    val = trim(val);
    println(val);
    String[] tokens = val.split(",");
    if(tokens.length == 4){
        x = float(tokens[1]);
        y = float(tokens[2]);
        z = float(tokens[3]);
    }
  }
}
