namespace Core
{
    public abstract class Piece<TPosition, TOrientation, TValues>(TPosition destination, TOrientation orientation)
        where TPosition    : Enum
        where TOrientation : Enum
    {
        public TOrientation Orientation { get; protected set; } = orientation;
        public TPosition Destination { get; } = destination;
        public abstract TValues Values { get; }
    }

    public class Vertex(VertexPositions destination, VertexOrientations orientation = VertexOrientations.Ok)
        : Piece<VertexPositions, VertexOrientations, VertexValues>(destination, orientation)
    {
        public override VertexValues Values => new() {
            Orientation = Orientation,
            Destination = Destination
        };

        public void Turn(VertexOrientations orientation = VertexOrientations.Clockwise)
        {
            var orientationNum = (int)Orientation;
            var turnNum = (int)orientation;
            var newOrientation = (orientationNum + turnNum) % 3;
            Orientation = (VertexOrientations)newOrientation;
        }
    }

    public class Edge(EdgePositions destination, EdgeOrientations orientation = EdgeOrientations.Ok)
        : Piece<EdgePositions, EdgeOrientations, EdgeValues>(destination, orientation)
    {
        public override EdgeValues Values => new() { Orientation = Orientation, Destination = Destination };

        public void Flip()
        {
            Orientation = (EdgeOrientations)(((int)Orientation + 1) % 2);
        }
    }

    public class PieceLoops<TPiece, TOrientations, TPositions>()
        where TPiece : IPieceValues<TOrientations, TPositions>
        where TOrientations : Enum
        where TPositions : Enum
    {
        private List<List<TPiece>> Loops { get; set; }

        public void Add(List<TPiece> loop)
        {
            Loops ??= [];
            Loops.Add(loop);
        }

        public override string ToString()
        {
            var result = "{ ";
            foreach (var loop in Loops.Where(c => c.Count > 1 || c.Any(a => (int)(object)a.Orientation != 0)))
            {
                result += ("[");
                result += string.Join(' ', loop.Select(c => $"{c.Destination}-{(int)(object)c.Orientation}"));
                result += ("]");
            }
            result += (" }");

            return result;
        }
    }

    public interface IPieceValues<TOrientations, TPositions>
        where TOrientations : Enum
        where TPositions : Enum
    {
        public TOrientations Orientation { get; set; }
        public TPositions Destination { get; set; }
    }

    public struct EdgeValues : IPieceValues<EdgeOrientations, EdgePositions>
    {
        public EdgeOrientations Orientation { get; set; }
        public EdgePositions Destination { get; set; }

        public readonly EdgeOrientations Flipped()
        {

            return (EdgeOrientations)(((int)Orientation + 1) % 2);
        }
    }

    public struct VertexValues : IPieceValues<VertexOrientations, VertexPositions>
    {
        public VertexOrientations Orientation { get; set; }
        public VertexPositions Destination { get; set; }
        public readonly VertexOrientations Turned(VertexOrientations orientation)
        {
            var orientationNum = (int)Orientation;
            var turnNum = (int)orientation;
            var newOrientation = (orientationNum + turnNum) % 3;
            return (VertexOrientations)newOrientation;
        }
    }

    public enum VertexPositions
    {
        UFL,
        UFR,
        UBL,
        UBR,
        DFL,
        DFR,
        DBL,
        DBR
    }
    public enum VertexOrientations
    {
        Ok,
        Clockwise,
        Counterclockwise
    }
    public enum EdgeOrientations
    {
        Ok,
        Incorrect
    }
    public enum EdgePositions
    {
        UF,
        UL,
        UR,
        UB,
        FL,
        FR,
        BL,
        BR,
        DF,
        DL,
        DR,
        DB
    }
    public enum TurnType
    {
        Counterclockwise = 1,
        Half,
        Clockwise,
    }
    public enum Faces
    {
        Up,
        Down,
        Left,
        Right,
        Front,
        Back
    }
}
