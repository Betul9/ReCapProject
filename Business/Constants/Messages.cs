using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public class Messages
    {
        public static string BrandAdded = "Marka Eklendi";
        public static string BrandDeleted = "Marka Silindi";
        public static string BrandUpdated = "Marka Bilgisi Güncellendi";
        public static string CarAdded = "Araba Eklendi";
        public static string CarPriceInvalid = "Araba Fiyatı Geçersiz";
        public static string CarDeleted = "Araba Silindi";
        public static string CarUpdated = "Araba Bilgisi Güncellendi";
        public static string ColorAdded = "Renk Eklendi";
        public static string ColorUpdated = "Renk Bilgisi Güncellendi";
        public static string ColorDeleted = "Renk Silindi";
        public static string CustomerUpdated = "Müşteri Bilgisi Güncellendi";
        public static string CustomerDeleted = "Müşteri Silindi";
        public static string CustomerAdded = "Müşteri Eklendi";
        public static string RentalAdded = "Rental Eklendi";
        public static string RentalDeleted = "Rental Silindi";
        public static string RentalUpdated = "Rental Güncellendi";
        public static string UserAdded = "Kullanıcı Eklendi";
        public static string UserDeleted = "Kullanıcı Silindi";
        public static string UserUpdated = "Kullanıcı Bilgisi Güncellendi";
        public static string RentalNotAvailable = "Rental Mevcut Değil";
        public static string RentalCouldNotFound = "Rental Bulunamadı";
        public static string CarImageAdded = "Araba Fotoğrafı Eklendi";
        public static string ImageCouldNotBeAdded = "Araba Fotoğrafı Eklenemedi";
        public static string AuthorizationDenied = "Yetkiniz Yok";
        public static string carUpdated = "Araba Güncellendi";
        public static string DefaultCarImageCannotBeAdded="Default Resim Eklenemedi";
        public static string CarRentDateSet;
        public static string CarRentDateCouldNotSet;
        public static string RentalAvailable;
        public static string CarRentable;
        public static string CardAdded;
        public static string CardDeleted;
        public static string CardUpdated;
        public static string CardDoesNotExist;
        public static string CardExists;
        public static string CustomerCardUpdated;
    }
}
