namespace Model.MSTest
{
public class TripsTests
{

    [Fact]
    public void AddingTripsToCollection()
    {
        //Arrange
        var trips = new List<Trip> {
                    new Trip() {Date = new DateOnly(2023, 6, 24), TripName = "Da Vinci Expo", City = "Antwerpen", Country = "Belgium", Type = Enums.TripType.Expo},
                    new Trip() {Date = new DateOnly(2023, 7, 28), TripName = "Plopsaland", City = "De Panne", Country = "Belgium", Type = Enums.TripType.Pretpark}
                };
        var tripStorageMock = new Mock<Trips>(trips);
        var tripsObject = new Trips(trips);

        //Act
        tripsObject.AddTrips(trips);

        //Assert
        tripStorageMock.Verify(x => x.AddTrips(trips), Times.Once());
    }

    [Fact]
    public void ExceptionThrownWhenAddingNullTrips()
    {
        //Arrange
        var tripsObject = new Trips(new List<Trip>());

        //Act & Assert
        Assert.Throws<Exception>(() => tripsObject.AddTrips(null));
    }
}
 }

