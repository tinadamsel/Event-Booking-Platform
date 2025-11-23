using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service
{
    public interface IMemberBaseService
    {
        Task<MemberApiDTO.ApiResponse<MemberApiDTO.ContactResponseData>> CreateContactAsync(MemberApiDTO.CreateContactRequest req);
    }
}
