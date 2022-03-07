using Xunit;
using GreedFlameTale.Model;

namespace GreedFlameTale.Tests.Model
{
    public class MeasureTest
    {
        /// <summary>
        /// Test the properties of a <see cref="Measure"/> with a zeroed <see cref="Measure.Maximum"/>
        /// </summary>
        [Fact]
        public void OnCreateWithZero()
        {
            // Arrange // Act
            var testMeasure = new Measure(0);
            // Assert
            Assert.Equal<int>(0, testMeasure.Maximum);
            Assert.Equal<int>(0, testMeasure.Value);
            Assert.Equal<int>(0, testMeasure.Percentage);
            Assert.True(testMeasure.IsFull);
            Assert.True(testMeasure.IsEmpty);
        }

        /// <summary>
        /// Test to verify if the <see cref="Measure.Maximum"/> is truncated to <see cref="ushort.MaxValue"/>
        /// after creation with a value higher than the upper limit.
        /// </summary>
        [Fact]
        public void ClampOverMaxOnNew()
        {
            // Arrange
            var maximum = ushort.MaxValue + 1;
            var value = maximum;
            // Act
            var testMeasure = new Measure(value, maximum);
            // Assert
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Maximum);
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the <see cref="Measure.Maximum"/> and <see cref="Measure.Value"/> are truncated to <see langword="0"/>
        /// after creation with negative maximum.
        /// </summary>
        [Fact]
        public void ClampNegativeMaxOnNew()
        {
            // Arrange
            var maximum = -1;
            var value = -1;
            // Act
            var testMeasure = new Measure(value, maximum);
            // Assert
            Assert.Equal<long>(0, testMeasure.Maximum);
            Assert.Equal<long>(0, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the value is truncated at zero after negative offsetting
        /// </summary>
        [Fact]
        public void OffsetByNegativeOnZeroUnit()
        {
            // Arrange
            var testMeasure = new Measure(0);
            // Act
            testMeasure.OffsetBy(-1);
            // Assert
            Assert.Equal<long>(0, testMeasure.Maximum);
            Assert.Equal<long>(0, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the value is truncated at <see cref="ushort.MaxValue"/> after ofsetting by a very big number
        /// </summary>
        [Fact]
        public void OffsetByBigNumber()
        {
            // Arrange
            var testMeasure = new Measure(0);
            // Act
            testMeasure.OffsetBy(long.MaxValue);
            // Assert
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Maximum);
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the value is truncated at <see cref="ushort.MaxValue"/>
        /// after offsetting to go over the upper limit
        /// </summary>
        [Fact]
        public void OffsetByCauseOverflow()
        {
            // Arrange
            var testMeasure = new Measure(ushort.MaxValue);
            // Act
            testMeasure.OffsetBy(+1);
            // Assert
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Maximum);
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the value is truncated after increasing to go over the upper limit
        /// </summary>
        [Fact]
        public void IncreaseByCauseOverflow()
        {
            // Arrange
            var testMeasure = new Measure(ushort.MaxValue);
            var increase = new Measure(1);
            // Act
            testMeasure.IncreaseBy(increase);
            // Assert
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Maximum);
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the value is truncated at zero after decreasing to get negative
        /// </summary>
        [Fact]
        public void DecreaseByCauseNegative()
        {
            // Arrange
            var testMeasure = new Measure(0);
            var decrease = new Measure(1);
            // Act
            testMeasure.DecreaseBy(decrease);
            // Assert
            Assert.Equal<long>(0, testMeasure.Maximum);
            Assert.Equal<long>(0, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the sum of two <see cref="Measure"/> could cause overflow
        /// </summary>
        [Fact]
        public void SumCauseOverflow()
        {
            // Arrange
            var maxMeasure = new Measure(ushort.MaxValue);
            // Act
            var testMeasure = maxMeasure + maxMeasure;
            // Assert
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Maximum);
            Assert.Equal<long>(ushort.MaxValue, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the difference between two <see cref="Measure"/> could cause underflow or negative
        /// </summary>
        [Fact]
        public void SubCauseNegative()
        {
            // Arrange
            var zero = new Measure(0);
            var one = new Measure(1);
            // Act
            var testMeasure = zero - one;
            // Assert
            Assert.Equal<long>(1, testMeasure.Maximum);
            Assert.Equal<long>(0, testMeasure.Value);
        }

        /// <summary>
        /// Verify if the equal operator works as intended, comparing value and maximum wise
        /// </summary>
        [Fact]
        public void EqEqOperator()
        {
            // Arrange
            var zeroOne = new Measure(0, 1);
            var testMeasure = new Measure(1) - new Measure(1);
            // Act
            // Assert
            Assert.True(zeroOne == testMeasure);
        }

    }
}