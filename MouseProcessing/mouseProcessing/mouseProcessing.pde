import java.awt.Robot;
import java.awt.AWTException;
import processing.serial.*;

Serial myPort;  // The serial port
Robot robot;

void setup() {
  size(400, 400);
  String portName = Serial.list()[2];
  myPort = new Serial(this, portName, 115200); // Adjust your baud rate as needed

  try {
    robot = new Robot();
  } catch (AWTException e) {
    println("Robot class not supported");
    exit();
  }

  // Read bytes into a buffer until you get a linefeed (ASCII 10):
  myPort.bufferUntil('\n');
}

void draw() {
  background(255);
  // Nothing to do here, the mouse movement is handled in the serialEvent
}

void serialEvent(Serial myPort) {
  // Get the ASCII string from the serial port
  String inString = myPort.readStringUntil('\n');

  if (inString != null) {
    // Trim the string to remove any whitespace
    inString = trim(inString);

    // Split the string on commas
    String[] sensorStrings = split(inString, ',');

    // Convert the strings to floats
    if (sensorStrings.length >= 3) {
      float sensorX = float(sensorStrings[1]);
      float sensorY = float(sensorStrings[2]);
      // Optionally use a third value for another purpose
      // float sensorZ = float(sensorStrings[2]);

      // Map the sensor values to the screen size
      int screenX = (int) map(sensorX, -90, 90, 0, displayWidth);
      int screenY = (int) map(sensorY, -90, 90, 0, displayHeight);
      
      if (sensorStrings[0].equals("release")){
          robot.mousePress(java.awt.event.InputEvent.BUTTON1_MASK);
          robot.mouseRelease(java.awt.event.InputEvent.BUTTON1_MASK);
       }
      // Move the system cursor
      robot.mouseMove(screenX, screenY);
    }
  }
}

// Remember to disconnect the serial port when finished
void stop() {
  myPort.stop();
  super.stop();
}
