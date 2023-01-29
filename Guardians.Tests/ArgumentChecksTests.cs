using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Guardians.Tests
{
    [TestClass]
    public class ArgumentChecksTests
    {
        [TestMethod]
        public void NonNull_ShouldThrowArgumentNullException_WhenNullPassed()
        {
            //arrange
            object? nullVal = null;
            //act
            Action act = () => Ensure.Argument.NotNull(nullVal);

            //assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Argument is null. (Parameter 'nullVal')")
                .Which.ParamName.Should().Be(nameof(nullVal));
        }

        [TestMethod]
        public void NonNull_ShouldThrowArgumentNullException_WhenNullableWithoutValuePassed()
        {
            //arrange
            int? nullableVal = null;
            //act
            Action act = () => Ensure.Argument.NotNull(nullableVal);

            //assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Argument is null. (Parameter 'nullableVal')")
                .Which.ParamName.Should().Be(nameof(nullableVal));
        }

        [TestMethod]
        public void NonNullOrEmptyString_ShouldThrowArgumentNullException_WhenNullPassed()
        {
            //arrange
            string? nullStr = null;
            //act
            Action act = () => Ensure.Argument.NotNullOrEmpty(nullStr);

            //assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Argument is null. (Parameter 'nullStr')")
                .Which.ParamName.Should().Be(nameof(nullStr));
        }

        [TestMethod]
        public void NonNullOrEmptyString_ShouldThrowArgumentException_WhenEmptyPassed()
        {
            //arrange
            string emptyStr = "";
            //act
            Action act = () => Ensure.Argument.NotNullOrEmpty(emptyStr);

            //assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Argument is empty. (Parameter 'emptyStr')")
                .Which.ParamName.Should().Be(nameof(emptyStr));
        }

        [TestMethod]
        public void NotNullOrEmptyEnumerable_ShouldThrowArgumentNullException_WhenNullPassed()
        {
            //arrange
            int[]? enumerable = null;
            //act
            Action act = () => Ensure.Argument.NotNullOrEmpty(enumerable);
            //assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Argument is null. (Parameter 'enumerable')")
                .Which.ParamName.Should().Be(nameof(enumerable));
        }

        [TestMethod]
        public void NotNullOrEmptyEnumerable_ShouldThrowArgumentException_WhenEmptyPassed()
        {
            //arrange
            int[]? enumerable = new int[0];
            //act
            Action act = () => Ensure.Argument.NotNullOrEmpty(enumerable);
            //assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Argument is empty. (Parameter 'enumerable')")
                .Which.ParamName.Should().Be(nameof(enumerable));
        }

        [TestMethod]
        public void HasNoNulls_ShouldThrowArgumentNullException_WhenNullEnumerablePassed()
        {
            //arrange
            string?[]? enumerable = null;
            //act
            Action act = () => Ensure.Argument.HasNoNulls(enumerable);
            //assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Argument is null. (Parameter 'enumerable')")
                .Which.ParamName.Should().Be(nameof(enumerable));
        }

        [TestMethod]
        public void HasNoNulls_ShouldThrowArgumentException_WhenEnumerableWithNullPassed()
        {
            //arrange
            string?[]? enumerable = new [] {"one", null,"three" };
            //act
            Action act = () => Ensure.Argument.HasNoNulls(enumerable);
            //assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Argument has null elements. (Position 1) (Parameter 'enumerable')")
                .Which.ParamName.Should().Be(nameof(enumerable));
        }

        [TestMethod]
        public void NonEmptyGuid_ShouldThrowArgumentException_WhenEmptyGuidPassed()
        {
            //arrange
            Guid guid = Guid.Empty;
            //act
            Action act = () => Ensure.Argument.NotEmpty(guid);

            //assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Argument is empty. (Parameter 'guid')")
                .And.ParamName.Should().Be(nameof(guid));
        }

        [TestMethod]
        public void MeetsCondition_ShouldThrowArgumentException_WhenConditionWasNoMet()
        {
            //arrange

            //act
            Action act = () => Ensure.Argument.MeetsCondition(false);
            //assert
            act.Should().Throw<ArgumentException>()
                .Which.Message.Should().Be("Arguments condition was not met. (Condition 'false')");
        }

        [TestMethod]
        public void IsPositive_ShouldThrowArgumentOutOfRangeException_WhenNumberIsNonPositive()
        {
            //arrange

            //act
            Action act = () => Ensure.Argument.IsPositive(0);
            //assert
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("Argument is not positive. (Parameter '0')")
                .Which.ParamName.Should().Be("0");
        }

        [TestMethod]
        public void IsOfType_ShouldThrowArgumentException_WhenInstanceOfIncompatibleTypePassed()
        {
            //arrange
            var catStr = "";
            //act
            Action act = () => Ensure.Argument.IsOfType<FakeCat>(catStr);
            //assert
            act.Should().Throw<ArgumentException>()
                .WithMessage("Argument is of incompatible type. (Type 'System.String') (Parameter 'catStr')")
                .Which.ParamName.Should().Be(nameof(catStr));
        }

        [TestMethod]
        public void IsOfType_ShouldThrowArgumentNullException_WhenNullPassed()
        {
            //arrange
            string? catStr = null;
            //act
            Action act = () => Ensure.Argument.IsOfType<FakeCat>(catStr);
            //assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Argument is null. (Parameter 'catStr')")
                .Which.ParamName.Should().Be(nameof(catStr));
        }
    }

    class FakeAnimal { };
    class FakeMammal : FakeAnimal { }
    class FakeCat : FakeMammal { }
        
}