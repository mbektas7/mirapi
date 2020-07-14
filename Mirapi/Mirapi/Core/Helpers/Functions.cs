using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mirapi.Persistence;

namespace Mirapi.Core.Helpers
{
    public class Functions
    {
        public static Expression<Func<T, bool>> Filters<T>(List<FilterModelIn> propertyFilterValue) where T : class
        {
            //ToUpper(new CultureInfo("tr-TR", false)) bunu ekleyebilirsin
            //ama şimdilik küçük büyük harf duyarlı
            ParameterExpression argParam = Expression.Parameter(typeof(T), "s");
            Expression body = null;
            Expression e = null;
            string uid = null;

            if (propertyFilterValue != null)
            {
                foreach (var item in propertyFilterValue)
                {
                    //eksikler 2 tarih arası between ve foreqinler
                    var allProp = typeof(T).GetProperties();
                    PropertyInfo elementProp = typeof(T).GetProperty(item.property);

                    ///Geçici Kod//
                    if (elementProp == null)
                    {
                        continue;
                    }
                    ///Geçici Kod//
                    
                    Type t = Nullable.GetUnderlyingType(elementProp.PropertyType) ?? elementProp.PropertyType;
                    object safeValue = (item.value == null) ? null : Convert.ChangeType(item.value, t);
                    ConstantExpression val1 = Expression.Constant(safeValue);

                    //ApplicationDbContext _context;
                    //DbSet<T> _contextTable;

                    //var sonuc = _contextTable..Where(predicate);

                    if (t == typeof(string))
                    {
                        var getter = Expression.Property(argParam, item.property);
                        var stringContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        e = Expression.Call(getter, stringContainsMethod,
                            Expression.Constant(safeValue, typeof(string)));
                    }
                    else if (t == typeof(DateTime))
                    {
                        if (item.condition == "<=")
                            e = Expression.LessThanOrEqual(Expression.Property(argParam, item.property), val1);
                        else if (item.condition == "<")
                            e = Expression.LessThan(Expression.Property(argParam, item.property), val1);
                        else if (item.condition == ">=")
                            e = Expression.GreaterThanOrEqual(Expression.Property(argParam, item.property), val1);
                        else if (item.condition == ">")
                            e = Expression.GreaterThan(Expression.Property(argParam, item.property), val1);
                    }
                    else
                        e = Expression.Equal(Expression.Property(argParam, item.property), val1);

                    if (body == null)
                    {
                        body = Expression.Or(e, e);
                    }
                    else
                    {
                        if (item.optr == "&&" || string.IsNullOrWhiteSpace(item.optr))
                            body = Expression.And(body, e);
                        else if (item.optr == "||")
                            body = Expression.Or(body, e);
                    }

                }
            }

            
            if (body == null)
                return null;

            return Expression.Lambda<Func<T, bool>>(body, argParam);
        }

        public static Expression<Func<T, bool>> FiltersAlternative<T>(string filter) where T : class
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            // (T _) => ...
            var param = Expression.Parameter(type, "_");
            //filter
            var filterConstant = Expression.Constant(filter);
            // null
            var nullString = Expression.Constant(null, typeof(string));

            Expression body = null;

            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                if (propertyType == typeof(string))
                {
                    // _.Property
                    var property = Expression.Property(param, propertyInfo);
                    // _.Property != null
                    var notNull = Expression.NotEqual(property, nullString);
                    // _.Property.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)
                    var method = Expression.Call(
                        property,
                        propertyType.GetMethod("IndexOf", new[] { typeof(string), typeof(StringComparison) }),
                        filterConstant,
                        Expression.Constant(StringComparison.CurrentCultureIgnoreCase)
                    );
                    // _.Property.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0
                    var contains = Expression.GreaterThanOrEqual(method, Expression.Constant(0));
                    // _.Property != null && _.Property.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0
                    var condition = Expression.AndAlso(notNull, contains);

                    if (body == null)
                    {
                        body = condition;
                    }
                    else
                    {
                        body = Expression.Or(body, condition);
                    }
                }
                else if (propertyType.IsValueType)
                {
                    // _.Property
                    var property = Expression.Property(param, propertyInfo);
                    // _.Property.ToString()
                    var method = Expression.Call(
                        property,
                        propertyType.GetMethod("ToString", new Type[0])
                    );
                    // _.Property.ToString().IndexOf(filter, StringComparison.CurrentCultureIgnoreCase)
                    method = Expression.Call(
                        method,
                        method.Method.ReturnType.GetMethod("IndexOf", new[] { typeof(string), typeof(StringComparison) }),
                        filterConstant,
                        Expression.Constant(StringComparison.CurrentCultureIgnoreCase)
                    );
                    // _.Property.ToString().IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) >= 0
                    var contains = Expression.GreaterThanOrEqual(method, Expression.Constant(0));
                    var condition = contains;

                    if (body == null)
                    {
                        body = condition;
                    }
                    else
                    {
                        body = Expression.Or(body, condition);
                    }
                }
            }

            var lambda = Expression.Lambda<Func<T, bool>>(body, param);

            return lambda;
        }

        public static IList<ValidationResult> ModelValidate<T>(T entity)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(entity, null, null);
            Validator.TryValidateObject(entity, validationContext, validationResults, true);

            return validationResults;
        }

        public static Dictionary<string, string[]> ModelStateGetErrors(ModelStateDictionary modelState)
        {
            return modelState.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
        }

        public static bool FileUpload(string fullPath, byte[] fileAsByte)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                File.WriteAllBytes(fullPath, fileAsByte);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DepolamaAnaDizinAl()
        {
            return Startup.Configuration["UploadBaseDirectory:path"];
        }

        /// <summary>
        /// Haversine Formülüne Göre Noktalar Arası Uzaklığı metre olarak veren metottur.
        /// </summary>
        /// <param name="firstLat"></param>
        /// <param name="firstLng"></param>
        /// <param name="secondLat"></param>
        /// <param name="secondLng"></param>
        /// <returns></returns>
        public double findDistance(double firstLat, double firstLng, double secondLat, double secondLng)
        {
            int Rk = 6371; // dünya yarçapı

            double lat1, lon1, lat2, lon2, dlat, dlon, a, c, dk, km;

            lat1 = firstLat * Math.PI / 180;
            lon1 = firstLng * Math.PI / 180;
            lat2 = secondLat * Math.PI / 180;
            lon2 = secondLng * Math.PI / 180;

            dlat = lat2 - lat1;
            dlon = lon2 - lon1;

            a = Math.Pow(Math.Sin(dlat / 2), 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);
            c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            dk = c * Rk; // kilometre cinsinden
            km = Math.Round(dk, 3);

            return km * 1000;
        }

        public static JwtSecurityToken tokenS(HttpRequest request)
        {
            var handler = new JwtSecurityTokenHandler();
            var accesToken = request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

            return  handler.ReadJwtToken(accesToken) as JwtSecurityToken;
        }

        public static string getUserIdInTokens(HttpRequest Request)
        {
            return tokenS(Request).Claims.First(claim => claim.Type == "userid").Value;

        }

        public static IPAddress ClientGetIP(HttpRequest request)
        {
            return request.HttpContext.Connection.RemoteIpAddress ?? request.HttpContext.Connection.LocalIpAddress;
        }

    }
}
