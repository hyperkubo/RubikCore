namespace Core
{
    public static class Extensions
    {
        public static EdgeOrientations Flipped(this EdgeOrientations orientation)
        {
            return orientation == EdgeOrientations.Ok ? EdgeOrientations.Incorrect : EdgeOrientations.Ok;
        }

        public static VertexOrientations TurnClockwise(this VertexOrientations orientation)
        {
            return orientation switch
            {
                VertexOrientations.Ok => VertexOrientations.Clockwise,
                VertexOrientations.Clockwise => VertexOrientations.Counterclockwise,
                VertexOrientations.Counterclockwise => VertexOrientations.Ok,
                _ => throw new NotImplementedException()
            };
        }

        public static VertexOrientations TurnCounterclockwise(this VertexOrientations orientation)
        {
            return orientation switch
            {
                VertexOrientations.Ok => VertexOrientations.Counterclockwise,
                VertexOrientations.Counterclockwise => VertexOrientations.Clockwise,
                VertexOrientations.Clockwise => VertexOrientations.Ok,
                _ => throw new NotImplementedException()
            };
        }

        public static EdgePositions NextPosAfterTurn(this EdgePositions position, Faces face, TurnType turnType)
        {
            var edgesInFace = Rubik.FaceEdgesNames(face);
            if (!edgesInFace.Contains(position)) return position;

            var idxPositionInFace = edgesInFace.IndexOf(position);
            int clockWiseTurns = 4 - (int)turnType;
            var nextIdxInFace = (idxPositionInFace + clockWiseTurns) % 4;
            return edgesInFace[nextIdxInFace];
        }

        public static VertexPositions NextPosAfterTurn(this VertexPositions position, Faces face, TurnType turnType)
        {
            var vertexesInFace = Rubik.FaceVertexesNames(face);
            if (!vertexesInFace.Contains(position)) return position;

            var idxPositionInFace = vertexesInFace.IndexOf(position);
            int clockWiseTurns = 4 - (int)turnType;
            var nextIdxInFace = (idxPositionInFace + clockWiseTurns) % 4;
            return vertexesInFace[nextIdxInFace];
        }

        public static string ToReadable(this List<(Faces Face, TurnType TurnType)> values)
        {
            var result = string.Empty;
            foreach (var mov in values)
            {
                var turnType = mov.TurnType switch
                {
                    TurnType.Half => "2",
                    TurnType.Counterclockwise => "'",
                    _ => string.Empty,
                };
                result += $"{mov.Face.ToString().First()}{turnType} ";
            }
            return result;
        }
    }
}
