#include <Arduino.h>

// Simulated gyro values
float gyroX = 0.0;
float gyroY = 0.0;
float gyroZ = 0.0;

// Time tracking
unsigned long previousMillis = 0;
const long interval = 100;  // Interval at which to send data (milliseconds)

void setup() {
  // Start serial communication
  Serial.begin(115200);
}

void loop() {
  unsigned long currentMillis = millis();

  if (currentMillis - previousMillis >= interval) {
    previousMillis = currentMillis;

    // Simulate gyro data changes
    gyroX += 0.1;
    gyroY += 0.2;
    gyroZ += 0.3;

    // Send data to the serial port
    //Serial.print("X:");
    Serial.print(gyroX);
    Serial.print(",");
    Serial.print(gyroY);
    Serial.print(",");
    Serial.println(gyroZ);
  }
}
