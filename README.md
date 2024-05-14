# RubikCore
Abstract rubik layer

# What is this?

This class library allows to locate the position and orientation of the twenty movable pieces of a 3x3x3 rubik cube while you turn the faces.
Uses a subset of the [Singmaster notation](https://en.wikipedia.org/wiki/Rubik%27s_Cube#Singmaster_notation) to represent the movements.
The representation of the position and orientation of the pieces is given by the pieces loops. It shows two different series of arrays where the first is for the edges and the other is for the vertexes.
If this two lists are empty, the cube is solved.

## How to use?

Just create an instance of Rubik class and just call one of the six possible turns (Up, Down, Left, Right, Back and Front) with a 1 of 3 types of turns (Clockwise, Counterclockwise or Half turn).

Maybe you can integrate in a bigger project, IDK ¯\\_(ツ)\_/¯
