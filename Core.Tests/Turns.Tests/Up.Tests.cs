using NUnit.Framework.Internal;
namespace Core.Tests.Turns.Tests
{
    internal class UpTests
    {
        private Rubik _myRubikCube;
        [SetUp]
        public void SetUp()
        {
            _myRubikCube = new Rubik();
        }

        [Test]
        [TestCase(TurnType.Clockwise, EdgePositions.UF, EdgePositions.UL)]
        [TestCase(TurnType.Clockwise, EdgePositions.UL, EdgePositions.UB)]
        [TestCase(TurnType.Clockwise, EdgePositions.UB, EdgePositions.UR)]
        [TestCase(TurnType.Clockwise, EdgePositions.UR, EdgePositions.UF)]
        [TestCase(TurnType.Half, EdgePositions.UF, EdgePositions.UB)]
        [TestCase(TurnType.Half, EdgePositions.UB, EdgePositions.UF)]
        [TestCase(TurnType.Half, EdgePositions.UL, EdgePositions.UR)]
        [TestCase(TurnType.Half, EdgePositions.UR, EdgePositions.UL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UF, EdgePositions.UR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UR, EdgePositions.UB)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UB, EdgePositions.UL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UL, EdgePositions.UF)]
        public void AnyTurnType_WhenCalled_EdgeAfterMoveSameAsEdgeBeforeMove(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Up(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
            });
        }

        [Test]
        [TestCase(EdgePositions.FL, TurnType.Clockwise)]
        [TestCase(EdgePositions.FR, TurnType.Clockwise)]
        [TestCase(EdgePositions.BL, TurnType.Clockwise)]
        [TestCase(EdgePositions.BR, TurnType.Clockwise)]
        [TestCase(EdgePositions.DF, TurnType.Clockwise)]
        [TestCase(EdgePositions.DL, TurnType.Clockwise)]
        [TestCase(EdgePositions.DB, TurnType.Clockwise)]
        [TestCase(EdgePositions.DR, TurnType.Clockwise)]
        [TestCase(EdgePositions.FL, TurnType.Half)]
        [TestCase(EdgePositions.FR, TurnType.Half)]
        [TestCase(EdgePositions.BL, TurnType.Half)]
        [TestCase(EdgePositions.BR, TurnType.Half)]
        [TestCase(EdgePositions.DF, TurnType.Half)]
        [TestCase(EdgePositions.DL, TurnType.Half)]
        [TestCase(EdgePositions.DB, TurnType.Half)]
        [TestCase(EdgePositions.DR, TurnType.Half)]
        [TestCase(EdgePositions.FL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.FR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DF, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DB, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DR, TurnType.Counterclockwise)]
        public void AnyTunrType_WhenCalled_EdgeDontChange(EdgePositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Up(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UFL, VertexPositions.UBL)]
        [TestCase(TurnType.Clockwise, VertexPositions.UBL, VertexPositions.UBR)]
        [TestCase(TurnType.Clockwise, VertexPositions.UBR, VertexPositions.UFR)]
        [TestCase(TurnType.Clockwise, VertexPositions.UFR, VertexPositions.UFL)]
        [TestCase(TurnType.Half, VertexPositions.UFL, VertexPositions.UBR)]
        [TestCase(TurnType.Half, VertexPositions.UBR, VertexPositions.UFL)]
        [TestCase(TurnType.Half, VertexPositions.UFR, VertexPositions.UBL)]
        [TestCase(TurnType.Half, VertexPositions.UBL, VertexPositions.UFR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UFL, VertexPositions.UFR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UFR, VertexPositions.UBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UBR, VertexPositions.UBL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UBL, VertexPositions.UFL)]
        public void AnyTurnType_WhenCalled_VertexAfterMoveSameAsVertexBeforeMove(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Up(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(VertexBefore.Orientation, Is.EqualTo(vertexAfter.Orientation));
            });
        }

        [Test]
        [TestCase(VertexPositions.DFL, TurnType.Clockwise)]
        [TestCase(VertexPositions.DFR, TurnType.Clockwise)]
        [TestCase(VertexPositions.DBL, TurnType.Clockwise)]
        [TestCase(VertexPositions.DBR, TurnType.Clockwise)]
        [TestCase(VertexPositions.DFL, TurnType.Half)]
        [TestCase(VertexPositions.DFR, TurnType.Half)]
        [TestCase(VertexPositions.DBL, TurnType.Half)]
        [TestCase(VertexPositions.DBR, TurnType.Half)]
        [TestCase(VertexPositions.DFL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DFR, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DBL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DBR, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_VertexDontChange(VertexPositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Up(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }
    }
}
