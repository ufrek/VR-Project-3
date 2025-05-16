# Manatee Educational Interactive Experience
Link to short video demo: https://youtu.be/upuQn4r4mmw

This program uses Unity and C# in tandem with the Google Cardboard to provide a short presentation while on a boat ride about Manatees and their behaviors that users can play on their phone.

The following level is an on rails segment where the player is guided through an underwater scene and they can gaze at seaweed to consume it and increase their score. There are several meetings with other manatees and boats
along the journey.

##Optimizations:
There were several measures taken to increase the frame rate to smooth levels:
  1: The ocean wave lighting diffusion is only made up of a small number of frames to look nice but not provide completely realistic performance.
  2: All models hav elow polygon counts and the item variety sis kept low to decrease the number of resources being used.
  
