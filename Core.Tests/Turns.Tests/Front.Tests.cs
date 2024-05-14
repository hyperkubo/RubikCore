using NUnit.Framework.Internal;
namespace Core.Tests.Turns.Tests
{
    internal class FrontTests
    {
        private Rubik _myRubikCube;
        [SetUp]
        public void SetUp()
        {
            _myRubikCube = new Rubik();
        }

        [Test]
        [TestCase(TurnType.Half, EdgePositions.UF, EdgePositions.DF)]
        [TestCase(TurnType.Half, EdgePositions.DF, EdgePositions.UF)]
        [TestCase(TurnType.Half, EdgePositions.FL, EdgePositions.FR)]
        [TestCase(TurnType.Half, EdgePositions.FR, EdgePositions.FL)]
        public void HalfTurn_WhenCalled_EdgeAfterMoveSameAsEdgeBeforeMove(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Front(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
            });
        }
        [Test]
        [TestCase(TurnType.Clockwise, EdgePositions.UF, EdgePositions.FR)]
        [TestCase(TurnType.Clockwise, EdgePositions.FR, EdgePositions.DF)]
        [TestCase(TurnType.Clockwise, EdgePositions.DF, EdgePositions.FL)]
        [TestCase(TurnType.Clockwise, EdgePositions.FL, EdgePositions.UF)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UF, EdgePositions.FL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.FL, EdgePositions.DF)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DF, EdgePositions.FR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.FR, EdgePositions.UF)]
        public void ClockwiseAndCounterclockwise_WhenCalled_EdgeFlip(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Front(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
                Assert.That(edgeAfter.Orientation, Is.EqualTo(edgeBefore.Orientation.Flipped()));
            });
        }

        [Test]
        [TestCase(EdgePositions.UL, TurnType.Clockwise)]
        [TestCase(EdgePositions.UR, TurnType.Clockwise)]
        [TestCase(EdgePositions.UB, TurnType.Clockwise)]
        [TestCase(EdgePositions.BL, TurnType.Clockwise)]
        [TestCase(EdgePositions.BR, TurnType.Clockwise)]
        [TestCase(EdgePositions.DL, TurnType.Clockwise)]
        [TestCase(EdgePositions.DR, TurnType.Clockwise)]
        [TestCase(EdgePositions.DB, TurnType.Clockwise)]
        [TestCase(EdgePositions.UL, TurnType.Half)]
        [TestCase(EdgePositions.UR, TurnType.Half)]
        [TestCase(EdgePositions.UB, TurnType.Half)]
        [TestCase(EdgePositions.BL, TurnType.Half)]
        [TestCase(EdgePositions.BR, TurnType.Half)]
        [TestCase(EdgePositions.DL, TurnType.Half)]
        [TestCase(EdgePositions.DR, TurnType.Half)]
        [TestCase(EdgePositions.DB, TurnType.Half)]
        [TestCase(EdgePositions.UL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UB, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.BR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DB, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_EdgeDontChange(EdgePositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Front(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }

        [Test]
        [TestCase(TurnType.Half, VertexPositions.UFL, VertexPositions.DFR)]
        [TestCase(TurnType.Half, VertexPositions.DFR, VertexPositions.UFL)]
        [TestCase(TurnType.Half, VertexPositions.UFR, VertexPositions.DFL)]
        [TestCase(TurnType.Half, VertexPositions.DFL, VertexPositions.UFR)]
        public void HalfTurn_WhenCalled_VertexAfterMoveSameAsVertexBeforeMove(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Front(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(VertexBefore.Orientation, Is.EqualTo(vertexAfter.Orientation));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UFL, VertexPositions.UFR)]
        [TestCase(TurnType.Clockwise, VertexPositions.DFR, VertexPositions.DFL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UFL, VertexPositions.DFL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DFR, VertexPositions.UFR)]
        public void ClockwiseAndCounterclockwise_WhenCalled_VertexTurnsClockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Front(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(VertexBefore.Orientation.TurnClockwise()));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UFR, VertexPositions.DFR)]
        [TestCase(TurnType.Clockwise, VertexPositions.DFL, VertexPositions.UFL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UFR, VertexPositions.UFL)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DFL, VertexPositions.DFR)]
        public void ClockwiseAndCounterclockwise_WhenCalled_VertexTurnsCounterclockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Front(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(VertexBefore.Orientation.TurnCounterclockwise()));
            });
        }

        [Test]
        [TestCase(VertexPositions.UBL, TurnType.Clockwise)]
        [TestCase(VertexPositions.UBR, TurnType.Clockwise)]
        [TestCase(VertexPositions.DBL, TurnType.Clockwise)]
        [TestCase(VertexPositions.DBR, TurnType.Clockwise)]
        [TestCase(VertexPositions.UBL, TurnType.Half)]
        [TestCase(VertexPositions.UBR, TurnType.Half)]
        [TestCase(VertexPositions.DBL, TurnType.Half)]
        [TestCase(VertexPositions.DBR, TurnType.Half)]
        [TestCase(VertexPositions.UBL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UBR, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DBL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DBR, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_VertexDontChange(VertexPositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Front(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }
    }
}
