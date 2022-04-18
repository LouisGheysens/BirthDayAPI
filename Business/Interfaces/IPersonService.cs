using Business.Services;
using Dto.Models;
using Gridify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPersonService
    {
        Task<ServiceResponse<List<PersonDto>>> GetPersonsAsync(GridifyQuery gQuery, CancellationToken token);
        Task<ServiceResponse<PersonDto>> GetPerson(int id, CancellationToken token);
        Task<ServiceResponse<int>> UpdatePerson(int id, PersonDto dto, CancellationToken token);
        Task<ServiceResponse<int>> AddPerson(PersonDto dto, CancellationToken token);
        Task<ServiceResponse<object>> DeletePerson(int id, CancellationToken token);
    }
}
