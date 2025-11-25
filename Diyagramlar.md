
classDiagram
    direction LR

    class User {
        <<Abstract>>
        +int Id
        +string UserName
        +string PasswordHash
        +string Name
        +string Surname
        +bool Login(string username, string password)*
    }

    class Admin {
        +bool Login(string username, string password)
    }

    class Customer {
        +string TCKimlikNo
        +List~Reservation~ Reservations
        +bool Login(string username, string password)
        +Reservation MakeReservation(Flight flight, Seat seat)
        +void CancelReservation(string pnr)
    }

    class Reservation {
        +int Id
        +string Pnr
        +DateTime ReservationDate
        +decimal PricePaid
        +Customer ReservedCustomer
        +Flight ReservedFlight
        +Seat SelectedSeat
        +string ToString()
    }

    class Flight {
        +int Id
        +string FlightNumber
        +string DepartureCity
        +string ArrivalCity
        +DateTime DepartureTime
        +Airplane Airplane
        +decimal BasePrice
        +string ToString()
    }

    class Airplane {
        +string Model
        +int Capacity
        +List~Seat~ Seats
        +Airplane(string model, int capacity)
    }

    class Seat {
        +string SeatNumber
        +SeatStatus Status
        +decimal PriceMultiplier
        +Seat()
        +string ToString()
    }

    class SeatStatus {
        <<Enumeration>>
        Available
        Occupied
    }

    ' --- Ýliþkiler ---

    ' Kalýtým (Inheritance)
    User <|-- Admin
    User <|-- Customer

    ' Kompozisyon (Composition) - Airplane, Seat'leri oluþturur ve sahibi
    Airplane "1" *-- "1..*" Seat : Contains

    ' Birleþim (Aggregation) - Customer rezervasyonlara sahiptir
    Customer "1" o-- "0..*" Reservation : Has
    ' Birleþim (Aggregation) - Flight bir Airplane kullanýr
    Flight "1" o-- "1" Airplane : Uses

    ' Ýliþki (Association) - Reservation, diðer nesneleri birbirine baðlar
    Reservation "0..*" -- "1" Flight : ReservedOn
    Reservation "0..*" -- "1" Seat : SelectedFor
    Reservation "0..*" -- "1" Customer : MadeBy

    ' Baðýmlýlýk (Dependency)
    Seat ..> SeatStatus : Uses
    Customer ..> Flight : Uses
    Customer ..> Seat : Uses
    Customer ..> SeatStatus : Uses
    