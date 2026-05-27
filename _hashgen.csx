using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
var salt = RandomNumberGenerator.GetBytes(16);
var key = KeyDerivation.Pbkdf2("Demo@123", salt, KeyDerivationPrf.HMACSHA256, 100000, 32);
Console.WriteLine($"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}");
