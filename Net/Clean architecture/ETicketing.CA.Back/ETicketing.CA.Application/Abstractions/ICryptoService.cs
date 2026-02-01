using System;
using System.Collections.Generic;
using System.Text;

namespace ETicketing.CA.Application.Abstractions
{
    public interface ICryptoService
    {
        string CreateHash(string text);
        bool VerifyHash(string text, string hash);
    }
}
