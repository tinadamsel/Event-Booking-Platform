using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class MemberApiDTO
    {
        public class CreateContactRequest
        {
            public string surname { get; set; }
            public string firstName { get; set; }
            public string emailAddress { get; set; }
        }

        public class ApiResponse<T>
        {
            public string Message { get; set; }
            public T Data { get; set; }
        }

        public class ContactExtras
        {
            public string Type { get; set; }
            public int Id { get; set; }
        }

        public class ContactResponseData
        {
            public int Id { get; set; }
            public List<ContactExtras> Extras { get; set; }
        }
    }
}
