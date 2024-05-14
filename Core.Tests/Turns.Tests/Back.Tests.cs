using NUnit.Framework.Internal;
namespace Core.Tests.Turns.Tests
{
    internal class BackTests
    {
        private Rubik _myRubikCube;
        [SetUp]
        public void SetUp()
        {
            _myRubikCube = new Rubik();
        }

        [Test]
        [TestCase(TurnType.Half, EdgePositions.UB, EdgePositions.DB)]
        [TestCase(TurnType.Half, EdgePositions.DB, EdgePositions.UB)]
        [TestCase(TurnType.Half, EdgePositions.BL, EdgePositions.BR)]
        [TestCase(TurnType.Half, EdgePositions.BR, EdgePositions.BL)]
        public void HalfTurn_WhenCalled_EdgeAfterMoveSameAsEdgeBeforeMove(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Back(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, EdgePositions.UB, EdgePositions.BL)]
        [TestCase(TurnType.Clockwise, EdgePositions.BL, EdgePositions.DB)]
        [TestCase(TurnType.Clockwise, EdgePositions.DB, EdgePositions.BR)]
        [TestCase(TurnType.Clockwise, EdgePositions.BR, EdgePositions.UB)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.UB, EdgePositions.BR)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.BR, EdgePositions.DB)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.DB, EdgePositions.BL)]
        [TestCase(TurnType.Counterclockwise, EdgePositions.BL, EdgePositions.UB)]
        public void ClockwiseAndCounterclockwise_WhenCalled_EdgeFlip(TurnType turnType, EdgePositions positionBeforeMov, EdgePositions positionAfterMov)
        {
            var edgeBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Back(turnType);
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
        [TestCase(EdgePositions.UF, TurnType.Clockwise)]
        [TestCase(EdgePositions.FL, TurnType.Clockwise)]
        [TestCase(EdgePositions.FR, TurnType.Clockwise)]
        [TestCase(EdgePositions.DL, TurnType.Clockwise)]
        [TestCase(EdgePositions.DR, TurnType.Clockwise)]
        [TestCase(EdgePositions.DF, TurnType.Clockwise)]
        [TestCase(EdgePositions.UL, TurnType.Half)]
        [TestCase(EdgePositions.UR, TurnType.Half)]
        [TestCase(EdgePositions.UF, TurnType.Half)]
        [TestCase(EdgePositions.FL, TurnType.Half)]
        [TestCase(EdgePositions.FR, TurnType.Half)]
        [TestCase(EdgePositions.DL, TurnType.Half)]
        [TestCase(EdgePositions.DR, TurnType.Half)]
        [TestCase(EdgePositions.DF, TurnType.Half)]
        [TestCase(EdgePositions.UL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.UF, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.FL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.FR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DL, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DR, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.DF, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_EdgeDontChange(EdgePositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Back(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }

        [Test]
        [TestCase(TurnType.Half, VertexPositions.UBL, VertexPositions.DBR)]
        [TestCase(TurnType.Half, VertexPositions.DBR, VertexPositions.UBL)]
        [TestCase(TurnType.Half, VertexPositions.UBR, VertexPositions.DBL)]
        [TestCase(TurnType.Half, VertexPositions.DBL, VertexPositions.UBR)]
        public void HalfTurn_WhenCalled_VertexAfterMoveSameAsVertexBeforeMove(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Back(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(VertexBefore.Orientation, Is.EqualTo(vertexAfter.Orientation));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UBR, VertexPositions.UBL)]
        [TestCase(TurnType.Clockwise, VertexPositions.DBL, VertexPositions.DBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UBR, VertexPositions.DBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DBL, VertexPositions.UBL)]
        public void ClockwiseAndCounterclockwise_WhenCalled_VertexTurnsClockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Back(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(VertexBefore.Orientation.TurnClockwise()));
            });
        }

        [Test]
        [TestCase(TurnType.Clockwise, VertexPositions.UBL, VertexPositions.DBL)]
        [TestCase(TurnType.Clockwise, VertexPositions.DBR, VertexPositions.UBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.UBL, VertexPositions.UBR)]
        [TestCase(TurnType.Counterclockwise, VertexPositions.DBR, VertexPositions.DBL)]
        public void ClockwiseAndCounterclockwise_WhenCalled_VertexTurnsCounterclockwise(TurnType turnType, VertexPositions positionBeforeMov, VertexPositions positionAfterMov)
        {
            var VertexBefore = _myRubikCube.PieceInfo(positionBeforeMov);
            _myRubikCube.Back(turnType);
            var vertexAfter = _myRubikCube.PieceInfo(positionAfterMov);

            Assert.Multiple(() =>
            {
                Assert.That(VertexBefore.Destination, Is.EqualTo(vertexAfter.Destination));
                Assert.That(vertexAfter.Orientation, Is.EqualTo(VertexBefore.Orientation.TurnCounterclockwise()));
            });
        }

        [Test]
        [TestCase(VertexPositions.UFL, TurnType.Clockwise)]
        [TestCase(VertexPositions.UFR, TurnType.Clockwise)]
        [TestCase(VertexPositions.DFL, TurnType.Clockwise)]
        [TestCase(VertexPositions.DFR, TurnType.Clockwise)]
        [TestCase(VertexPositions.UFL, TurnType.Half)]
        [TestCase(VertexPositions.UFR, TurnType.Half)]
        [TestCase(VertexPositions.DFL, TurnType.Half)]
        [TestCase(VertexPositions.DFR, TurnType.Half)]
        [TestCase(VertexPositions.UFL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UFR, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DFL, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.DFR, TurnType.Counterclockwise)]
        public void AnyTurnType_WhenCalled_VertexDontChange(VertexPositions position, TurnType turnType)
        {
            var edgeBefore = _myRubikCube.PieceInfo(position);
            _myRubikCube.Back(turnType);
            var edgeAfter = _myRubikCube.PieceInfo(position);


            Assert.Multiple(() =>
            {
                Assert.That(edgeBefore.Orientation, Is.EqualTo(edgeAfter.Orientation));
                Assert.That(edgeBefore.Destination, Is.EqualTo(edgeAfter.Destination));
            });
        }
    }
}
