void statistic_box_draw() {
    // Display statistics box
  fill(255); // White color for the statistics box
  noStroke(); // No border for the box
  rect(20, height/2 - 150, 300, 300, 10); // Draw a rectangle with rounded corners (radius 10)

  // Set text properties
  fill(0); // Black color for the text
  
  textSize(24);
  textAlign(LEFT, TOP);

  // Display statistics inside the box
  text("Statistics:", 30, height/2 - 150 + 20); // Title for the statistics box
  text("Arm X Rotation: " + x, 30, height/2 - 150 + 60);
  text("Arm Y Rotation: " + y, 30, height/2 - 150 + 100);
  text("Arm Z Rotation: " + z, 30, height/2 - 150 + 140);
  text("Holding: " + (isHolding ? "Yes" : "No"), 30, height/2 - 150 + 180);
}

void drawGrid(float centerX, float centerY, float centerZ, int size, int step) {
  stroke(150); // Set grid line color
  strokeWeight(1); // Thin lines for the grid
  pushMatrix();
  translate(centerX, centerY, centerZ);
  for (int i = -size; i <= size; i += step) {
    line(i, -size, 0, i, size, 0); // Lines parallel to Y-axis (since we're looking down on the grid)
    line(-size, i, 0, size, i, 0); // Lines parallel to X-axis
  }
  popMatrix();
}
