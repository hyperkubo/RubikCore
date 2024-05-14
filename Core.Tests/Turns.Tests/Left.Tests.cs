using NUnit.Framework.Internal;
namespace Core.Tests.Turns.Tests
{
    internal class LeftTests
    {
        private Rubik _myRubikCube;
        [SetUp]
        public void SetUp()
        {
            _myRubikCube = new Rubik();
        }

        [Test]
        [TestCase(TurnType.Clockwise, EdgePositions.FL, EdgePositions.DL)]
        [TestCase(TurnType.Clockwise, EdgePositions.DL, EdgePositions.BL)]
        [TestCase(TurnType.Clockwise, EdgePositions.BL, EdgePositions.UL)]
        [TestCase(TurnType.Clockwise, EdgePositions.UL, EdgePositions.FL)]
        [TestCase(TurnType.Half, EdgePositions.FL, EdgePositions.BL)]
        [TestCase(TurnType.Half, EdgePositions.BL, EdgePositions.FL)]
        [TestCase(TurnType.Half, EdgePositions.UL, EdgePositions.DL)]
        [TestCase(TurnType.Half, EdgePositions.DL, EdgePositions.UL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.FL, EdgePositions.UL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UL, EdgePositions.BL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.BL, EdgePositions.DL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DL, EdgePositions.FL)]
        public void AnyTurnType_WhenCalled_EdgeAfterMoveSameAsEdgeBeforeMove(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Left(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
            });
        }

        [Test]
        [TestCase(EdgePositions.UF, TurnType.Clockwise)]
        [TestCase(EdgePositions.UR, TurnType.Clockwise)]
        [TestCase(EdgePositions.UB, TurnType.Clockwise)]
        [TestCase(EdgePositions.FR, TurnType.Clockwise)]
        [TestCase(EdgePositions.BR, TurnType.Clockwise)]
        [TestCase(EdgePositions.DF, TurnType.Clockwise)]
        [TestCase(EdgePositions.DR, TurnType.Clockwise)]
        [TestCase(EdgePositions.DB, TurnType.Clockwise)]
        [TestCase(EdgePositions.UF, TurnType.Half)]
        [TestCase(EdgePositions.UR, TurnType.Half)]
        [TestCase(EdgePositions.UB, TurnType.Half)]
        [TestCase(EdgePositions.FR, TurnType.Half)]
        [TestCase(EdgePositions.BR, TurnType.Half)]
        [TestCase(EdgePositions.DF, TurnType.Half)]
        [TestCase(EdgePositions.DR, TurnType.Half)]
        [TestCase(EdgePositions.DB, TurnType.Half)]
        [TestCase(EdgePositions.UF, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UB, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.FR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DF, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DB, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_EdgeDontChange(EdgePositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Left(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }

        [Test]
        [TestCase(TurnType.Half, VertexPositions.UFL, VertexPositions.DBL)]
        [TestCase(TurnType.Half, VertexPositions.DBL, VertexPositions.UFL)]
        [TestCase(TurnType.Half, VertexPositions.DFL, VertexPositions.UBL)]
        [TestCase(TurnType.Half, VertexPositions.UBL, VertexPositions.DFL)]
        public void HalfTurn_WhenCalled_VertexAfterMoveSameAsVertexBeforeMove(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Left(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(VertexBefore.Orientation, Is.EqualTo(vertexAfter.Orientation));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UBL, VertexPositions.UFL)]
        [TestCase(TurnType.Clockwise, VertexPositions.DFL, VertexPositions.DBL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UBL, VertexPositions.DBL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DFL, VertexPositions.UFL)]
        public void ClockwiseAndCounterClockwise_WhenCalled_VertexTurnsClockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var vertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Left(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(vertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(vertexBefore.Orientation.TurnClockwise()));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UFL, VertexPositions.DFL)]
        [TestCase(TurnType.Clockwise, VertexPositions.DBL, VertexPositions.UBL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UFL, VertexPositions.UBL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DBL, VertexPositions.DFL)]
        public void ClockwiseAndCounterClockwise_WhenCalled_VertexTurnsCounterclockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var vertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Left(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(vertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(vertexBefore.Orientation.TurnCounterclockwise()));
            });
        }

        [Test]
        [TestCase(VertexPositions.UFR, TurnType.Clockwise)]
        [TestCase(VertexPositions.UBR, TurnType.Clockwise)]
        [TestCase(VertexPositions.DFR, TurnType.Clockwise)]
        [TestCase(VertexPositions.DBR, TurnType.Clockwise)]
        [TestCase(VertexPositions.UFR, TurnType.Half)]
        [TestCase(VertexPositions.UBR, TurnType.Half)]
        [TestCase(VertexPositions.DFR, TurnType.Half)]
        [TestCase(VertexPositions.DBR, TurnType.Half)]
        [TestCase(VertexPositions.UFR, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UBR, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DFR, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DBR, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_VertexDontChange(VertexPositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Left(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }
    }
}
