using NUnit.Framework.Internal;
namespace Core.Tests.Turns.Tests
{
    internal class DownTests
    {
        private Rubik _myRubikCube;
        [SetUp]
        public void SetUp()
        {
            _myRubikCube = new Rubik();
        }

        [Test]
        [TestCase(TurnType.Clockwise, EdgePositions.DF, EdgePositions.DR)]
        [TestCase(TurnType.Clockwise, EdgePositions.DR, EdgePositions.DB)]
        [TestCase(TurnType.Clockwise, EdgePositions.DB, EdgePositions.DL)]
        [TestCase(TurnType.Clockwise, EdgePositions.DL, EdgePositions.DF)]
        [TestCase(TurnType.Half, EdgePositions.DF, EdgePositions.DB)]
        [TestCase(TurnType.Half, EdgePositions.DB, EdgePositions.DF)]
        [TestCase(TurnType.Half, EdgePositions.DL, EdgePositions.DR)]
        [TestCase(TurnType.Half, EdgePositions.DR, EdgePositions.DL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DF, EdgePositions.DL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DL, EdgePositions.DB)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DB, EdgePositions.DR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DR, EdgePositions.DF)]
        public void AnyTurnType_WhenCalled_EdgeAfterMoveSameAsEdgeBeforeMove(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Down(turnType);
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
        [TestCase(EdgePositions.UF, TurnType.Clockwise)]
        [TestCase(EdgePositions.UL, TurnType.Clockwise)]
        [TestCase(EdgePositions.UB, TurnType.Clockwise)]
        [TestCase(EdgePositions.UR, TurnType.Clockwise)]
        [TestCase(EdgePositions.FL, TurnType.Half)]
        [TestCase(EdgePositions.FR, TurnType.Half)]
        [TestCase(EdgePositions.BL, TurnType.Half)]
        [TestCase(EdgePositions.BR, TurnType.Half)]
        [TestCase(EdgePositions.UF, TurnType.Half)]
        [TestCase(EdgePositions.UL, TurnType.Half)]
        [TestCase(EdgePositions.UB, TurnType.Half)]
        [TestCase(EdgePositions.UR, TurnType.Half)]
        [TestCase(EdgePositions.FL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.FR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UF, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UB, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UR, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_EdgeDontChange(EdgePositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Down(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.DFL, VertexPositions.DFR)]
        [TestCase(TurnType.Clockwise, VertexPositions.DFR, VertexPositions.DBR)]
        [TestCase(TurnType.Clockwise, VertexPositions.DBR, VertexPositions.DBL)]
        [TestCase(TurnType.Clockwise, VertexPositions.DBL, VertexPositions.DFL)]
        [TestCase(TurnType.Half, VertexPositions.DFL, VertexPositions.DBR)]
        [TestCase(TurnType.Half, VertexPositions.DBR, VertexPositions.DFL)]
        [TestCase(TurnType.Half, VertexPositions.DFR, VertexPositions.DBL)]
        [TestCase(TurnType.Half, VertexPositions.DBL, VertexPositions.DFR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DFL, VertexPositions.DBL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DBL, VertexPositions.DBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DBR, VertexPositions.DFR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DFR, VertexPositions.DFL)]
        public void AnyTurnType_WhenCalled_VertexAfterMoveSameAsVertexBeforeMove(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Down(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(VertexBefore.Orientation, Is.EqualTo(vertexAfter.Orientation));
            });
        }

        [Test]
        [TestCase(VertexPositions.UFL, TurnType.Clockwise)]
        [TestCase(VertexPositions.UFR, TurnType.Clockwise)]
        [TestCase(VertexPositions.UBL, TurnType.Clockwise)]
        [TestCase(VertexPositions.UBR, TurnType.Clockwise)]
        [TestCase(VertexPositions.UFL, TurnType.Half)]
        [TestCase(VertexPositions.UFR, TurnType.Half)]
        [TestCase(VertexPositions.UBL, TurnType.Half)]
        [TestCase(VertexPositions.UBR, TurnType.Half)]
        [TestCase(VertexPositions.UFL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UFR, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UBL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UBR, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_VertexDontChange(VertexPositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Down(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }
    }
}
