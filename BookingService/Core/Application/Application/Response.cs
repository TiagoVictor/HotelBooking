namespace Application.Responses
{
    public enum ErrorCode
    {
        // Guests related codes 1 to 99
        NOT_FOUND = 1,
        COULDNOT_STORE_DATA = 2,
        INVALID_DOCUMENT = 3,
        MISSING_REQUIRED_INFORMATION = 4,
        INVALID_EMAIL = 5,


        // Room related code 100 to 199
        ROOM_NOT_FOUND = 100,
        ROOM_COLD_NOT_STORE_DATA = 101,
        ROOM_INVALID_PERSON_ID = 102,
        ROOM_MISSING_REQUIRED_INFORMATION = 103,
        ROOM_INVALID_EMAIL = 104,

        // Booking related code 200 to 299
        BOOKING_NOT_FOUND = 200,
        BOOKING_COLD_NOT_STORE_DATA = 201,
        BOOKING_MISSING_REQUIRED_INFORMATION = 202,
        BOOKING_ROOM_CANNOT_BE_BOOKED = 203
    }

    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
    }
}
