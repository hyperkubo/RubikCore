using System.Data;
using System.Text.RegularExpressions;

namespace Core;

public partial class Rubik
{
    private const char TURN_UP = 'U';
    private const char TURN_FRONT = 'F';
    private const char TURN_LEFT = 'L';
    private const char TURN_BACK = 'B';
    private const char TURN_DOWN = 'D';
    private const char TURN_RIGHT = 'R';
    private const char HALF_TURN_NOTATION = '2';
    private const char COUNTERCLOCKWISE_TURN_NOTATION = '\'';
    private const char COUNTERCLOCKWISE_NUMBER_TURN_NOTATION = '3';
    private const char CLOCKWISE_TURN_NOTATION = '1';
    private readonly Dictionary<EdgePositions, Edge> _edges = [];
    private readonly Dictionary<VertexPositions, Vertex> _vertexes = [];
    private readonly Stack<(Faces Face, TurnType TurnType)> _movsList;

    public List<(Faces Face, TurnType TurnType)> MovementsList { get
        {
            var movs = _movsList.Reverse();
            return movs.ToList();
        } }
    public IReadOnlyCollection<KeyValuePair<EdgePositions, EdgeValues>> Edges
    {
        get {
            return Array.AsReadOnly(_edges.Select(e =>
                new KeyValuePair<EdgePositions, EdgeValues>
                (
                e.Key, new EdgeValues
                    { Orientation = e.Value.Orientation, Destination = e.Value.Destination }
                )
                ).ToArray());
        }
    }
    public IReadOnlyCollection<KeyValuePair<EdgePositions, EdgeOrientations>> EdgesOrientations
    {
        get
        {
            return Array.AsReadOnly(_edges.Select(e => 
                new KeyValuePair<EdgePositions, EdgeOrientations>(e.Key, e.Value.Orientation)
            ).ToArray());
        }
    }
    public IReadOnlyCollection<KeyValuePair<VertexPositions, VertexValues>> Vertexes
    {
        get {
            return Array.AsReadOnly(_vertexes.Select(c =>
                new KeyValuePair<VertexPositions, VertexValues>
                (
                c.Key, new VertexValues
                    { Orientation = c.Value.Orientation, Destination = c.Value.Destination }
                )
                ).ToArray());
        }
    }
    public IReadOnlyCollection<KeyValuePair<VertexPositions, VertexOrientations>> VertexesOrientations
    {
        get
        {
            return Array.AsReadOnly(_vertexes.Select(c => 
                new KeyValuePair<VertexPositions, VertexOrientations>(c.Key, c.Value.Orientation)
            ).ToArray());
        }
    }

    public Rubik()
    {
        foreach (var edgePos in Enum.GetValues(typeof(EdgePositions)))
        {
            _edges.Add((EdgePositions)edgePos, new((EdgePositions)edgePos));
        }

        foreach (var vertexPos in Enum.GetValues(typeof(VertexPositions)))
        {
            _vertexes.Add((VertexPositions)vertexPos, new((VertexPositions)vertexPos));
        }

        _movsList = [];
    }

    #region Rubik Turns
    public Rubik Up(TurnType turnType = TurnType.Clockwise)
    {
        var uf = _edges[EdgePositions.UF];
        var ul = _edges[EdgePositions.UL];
        var ub = _edges[EdgePositions.UB];
        var ur = _edges[EdgePositions.UR];
        var ufl = _vertexes[VertexPositions.UFL];
        var ufr = _vertexes[VertexPositions.UFR];
        var ubl = _vertexes[VertexPositions.UBL];
        var ubr = _vertexes[VertexPositions.UBR];
        switch (turnType)
        {
            case TurnType.Clockwise:
                _edges[EdgePositions.UF] = ur;
                _edges[EdgePositions.UR] = ub;
                _edges[EdgePositions.UB] = ul;
                _edges[EdgePositions.UL] = uf;
                _vertexes[VertexPositions.UFL] = ufr;
                _vertexes[VertexPositions.UFR] = ubr;
                _vertexes[VertexPositions.UBL] = ufl;
                _vertexes[VertexPositions.UBR] = ubl;

                PushMovement(Faces.Up);
                break;

            case TurnType.Half:
                Up().Up();
                break;

            case TurnType.Counterclockwise:
                Up().Up().Up();
                break;
        }
        return this;
    }
    public Rubik Down(TurnType turnType = TurnType.Clockwise)
    {
        var df = _edges[EdgePositions.DF];
        var dl = _edges[EdgePositions.DL];
        var db = _edges[EdgePositions.DB];
        var dr = _edges[EdgePositions.DR];
        var dfl = _vertexes[VertexPositions.DFL];
        var dfr = _vertexes[VertexPositions.DFR];
        var dbl = _vertexes[VertexPositions.DBL];
        var dbr = _vertexes[VertexPositions.DBR];
        switch (turnType)
        {
            case TurnType.Clockwise:
                _edges[EdgePositions.DF] = dl;
                _edges[EdgePositions.DR] = df;
                _edges[EdgePositions.DB] = dr;
                _edges[EdgePositions.DL] = db;
                _vertexes[VertexPositions.DFL] = dbl;
                _vertexes[VertexPositions.DFR] = dfl;
                _vertexes[VertexPositions.DBL] = dbr;
                _vertexes[VertexPositions.DBR] = dfr;

                PushMovement(Faces.Down);
                break;

            case TurnType.Half:
                Down().Down();
                break;

            case TurnType.Counterclockwise:
                Down().Down().Down();
                break;
        }

        return this;
    }
    public Rubik Right(TurnType turnType = TurnType.Clockwise)
    {
        var fr = _edges[EdgePositions.FR];
        var ur = _edges[EdgePositions.UR];
        var br = _edges[EdgePositions.BR];
        var dr = _edges[EdgePositions.DR];
        var ufr = _vertexes[VertexPositions.UFR];
        var ubr = _vertexes[VertexPositions.UBR];
        var dbr = _vertexes[VertexPositions.DBR];
        var dfr = _vertexes[VertexPositions.DFR];
        switch (turnType)
        {
            case TurnType.Clockwise:
                ufr.Turn(VertexOrientations.Clockwise);
                ubr.Turn(VertexOrientations.Counterclockwise);
                dbr.Turn(VertexOrientations.Clockwise);
                dfr.Turn(VertexOrientations.Counterclockwise);
                _edges[EdgePositions.FR] = dr;
                _edges[EdgePositions.DR] = br;
                _edges[EdgePositions.BR] = ur;
                _edges[EdgePositions.UR] = fr;
                _vertexes[VertexPositions.UFR] = dfr;
                _vertexes[VertexPositions.DFR] = dbr;
                _vertexes[VertexPositions.DBR] = ubr;
                _vertexes[VertexPositions.UBR] = ufr;

                PushMovement(Faces.Right);
                break;

            case TurnType.Half:
                Right().Right();
                break;

            case TurnType.Counterclockwise:
                Right().Right().Right();
                break;
        }

        return this;
    }
    public Rubik Left(TurnType turnType = TurnType.Clockwise)
    {
        var fl = _edges[EdgePositions.FL];
        var ul = _edges[EdgePositions.UL];
        var bl = _edges[EdgePositions.BL];
        var dl = _edges[EdgePositions.DL];
        var ufl = _vertexes[VertexPositions.UFL];
        var ubl = _vertexes[VertexPositions.UBL];
        var dbl = _vertexes[VertexPositions.DBL];
        var dfl = _vertexes[VertexPositions.DFL];

        switch (turnType)
        {
            case TurnType.Clockwise:
                ufl.Turn(VertexOrientations.Counterclockwise);
                ubl.Turn(VertexOrientations.Clockwise);
                dbl.Turn(VertexOrientations.Counterclockwise);
                dfl.Turn(VertexOrientations.Clockwise);
                _edges[EdgePositions.FL] = ul;
                _edges[EdgePositions.UL] = bl;
                _edges[EdgePositions.BL] = dl;
                _edges[EdgePositions.DL] = fl;
                _vertexes[VertexPositions.UFL] = ubl;
                _vertexes[VertexPositions.UBL] = dbl;
                _vertexes[VertexPositions.DBL] = dfl;
                _vertexes[VertexPositions.DFL] = ufl;

                PushMovement(Faces.Left);
                break;

            case TurnType.Half:
                Left().Left();
                break;

            case TurnType.Counterclockwise:
                Left().Left().Left();
                break;
        }
        return this;
    }
    public Rubik Front(TurnType turnType = TurnType.Clockwise)
    {
        var uf = _edges[EdgePositions.UF];
        var fr = _edges[EdgePositions.FR];
        var fl = _edges[EdgePositions.FL];
        var df = _edges[EdgePositions.DF];
        var ufl = _vertexes[VertexPositions.UFL];
        var ufr = _vertexes[VertexPositions.UFR];
        var dfl = _vertexes[VertexPositions.DFL];
        var dfr = _vertexes[VertexPositions.DFR];

        switch(turnType)
        {
            case TurnType.Clockwise:
                _edges[EdgePositions.UF] = fl;
                _edges[EdgePositions.FL] = df;
                _edges[EdgePositions.DF] = fr;
                _edges[EdgePositions.FR] = uf;
                _vertexes[VertexPositions.UFL] = dfl;
                _vertexes[VertexPositions.DFL] = dfr;
                _vertexes[VertexPositions.DFR] = ufr;
                _vertexes[VertexPositions.UFR] = ufl;
                uf.Flip();
                fr.Flip();
                fl.Flip();
                df.Flip();
                ufl.Turn(VertexOrientations.Clockwise);
                ufr.Turn(VertexOrientations.Counterclockwise);
                dfr.Turn(VertexOrientations.Clockwise);
                dfl.Turn(VertexOrientations.Counterclockwise);

                PushMovement(Faces.Front);
                break;

            case TurnType.Counterclockwise:
                Front().Front().Front();
                break;

            case TurnType.Half:
                Front().Front();
                break;
        }
        return this;
    }
    public Rubik Back(TurnType turnType = TurnType.Clockwise)
    {
        var ub = _edges[EdgePositions.UB];
        var br = _edges[EdgePositions.BR];
        var bl = _edges[EdgePositions.BL];
        var db = _edges[EdgePositions.DB];
        var ubl = _vertexes[VertexPositions.UBL];
        var ubr = _vertexes[VertexPositions.UBR];
        var dbl = _vertexes[VertexPositions.DBL];
        var dbr = _vertexes[VertexPositions.DBR];

        switch(turnType)
        {
            case TurnType.Clockwise:
                ub.Flip();
                br.Flip();
                bl.Flip();
                db.Flip();
                ubl.Turn(VertexOrientations.Counterclockwise);
                ubr.Turn(VertexOrientations.Clockwise);
                dbr.Turn(VertexOrientations.Counterclockwise);
                dbl.Turn(VertexOrientations.Clockwise);
                _edges[EdgePositions.UB] = br;
                _edges[EdgePositions.BR] = db;
                _edges[EdgePositions.DB] = bl;
                _edges[EdgePositions.BL] = ub;
                _vertexes[VertexPositions.UBL] = ubr;
                _vertexes[VertexPositions.UBR] = dbr;
                _vertexes[VertexPositions.DBR] = dbl;
                _vertexes[VertexPositions.DBL] = ubl;

                PushMovement(Faces.Back);
                break;

            case TurnType.Counterclockwise:
                Back().Back().Back();
                break;

            case TurnType.Half:
                Back().Back();
                break;
        }
        return this;
    }
    public Rubik TurnByAlg(string? algorithm)
    {
        algorithm = algorithm?.Trim().ToUpper().Replace(" ", "");
        Regex completeAlgregex = CompleteAlgorithmRegex();
        if (!completeAlgregex.IsMatch(algorithm ?? string.Empty))
        {
            throw new FormatException("The algorithm does not contain a valid format.");
        }

        var matches = SingleMoveAlgorithmRegex().Matches(algorithm ?? string.Empty);

        char move = ' ';
        char moveType = ' ';
        TurnType turnType;
        string singleMove = string.Empty;
        foreach(string algMatch in matches.Select(m => m.Value))
        {
            singleMove = algMatch;
            move = algMatch[0];
            if(algMatch.Length == 1)
            {
                singleMove += CLOCKWISE_TURN_NOTATION;
            }
            moveType = singleMove[1];

            turnType = moveType switch
            {
                HALF_TURN_NOTATION => TurnType.Half,
                COUNTERCLOCKWISE_TURN_NOTATION => TurnType.Counterclockwise,
                CLOCKWISE_TURN_NOTATION => TurnType.Clockwise,
                COUNTERCLOCKWISE_NUMBER_TURN_NOTATION => TurnType.Counterclockwise,
                _ => throw new FormatException("The algorithm does not contain a valid format.")
            };

            switch(move)
            {
                case TURN_UP:
                    Up(turnType);
                    break;
                case TURN_DOWN:
                    Down(turnType);
                    break;
                case TURN_FRONT:
                    Front(turnType);
                    break;
                case TURN_BACK:
                    Back(turnType);
                    break;
                case TURN_LEFT:
                    Left(turnType);
                    break;
                case TURN_RIGHT:
                    Right(turnType);
                    break;
            }
        }
        return this;
    }
    public Rubik TurnByFace(Faces face, TurnType turn)
    {
        return face switch
        {
            Faces.Up => Up(turn),
            Faces.Down => Down(turn),
            Faces.Left => Left(turn),
            Faces.Right => Right(turn),
            Faces.Front => Front(turn),
            Faces.Back => Back(turn),
            _ => this,
        };
    }
    public Rubik Undo()
    {
        if (_movsList.Count == 0) return this;
        var mov = _movsList.Peek();
        return mov.TurnType switch
        {
            TurnType.Half => TurnByFace(mov.Face, TurnType.Half),
            TurnType.Counterclockwise => TurnByFace(mov.Face, TurnType.Clockwise),
            TurnType.Clockwise => TurnByFace(mov.Face, TurnType.Counterclockwise),
            _ => this,
        };
    }
    public Rubik Undo(int moves)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(moves);
        for (int i = 0; i < moves; i++)
        {
            Undo();
        }
        return this;
    }
    #endregion

    public EdgeValues PieceInfo(EdgePositions edgePosition)
    {
        return new EdgeValues
        {
            Orientation = _edges[edgePosition].Orientation,
            Destination = _edges[edgePosition].Destination
        };
    }
    public VertexValues PieceInfo(VertexPositions vertexPosition)
    {
        return new VertexValues
        {
            Orientation = _vertexes[vertexPosition].Orientation,
            Destination = _vertexes[vertexPosition].Destination
        };
    }
    public static List<VertexPositions> FaceVertexesNames(Faces faceName)
    {
        List<VertexPositions> vertexes = [];
        switch (faceName)
        {
            case Faces.Up:
                vertexes.AddRange([
                    VertexPositions.UFL,
                    VertexPositions.UBL,
                    VertexPositions.UBR,
                    VertexPositions.UFR
                ]);
                break;
            case Faces.Down:
                vertexes.AddRange([
                    VertexPositions.DFL,
                    VertexPositions.DFR,
                    VertexPositions.DBR,
                    VertexPositions.DBL
                ]);
                break;
            case Faces.Left:
                vertexes.AddRange([
                    VertexPositions.UFL,
                    VertexPositions.DFL,
                    VertexPositions.DBL,
                    VertexPositions.UBL
                ]);
                break;
            case Faces.Right:
                vertexes.AddRange([
                    VertexPositions.UFR,
                    VertexPositions.UBR,
                    VertexPositions.DBR,
                    VertexPositions.DFR
                ]);
                break;
            case Faces.Front:
                vertexes.AddRange([
                    VertexPositions.UFL,
                    VertexPositions.UFR,
                    VertexPositions.DFR,
                    VertexPositions.DFL
                ]);
                break;
            case Faces.Back:
                vertexes.AddRange([
                    VertexPositions.UBL,
                    VertexPositions.DBL,
                    VertexPositions.DBR,
                    VertexPositions.UBR
                ]);
                break;
        }
        return vertexes;
    }
    public static List<EdgePositions> FaceEdgesNames(Faces faceName)
    {
        List<EdgePositions> edges = [];
        switch (faceName)
        {
            case Faces.Up:
                edges.AddRange([
                    EdgePositions.UF,
                    EdgePositions.UL,
                    EdgePositions.UB,
                    EdgePositions.UR
                ]);
                break;
            case Faces.Down:
                edges.AddRange([
                    EdgePositions.DF,
                    EdgePositions.DR,
                    EdgePositions.DB,
                    EdgePositions.DL
                ]);
                break;
            case Faces.Left:
                edges.AddRange([
                    EdgePositions.UL,
                    EdgePositions.FL,
                    EdgePositions.DL,
                    EdgePositions.BL
                ]);
                break;
            case Faces.Right:
                edges.AddRange([
                    EdgePositions.UR,
                    EdgePositions.BR,
                    EdgePositions.DR,
                    EdgePositions.FR
                ]);
                break;
            case Faces.Front:
                edges.AddRange([
                    EdgePositions.UF,
                    EdgePositions.FR,
                    EdgePositions.DF,
                    EdgePositions.FL
                ]);
                break;
            case Faces.Back:
                edges.AddRange([
                    EdgePositions.UB,
                    EdgePositions.BL,
                    EdgePositions.DB,
                    EdgePositions.BR
                ]);
                break;
        }
        return edges;
    }

    public PieceLoops<EdgeValues, EdgeOrientations, EdgePositions> EdgeLoops()
    {
        PieceLoops<EdgeValues, EdgeOrientations, EdgePositions> result = new();
        List<EdgePositions> edgesAssignedToALoop = [];
        foreach(var firstEdgeInLoop in _edges)
        {
            List<EdgeValues> newEdgeLoop = [];
            if (edgesAssignedToALoop.Contains(firstEdgeInLoop.Value.Destination)) {
                continue;
            }
            var edgePositionToEval = firstEdgeInLoop.Key;
            do
            {
                var edgeInEvalPosition = Edges.Where(e => e.Key == edgePositionToEval).First().Value;
                var edgeOrientation = edgeInEvalPosition.Orientation;
                EdgeValues newValues = new()
                {
                    Destination = edgePositionToEval,
                    Orientation = edgeOrientation
                };
                newEdgeLoop.Add(newValues);
                edgesAssignedToALoop.Add(edgePositionToEval);
                edgePositionToEval = edgeInEvalPosition.Destination;
            } while (edgePositionToEval != firstEdgeInLoop.Key);
            result.Add(newEdgeLoop);
        }
        return result;
    }

    public PieceLoops<VertexValues, VertexOrientations, VertexPositions> VertexLoops()
    {
        PieceLoops<VertexValues, VertexOrientations, VertexPositions> result = new();
        List<VertexPositions> vertexesAssignedToALoop = [];
        foreach(var firstVertexInLoop in _vertexes)
        {
            List<VertexValues> newVertexLoop = [];
            if (vertexesAssignedToALoop.Contains(firstVertexInLoop.Value.Destination)) {
                continue;
            }
            var vertexPositionToEval = firstVertexInLoop.Key;
            do
            {
                var vertexInEvalPosition = Vertexes.Where(e => e.Key == vertexPositionToEval).First().Value;
                var vertexOrientation = vertexInEvalPosition.Orientation;
                VertexValues newValues = new()
                {
                    Destination = vertexPositionToEval,
                    Orientation = vertexOrientation
                };
                newVertexLoop.Add(newValues);
                vertexesAssignedToALoop.Add(vertexPositionToEval);
                vertexPositionToEval = vertexInEvalPosition.Destination;
            } while (vertexPositionToEval != firstVertexInLoop.Key);
            result.Add(newVertexLoop);
        }
        return result;
    }

    [GeneratedRegex("^([UDFBLR][123']?)+$")]
    private static partial Regex CompleteAlgorithmRegex();
    [GeneratedRegex("[UDFBLR][123']?")]
    private static partial Regex SingleMoveAlgorithmRegex();

    private void PushMovement(Faces face)
    {
        if (_movsList.Count > 0 && _movsList.Peek().Face == face)
        {
            var lastMove = _movsList.Pop();
            if (lastMove.TurnType != TurnType.Counterclockwise)
            {
                lastMove.TurnType = lastMove.TurnType switch
                {
                    TurnType.Clockwise => TurnType.Half,
                    TurnType.Half => TurnType.Counterclockwise,
                    _ => throw new NotImplementedException("The cube just popped up...")
                };
                _movsList.Push(lastMove);
            }
        }
        else
        {
            _movsList.Push((face, TurnType.Clockwise));
        }
    }
}
