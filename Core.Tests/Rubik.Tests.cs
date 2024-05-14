using NUnit.Framework.Internal;
namespace Core.Tests;

public class RubikTests
{
    private Rubik _myRubikCube;
    [SetUp]
    public void Setup()
    {
        _myRubikCube = new Rubik();
    }

    [Test]
    [TestCase(EdgePositions.UF)]
    [TestCase(EdgePositions.UR)]
    public void PieceInfo_WhenCalledEdge_ReturnsCorrectPieceInfo(EdgePositions position)
    {
        var pieceInfo = _myRubikCube.PieceInfo(position);
        Assert.That(pieceInfo, Is.EqualTo(new EdgeValues { Orientation = EdgeOrientations.Ok, Destination = position }));
    }

    [Test]
    [TestCase(VertexPositions.UFL)]
    [TestCase(VertexPositions.UFR)]
    public void PieceInfo_WhenCalledVertex_ReturnsCorrectPieceInfo(VertexPositions position)
    {
        var pieceInfo = _myRubikCube.PieceInfo(position);
        Assert.That(pieceInfo, Is.EqualTo(new VertexValues { Orientation = VertexOrientations.Ok, Destination = position }));
    }

    [Test]
    [TestCase("ulr2u2fr2blr")]
    [TestCase("u'fd2flfr'd2")]
    public void RandomTurns_WhenCalled_OrientationsSumOk(string algorithm)
    {
        _myRubikCube.TurnByAlg(algorithm);
        var edgesOrientationsVal = _myRubikCube.EdgesOrientations.Sum(x => (int)x.Value) % 2;

        var vertexesOrientationsVal = _myRubikCube.VertexesOrientations.Sum(x => (int)x.Value) % 3;

        Assert.Multiple(() =>
        {
            Assert.That(vertexesOrientationsVal, Is.EqualTo(0));
            Assert.That(edgesOrientationsVal, Is.EqualTo(0));
        });
    }

    [Test]
    [TestCase("a")]
    [TestCase("a3")]
    [TestCase("'a")]
    public void TurnByAlg_SendInvaildFormat_ThrowException(string algorithm)
    {
        Assert.That(() =>_myRubikCube.TurnByAlg(algorithm), Throws.Exception.TypeOf<FormatException>());
    }

    [Test]
    [TestCase(Faces.Up, TurnType.Clockwise)]
    [TestCase(Faces.Front, TurnType.Counterclockwise)]
    [TestCase(Faces.Left, TurnType.Half)]
    public void PushMovement_WhenTurnAFace_MovsListHasTheMovement(Faces face, TurnType turnType)
    {
        _myRubikCube.TurnByFace(face, turnType);

        var lastMovInMovsList = _myRubikCube.MovementsList.Last();

        Assert.Multiple(() =>
        {
            Assert.That(lastMovInMovsList.Face, Is.EqualTo(face));
            Assert.That(lastMovInMovsList.TurnType, Is.EqualTo(turnType));
        });
    }
}