using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace practice.Model
{
    public class Models
    {
        [Table("guests", Schema = "public")]
        public class guests
        {
            [Key]
            public int id { get; set; }

            //[JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_регистрации")]
            public DateTime дата_регистрации { get; set; }
            public string Имя { get; set; }
            public string Фамилия { get; set; }
            public string Телефон { get; set; }
            public string email { get; set; }

            public ICollection<bookings> Bookings { get; set; }
        }

        [Table("rooms", Schema = "public")]
        public class rooms
        {
            [Key]
            public int id { get; set; }
            public int номер { get; set; }
            public decimal стоимость_за_ночь { get; set; }
            public int вместимость { get; set; }
            public int тип_номера { get; set; }

            public ICollection<bookings> Bookings { get; set; }
            public ICollection<room_assignments> RoomAssignments { get; set; }
        }
        public class roomsDto
        {
            [Key]
            public int id { get; set; }
            public int номер { get; set; }
            public decimal стоимость_за_ночь { get; set; }
            public int вместимость { get; set; }
            public int тип_номера { get; set; }
        }

        [Table("staff", Schema = "public")]
        public class staff
        {
            [Key]
            public int id { get; set; }
            public string Имя { get; set; }
            public string Фамилия { get; set; }
            public string Должность { get; set; }

            public ICollection<room_assignments> RoomAssignments { get; set; }
        }
        public class staffDto
        {
            [Key]
            public int id { get; set; }
            public string Имя { get; set; }
            public string Фамилия { get; set; }
            public string Должность { get; set; }

        }

        [Table("room_types", Schema = "public")]
        public class room_types
        {
            [Key]
            public int id { get; set; }
            public string название { get; set; }
        }

        [Table("services", Schema = "public")]
        public class services
        {
            [Key]
            public int id { get; set; }
            public decimal Стоимость { get; set; }
            public string Название { get; set; }
            public string Тип { get; set; }

            public ICollection<booked_services> BookedServices { get; set; }
        }
        public class servicesDto
        {
            [Key]
            public int id { get; set; }
            public decimal Стоимость { get; set; }
            public string Название { get; set; }
            public string Тип { get; set; }
        }

        [Table("bookings", Schema = "public")]
        public class bookings
        {
            [Key]
            public int id { get; set; }

            public int Номер_id { get; set; }
            public int Гость_id { get; set; }
            //[JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_заезда")]
            public DateTime дата_заезда { get; set; }
            //[JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_выезда")]
            public DateTime дата_выезда { get; set; }
            public bool Статус { get; set; }

            [ForeignKey("Гость_id")]
            public guests Guest { get; set; }

            [ForeignKey("Номер_id")]
            public rooms Room { get; set; }

            public ICollection<booked_services> BookedServices { get; set; }
            public ICollection<payments> Payments { get; set; }
        }

        [Table("booked_services", Schema = "public")]
        public class booked_services
        {
            [Key]
            public int id { get; set; }

            public int Бронирование_id { get; set; }
            public int услуга_id { get; set; }
            public int Количество { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("Дата")]
            public DateTime Дата { get; set; }

            [ForeignKey("Бронирование_id")]
            public bookings Booking { get; set; }

            [ForeignKey("услуга_id")]
            public services Service { get; set; }
        }
        public class booked_servicesDto
        {
            [Key]
            public int id { get; set; }

            public int Бронирование_id { get; set; }
            public int услуга_id { get; set; }
            public int Количество { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("Дата")]
            public DateTime Дата { get; set; }
        }

        [Table("room_assignments", Schema = "public")]
        public class room_assignments
        {
            public int Номер_id { get; set; }
            public int Сотрудник_id { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("Дата")]
            public DateTime Дата { get; set; }

            [ForeignKey("Номер_id")]
            public rooms Room { get; set; }

            [ForeignKey("Сотрудник_id")]
            public staff Staff { get; set; }
        }
        public class room_assignmentsDto
        {
            public int Номер_id { get; set; }
            public int Сотрудник_id { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("Дата")]
            public DateTime Дата { get; set; }
        }

        [Table("payments", Schema = "public")]
        public class payments
        {
            [Key]
            public int id { get; set; }
            public int Бронирование_id { get; set; }
            public decimal Сумма { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_заезда")]
            public DateTime Дата_платежа { get; set; }
            public string Метод { get; set; }

            [ForeignKey("Бронирование_id")]
            public bookings Booking { get; set; }
        }
        public class paymentsDto
        {
            [Key]
            public int id { get; set; }
            public int Бронирование_id { get; set; }
            public decimal Сумма { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_заезда")]
            public DateTime Дата_платежа { get; set; }
            public string Метод { get; set; }
        }
        public class BookingDto
        {
            public int Номер_id { get; set; }
            public int Гость_id { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_заезда")]
            public DateTime дата_заезда { get; set; }
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_выезда")]
            public DateTime дата_выезда { get; set; }
            public bool Статус { get; set; }
        }
        public class GuestDto
        {
            [JsonConverter(typeof(CustomDateConverter))]
            [JsonPropertyName("дата_регистрации")]
            public DateTime дата_регистрации { get; set; }
            public string Имя { get; set; }
            public string Фамилия { get; set; }
            public string Телефон { get; set; }
            public string email { get; set; }
        }
    }
}
//using System;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text.Json.Serialization;

//namespace practice.Model
//{
//    public class Models
//    {
//        [Table("guests", Schema = "public")]
//        public class guests
//        {
//            [Key]
//            public int id { get; set; }
//            //[JsonConverter(typeof(CustomDateConverter))]
//            [JsonPropertyName("дата_регистрации")]
//            public DateTime дата_регистрации { get; set; }
//            public string Имя { get; set; }
//            public string Фамилия { get; set; }
//            public string Телефон { get; set; }
//            public string email { get; set; }
//        }

//        [Table("rooms", Schema = "public")]
//        public class rooms
//        {
//            [Key]
//            public int id { get; set; }
//            public int номер { get; set; }
//            public decimal стоимость_за_ночь { get; set; }
//            public int вместимость { get; set; }
//            public int тип_номера { get; set; }
//        }

//        [Table("staff", Schema = "public")]
//        public class staff
//        {
//            [Key]
//            public int id { get; set; }
//            public string Имя { get; set; }
//            public string Фамилия { get; set; }
//            public string Должность { get; set; }
//        }

//        [Table("room_types", Schema = "public")]
//        public class room_types
//        {
//            [Key]
//            public int id { get; set; }
//            public string название { get; set; }
//        }

//        [Table("services", Schema = "public")]
//        public class services
//        {
//            [Key]
//            public int id { get; set; }
//            public decimal Стоимость { get; set; }
//            public string Название { get; set; }
//            public string Тип { get; set; }
//        }

//        [Table("bookings", Schema = "public")]
//        public class bookings
//        {
//            [Key]
//            public int id { get; set; }
//            public int Номер_id { get; set; }
//            public DateTime дата_заезда { get; set; }
//            public DateTime дата_выезда { get; set; }
//            public bool Статус { get; set; }
//            public int Гость_id { get; set; }
//        }

//        [Table("booked_services", Schema = "public")]
//        public class booked_services
//        {
//            [Key]
//            public int id { get; set; }
//            public int Бронирование_id { get; set; }
//            public int Услуга_id { get; set; }
//            public int Количество { get; set; }
//            public DateTime Дата { get; set; }
//        }

//        [Table("room_assignments", Schema = "public")]
//        public class room_assignments
//        {
//            public int Номер_id { get; set; }
//            public int Сотрудник_id { get; set; }
//            public DateTime Дата { get; set; }
//        }

//        [Table("payments", Schema = "public")]
//        public class payments
//        {
//            [Key]
//            public int id { get; set; }
//            public int Бронирование_id { get; set; }
//            public decimal Сумма { get; set; }
//            public DateTime Дата_платежа { get; set; }
//            public string Метод { get; set; }
//        }
//    }
//}
