#include <Arduino.h>

// Simulated gyro values
float gyroX = 0.0; // Initial gyro value for X-axis (horizontal rotation)
float gyroY = 0.0; // Initial gyro value for Y-axis (not used in this scenario)
float gyroZ = 0.0; // Initial gyro value for Z-axis (vertical rotation)

// Time tracking
unsigned long previousMillis = 0;
const long interval = 20;  // Interval at which to update gyro values (20 milliseconds for smooth rotation)
const long rotationDuration = 5000; // Duration to complete 90 degree rotation in milliseconds

bool isHolding = true; // The hand is always holding in this scenario

void setup() {
  // Start serial communication
  Serial.begin(115200);
}

void loop() {
  unsigned long currentMillis = millis();

  // Check if 5 seconds have passed to rotate horizontally
  if (currentMillis < 5000) {
    // Calculate the increment for each loop iteration to complete the rotation in 5 seconds

    // Update gyroX for horizontal rotation
    gyroX += 0.04;

    // Ensure that gyroX does not exceed 90 degrees
    if (gyroX > 90.0) {
      gyroX = 90.0;
    }
  } else {

    // Update gyroZ for vertical rotation
    gyroZ += 0.03;

    // Ensure that gyroZ does not exceed 90 degrees
    if (gyroZ > 90.0) {
      gyroZ = 90.0;
    }
  }


  Serial.print("hold,");
  // Send data to the serial port
  Serial.print(gyroX);
  Serial.print(",");
  Serial.print(gyroY);
  Serial.print(",");
  Serial.println(gyroZ);
}
