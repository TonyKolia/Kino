using Kino.Models.KinoDBModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KinoApp.Helpers
{
    public static class GeneralHelper
    {
        public static string Encrypt(string plainText)
        {
            var sha = SHA256Managed.Create();
            var encoding = new UTF8Encoding();
            var hash = sha.ComputeHash(encoding.GetBytes(plainText));
            string shash = "";
            foreach (var b in hash)
            {
                shash += b.ToString();
            }
            return shash;
        }

        public static int GetUserId(this ClaimsIdentity claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "Id").Value);
        }

        public static User GetCurrentUser(this ClaimsIdentity claimsPrincipal)
        {
            if(claimsPrincipal.Claims.Count() == 0) 
            {
                return new User();
            }

            return new User
            {
                Id = int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "Id")?.Value),
                Username = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
                Category = int.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value),
                Email = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Birthdate = DateTime.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "BirthDate")?.Value),
                RegisterDate = DateTime.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "RegistrationDate")?.Value)
            };
        }

        public static bool IsObjectEmpty(object myObject)
        {
            var isObjectNull = true;
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    var value = (string)pi.GetValue(myObject);
                    if (!string.IsNullOrEmpty(value))
                    {
                        isObjectNull = false;
                        break;
                    }
                }

                if (pi.PropertyType == typeof(int?))
                {
                    var value = (int?)pi.GetValue(myObject);
                    if (value.HasValue)
                    {
                        isObjectNull = false;
                        break;
                    }
                }

                if (pi.PropertyType == typeof(decimal?))
                {
                    var value = (decimal?)pi.GetValue(myObject);
                    if (value.HasValue)
                    {
                        isObjectNull = false;
                        break;
                    }
                }

                if (pi.PropertyType == typeof(DateTime?))
                {
                    var value = (DateTime?)pi.GetValue(myObject);
                    if (value.HasValue)
                    {
                        isObjectNull = false;
                        break;
                    }
                }
            }

            return isObjectNull;
        }
    }
}
