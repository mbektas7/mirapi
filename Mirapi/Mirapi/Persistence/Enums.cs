using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mirapi.Persistence
{
    //represents the enumarations 
    public class Enums
    {
        public static string SuperAdmin = "SuperAdmin";
        public static string Admin = "Admin";
        public const string ProfileImage= "6307dab5-f2ad-4145-8350-dcf5e154e9c6";
        public const string VerifiedImage= "6307dab5-f2ad-4145-8350-dcf5e154e9c6";

        public Enums()
        {

        }

        public enum ResultType
        {
            Error,
            Warning,
            Info,
            Success
        };

        public enum SpecificParameter
        {
            None,
            Armut = 1,
            Elma = 2,

        };

        public enum DaysOfWeek
        {
            Pazartesi,
            Salı,
            Çarşamba,
            Perşembe,
            Cuma,
            Cumartesi,
            Pazar
        };

        public enum CrudType
        {
            Create,
            Read,
            Update,
            Delete
        }

        public enum Claims
        {
            CompanyCreate,
            CompanyDelete,
            PersonelRead,
            PersonelUpdate

        };

        public enum TakeLimits
        {
            MaxTake = 100,

        };

    }

}
