using FluentAssertions;
using MicroMarinCaseV2.Domain.AggregateModels.CustomerModels;
using MicroMarinCaseV2.Domain.AggregateModels.OrderModels;
using MicroMarinCaseV2.Domain.ValueObjects;

namespace MicroMarinCaseV2.Domain.UnitTest.UnitTests
{
    public class CustomerDomainUnitTests
    {
        [Fact]
        public void Create_ShouldReturnValidCustomer_WhenValidParametersArePassed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "John";
            var surname = "Doe";
            var email = "john.doe@example.com";
            var address = new Address("USA", "New York", "Street 123");

            // Act
            var customer = Customer.Create(id, name, surname, email, address);

            // Assert
            customer.Should().NotBeNull();
            customer.Id.Should().Be(id);
            customer.Name.Should().Be(name);
            customer.Surname.Should().Be(surname);
            customer.Email.Should().Be(email);
            customer.Address.Should().Be(address);
            customer.Orders.Should().BeEmpty();
        }

        [Fact]
        public void Update_ShouldUpdateCustomerDetails_WhenValidParametersArePassed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var initialName = "John";
            var initialSurname = "Doe";
            var initialEmail = "john.doe@example.com";
            var initialAddress = new Address("USA", "New York", "Street 123");

            var customer = Customer.Create(id, initialName, initialSurname, initialEmail, initialAddress);

            var updatedName = "Jane";
            var updatedSurname = "Doe";
            var updatedEmail = "jane.doe@example.com";
            var updatedAddress = new Address("Canada", "Toronto", "Street 456");

            // Act
            customer.Update(updatedName, updatedSurname, updatedEmail, updatedAddress);

            // Assert
            customer.Name.Should().Be(updatedName);
            customer.Surname.Should().Be(updatedSurname);
            customer.Email.Should().Be(updatedEmail);
            customer.Address.Should().Be(updatedAddress);
        }

        [Fact]
        public void AddOrders_ShouldAddOrdersToCustomer_WhenValidOrdersArePassed()
        {
            // Arrange
            var customer = Customer.Create(Guid.NewGuid(), "John", "Doe", "john.doe@example.com", new Address("USA", "New York", "Street 123"));
            var orders = new List<Order>
        {
            new Order(Guid.NewGuid(), 100),
            new Order(Guid.NewGuid(), 200)
        };

            // Act
            customer.AddOrders(orders);

            // Assert
            customer.Orders.Should().HaveCount(2);
            customer.Orders.Should().Contain(orders);
        }

        [Fact]
        public void Create_ShouldThrowArgumentNullException_WhenInvalidParametersArePassed()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => Customer.Create(Guid.NewGuid(), null, "Doe", "john.doe@example.com", new Address("USA", "New York", "Street 123")));
            Assert.Throws<ArgumentNullException>(() => Customer.Create(Guid.NewGuid(), "John", null, "john.doe@example.com", new Address("USA", "New York", "Street 123")));
            Assert.Throws<ArgumentNullException>(() => Customer.Create(Guid.NewGuid(), "John", "Doe", null, new Address("USA", "New York", "Street 123")));
            Assert.Throws<ArgumentNullException>(() => Customer.Create(Guid.NewGuid(), "John", "Doe", "john.doe@example.com", null));
        }

        [Fact]
        public void AddOrders_ShouldThrowArgumentNullException_WhenNullOrdersArePassed()
        {
            // Arrange
            var customer = Customer.Create(Guid.NewGuid(), "John", "Doe", "john.doe@example.com", new Address("USA", "New York", "Street 123"));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => customer.AddOrders(null));
        }

        [Fact]
        public void Update_ShouldThrowArgumentNullException_WhenInvalidParametersArePassed()
        {
            // Arrange
            var customer = Customer.Create(Guid.NewGuid(), "John", "Doe", "john.doe@example.com", new Address("USA", "New York", "Street 123"));

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => customer.Update(null, "Doe", "john.doe@example.com", new Address("USA", "New York", "Street 123")));
            Assert.Throws<ArgumentNullException>(() => customer.Update("John", null, "john.doe@example.com", new Address("USA", "New York", "Street 123")));
            Assert.Throws<ArgumentNullException>(() => customer.Update("John", "Doe", null, new Address("USA", "New York", "Street 123")));
            Assert.Throws<ArgumentNullException>(() => customer.Update("John", "Doe", "john.doe@example.com", null));
        }
    }
}
