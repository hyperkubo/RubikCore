using NUnit.Framework.Internal;

namespace Core.Tests
{
    public class ExtensionMethods
    {
        [Test]
        [TestCase(EdgeOrientations.Ok)]
        [TestCase(EdgeOrientations.Incorrect)]
        public void Flipped_WhenCalled_ReturnsPieceFlipped(EdgeOrientations orientation)
        {
            Assert.That(orientation, Is.Not.EqualTo(orientation.Flipped()));
        }

        [Test]
        [TestCase(VertexOrientations.Ok, VertexOrientations.Clockwise)]
        [TestCase(VertexOrientations.Clockwise, VertexOrientations.Counterclockwise)]
        [TestCase(VertexOrientations.Counterclockwise, VertexOrientations.Ok)]
        public void TurnClockwise_WhenCalled_ReturnsNextClockwisePostition(VertexOrientations orientation, VertexOrientations expectedOrientation)
        {
            Assert.That(orientation.TurnClockwise(), Is.EqualTo(expectedOrientation));
        }

        [Test]
        [TestCase(VertexOrientations.Ok, VertexOrientations.Counterclockwise)]
        [TestCase(VertexOrientations.Counterclockwise, VertexOrientations.Clockwise)]
        [TestCase(VertexOrientations.Clockwise, VertexOrientations.Ok)]
        public void TurnCounterClockwise_WhenCalled_ReturnsNextClockwisePostition(VertexOrientations orientation, VertexOrientations expectedOrientation)
        {
            Assert.That(orientation.TurnCounterclockwise(), Is.EqualTo(expectedOrientation));
        }

        [Test]
        [TestCase(EdgePositions.UF, Faces.Up, TurnType.Clockwise, EdgePositions.UL)]
        [TestCase(EdgePositions.BR, Faces.Right, TurnType.Counterclockwise, EdgePositions.UR)]
        [TestCase(EdgePositions.FL, Faces.Left, TurnType.Half, EdgePositions.BL)]
        public void NextPosAfterTurn_WhenEdgeIsInTheFace_ReturnsNextPositionInLoop(EdgePositions position, Faces face, TurnType turnType, EdgePositions expectedPosition)
        {
            var newPos = position.NextPosAfterTurn(face, turnType);
            Assert.That(newPos, Is.EqualTo(expectedPosition));
        }

        [Test]
        [TestCase(EdgePositions.UF, Faces.Back, TurnType.Clockwise)]
        [TestCase(EdgePositions.BR, Faces.Front, TurnType.Counterclockwise)]
        [TestCase(EdgePositions.FL, Faces.Down, TurnType.Half)]
        public void NextPosAfterTurn_WhenEdgeIsNotInTheFace_ReturnsNextPositionInLoop(EdgePositions position, Faces face, TurnType turnType)
        {
            var newPos = position.NextPosAfterTurn(face, turnType);
            Assert.That(newPos, Is.EqualTo(position));
        }

        [Test]
        [TestCase(VertexPositions.UFL, Faces.Up, TurnType.Clockwise, VertexPositions.UBL)]
        [TestCase(VertexPositions.DBL, Faces.Back, TurnType.Counterclockwise, VertexPositions.UBL)]
        [TestCase(VertexPositions.UBL, Faces.Left, TurnType.Half, VertexPositions.DFL)]
        public void NextPosAfterTurn_WhenVertexIsInTheFace_ReturnsNextPositionInLoop(VertexPositions position, Faces face, TurnType turnType, VertexPositions expectedPosition)
        {
            var newPos = position.NextPosAfterTurn(face, turnType);
            Assert.That(newPos, Is.EqualTo(expectedPosition));
        }

        [Test]
        [TestCase(VertexPositions.UFL, Faces.Right, TurnType.Clockwise)]
        [TestCase(VertexPositions.DBL, Faces.Up, TurnType.Counterclockwise)]
        [TestCase(VertexPositions.UBL, Faces.Down, TurnType.Half)]
        public void NextPosAfterTurn_WhenVertexIsNotInTheFace_ReturnsNextPositionInLoop(VertexPositions position, Faces face, TurnType turnType)
        {
            var newPos = position.NextPosAfterTurn(face, turnType);
            Assert.That(newPos, Is.EqualTo(position));
        }
    }
}
