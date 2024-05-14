using NUnit.Framework.Internal;
namespace Core.Tests.Turns.Tests
{
    internal class RightTests
    {
        private Rubik _myRubikCube;
        [SetUp]
        public void SetUp()
        {
            _myRubikCube = new Rubik();
        }

        [Test]
        [TestCase(TurnType.Clockwise, EdgePositions.FR, EdgePositions.UR)]
        [TestCase(TurnType.Clockwise, EdgePositions.UR, EdgePositions.BR)]
        [TestCase(TurnType.Clockwise, EdgePositions.BR, EdgePositions.DR)]
        [TestCase(TurnType.Clockwise, EdgePositions.DR, EdgePositions.FR)]
        [TestCase(TurnType.Half, EdgePositions.FR, EdgePositions.BR)]
        [TestCase(TurnType.Half, EdgePositions.BR, EdgePositions.FR)]
        [TestCase(TurnType.Half, EdgePositions.UR, EdgePositions.DR)]
        [TestCase(TurnType.Half, EdgePositions.DR, EdgePositions.UR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.FR, EdgePositions.DR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DR, EdgePositions.BR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.BR, EdgePositions.UR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UR, EdgePositions.FR)]
        public void AnyTurnType_WhenCalled_EdgeAfterMoveSameAsEdgeBeforeMove(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Right(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
            });
        }

        [Test]
        [TestCase(EdgePositions.UF, TurnType.Clockwise)]
        [TestCase(EdgePositions.UL, TurnType.Clockwise)]
        [TestCase(EdgePositions.UB, TurnType.Clockwise)]
        [TestCase(EdgePositions.FL, TurnType.Clockwise)]
        [TestCase(EdgePositions.BL, TurnType.Clockwise)]
        [TestCase(EdgePositions.DF, TurnType.Clockwise)]
        [TestCase(EdgePositions.DL, TurnType.Clockwise)]
        [TestCase(EdgePositions.DB, TurnType.Clockwise)]
        [TestCase(EdgePositions.UF, TurnType.Half)]
        [TestCase(EdgePositions.UL, TurnType.Half)]
        [TestCase(EdgePositions.UB, TurnType.Half)]
        [TestCase(EdgePositions.FL, TurnType.Half)]
        [TestCase(EdgePositions.BL, TurnType.Half)]
        [TestCase(EdgePositions.DF, TurnType.Half)]
        [TestCase(EdgePositions.DL, TurnType.Half)]
        [TestCase(EdgePositions.DB, TurnType.Half)]
        [TestCase(EdgePositions.UF, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UB, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.FL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DF, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DB, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_EdgeDontChange(EdgePositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Right(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }

        [Test]
        [TestCase(TurnType.Half, VertexPositions.UFR, VertexPositions.DBR)]
        [TestCase(TurnType.Half, VertexPositions.DBR, VertexPositions.UFR)]
        [TestCase(TurnType.Half, VertexPositions.DFR, VertexPositions.UBR)]
        [TestCase(TurnType.Half, VertexPositions.UBR, VertexPositions.DFR)]
        public void HalfTurn_WhenCalled_VertexAfterMoveSameAsVertexBeforeMove(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Right(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(VertexBefore.Orientation, Is.EqualTo(vertexAfter.Orientation));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UFR, VertexPositions.UBR)]
        [TestCase(TurnType.Clockwise, VertexPositions.DBR, VertexPositions.DFR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UFR, VertexPositions.DFR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DBR, VertexPositions.UBR)]
        public void ClockwiseAndCounterClockwise_WhenCalled_VertexTurnsClockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var vertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Right(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(vertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(vertexBefore.Orientation.TurnClockwise()));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.DFR, VertexPositions.UFR)]
        [TestCase(TurnType.Clockwise, VertexPositions.UBR, VertexPositions.DBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DFR, VertexPositions.DBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UBR, VertexPositions.UFR)]
        public void ClockwiseAndCounterClockwise_WhenCalled_VertexTurnsCounterclockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var vertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Right(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(vertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(vertexBefore.Orientation.TurnCounterclockwise()));
            });
        }

        [Test]
        [TestCase(VertexPositions.UFL, TurnType.Clockwise)]
        [TestCase(VertexPositions.UBL, TurnType.Clockwise)]
        [TestCase(VertexPositions.DFL, TurnType.Clockwise)]
        [TestCase(VertexPositions.DBL, TurnType.Clockwise)]
        [TestCase(VertexPositions.UFL, TurnType.Half)]
        [TestCase(VertexPositions.UBL, TurnType.Half)]
        [TestCase(VertexPositions.DFL, TurnType.Half)]
        [TestCase(VertexPositions.DBL, TurnType.Half)]
        [TestCase(VertexPositions.UFL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UBL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DFL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DBL, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_VertexDontChange(VertexPositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Right(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }
    }
}
